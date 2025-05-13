using System.Numerics;
using System.Reflection;
using AlmostGoodFoster.Fonts;
using Foster.Framework;

namespace AlmostGoodFoster.Utils
{
    public static class BetterConsole
    {
        /// <summary>
        /// The current command
        /// </summary>
        public static string Current { get; internal set; } = "";

        /// <summary>
        /// The prexif of the command displayed firstly inside the console
        /// </summary>
        public static string Prefix { get; set; } = "";

        /// <summary>
        /// The command line displayed on the screen using the prefix
        /// </summary>
        internal static string DisplayCommand { get => Prefix + Current; }

        /// <summary>
        /// The list of known commands
        /// </summary>
        internal static Dictionary<string, Command> Commands { get; set; } = [];

        /// <summary>
        /// The commands history
        /// </summary>
        public static List<string> History { get; private set; } = [];

        /// <summary>
        /// True if the console is displayed
        /// </summary>
        public static bool Opened { get; internal set; } = false;

        /// <summary>
        /// If the keyboard used is using an azerty layout
        /// </summary>
        public static bool Azerty { get; internal set; } = true;

        #region Cursor parameters

        private static float _cursorTimer = 0f;
        private const float cursorBlinkDuration = 0.5f;
        private static bool _cursorBlink = false;
        private static int _cursorPosition = 0;

        #endregion

        #region History parameters

        private const int maxHistoryCapacity = 10_000;
        private static int _historyPosition = -1;
        private static string _tempCommand = "";

        #endregion

        #region Display parameters

        private const int consolePadding = 10;
        private const float consoleOpacity = 0.65f;
        private static SpriteFont? _font;

        private static Vector2 _prefixSize;
        private static Vector2 _commandSize;
        private static int _textHeight;

        #endregion

        /// <summary>
        /// Initialize the console
        /// </summary>
        internal static void Initialize()
        {
            Commands = [];
            History = [];

            Current = "";
            Prefix = "> ";

            _font = FontManager.Get("default", 12);
            _prefixSize = _font.SizeOf(Prefix);
            _commandSize = _font.SizeOf(Current);
            _textHeight = (int)_font.SizeOf("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789|çà&><!:;,?./§*$£µ^ù€²()=").Y;

            BuildCommands();
        }

        /// <summary>
        /// Create the default commands
        /// </summary>
        private static void BuildCommands()
        {
            MethodInfo[] methods = typeof(BetterConsole).GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (var method in methods)
            {
                if (method.GetCustomAttributes(typeof(CommandAttribute), false).FirstOrDefault() is CommandAttribute attribute)
                {
                    Command command = new()
                    {
                        Action = (args) =>
                        {
                            method.Invoke(method, args);
                        },
                        Usage = attribute.Name,
                        Description = attribute.Description,
                    };
                    Commands.Add(command.Usage, command);
                }
            }
        }

        internal static void HandleInputs(Input input, float deltaTime)
        {
            string previous = Current;
            AddToCurrent(input.Keyboard.Text.ToString());
            _cursorPosition += input.Keyboard.Text.Length;

            // If the command has changed, measure the new size of the command text
            if (previous != Current && _font != null)
            {
                _commandSize = _font.SizeOf(Current);
            }

            ManageToggle(input);
        }

        /// <summary>
        /// Update the console
        /// </summary>
        /// <param name="gameTime"></param>
        internal static void Update(float deltaTime)
        {
            if (!Opened)
            {
                return;
            }

            UpdateCursor(deltaTime);
        }

        /// <summary>
        /// Cursor blink animation
        /// </summary>
        /// <param name="delta"></param>
        private static void UpdateCursor(float delta)
        {
            _cursorTimer += delta;
            while (_cursorTimer >= cursorBlinkDuration)
            {
                _cursorTimer -= cursorBlinkDuration;
                _cursorBlink = !_cursorBlink;
            }
        }

        /// <summary>
        /// Manager the console's toggle
        /// </summary>
        /// <param name="delta"></param>
        private static void ManageToggle(Input input)
        {
            // The key used to open the console
            if (input.Keyboard.Pressed(Keys.F1))
            {
                Opened = !Opened;
            }
        }

        private static void AddToCurrent(string sub)
        {
            Current = Current.Insert(Math.Max(0, _cursorPosition - 1), sub);
        }

        private static void AddToCurrent(char sub) => AddToCurrent(sub.ToString());

        /// <summary>
        /// When the user's want to 
        /// </summary>
        private static void EnterCommand()
        {
            if (Current.Length == 0)
            {
                return;
            }

            // Get the command data
            string[] data = Current.Split(' ');
            if (data.Length == 0)
            {
                return;
            }

            if (Commands.ContainsKey(data[0]))
            {
                Commands[data[0]].Action?.Invoke(data);
            }

            // Manage history
            if (History.Count >= maxHistoryCapacity)
            {
                History.RemoveAt(0);
            }
            History.Add(Current);

            // Reset command
            Current = _tempCommand = "";
            _cursorPosition = 0;
        }

        /// <summary>
        /// Add a command to the console
        /// </summary>
        /// <param name="command"></param>
        public static void AddCommand(Command command)
        {
            if (Commands.ContainsKey(command.Usage))
            {
                return;
            }

            Commands.Add(command.Usage, command);
        }

        /// <summary>
        /// Remove a command from the console
        /// </summary>
        /// <param name="name"></param>
        public static void RemoveCommand(string name)
        {
            if (!Commands.ContainsKey(name))
            {
                return;
            }

            Commands.Remove(name);
        }

        public static void Render(Batcher batcher)
        {
            if (!Opened)
            {
                return;
            }

            var window = batcher.GraphicsDevice.App.Window;
            var consoleRect = new Rect(consolePadding, window.HeightInPixels - consolePadding - 30, window.WidthInPixels - consolePadding * 2, 30);

            batcher.Rect(consoleRect, Color.Black * consoleOpacity);
            batcher.Rect(new Rect(consoleRect.X, consoleRect.Y - 300 - consolePadding, consoleRect.Width, 300), Color.Black * consoleOpacity);

            if (_font != null)
            {
                // Text display
                int commandY = (int)(consoleRect.Y + consoleRect.Height / 2 - _textHeight / 2);
                batcher.Text(_font, DisplayCommand, new Vector2(consoleRect.X + 5, commandY), Color.White);

                // Cursor
                if (_cursorBlink)
                {
                    Vector2 subPos = _font.SizeOf(Current.Substring(0, _cursorPosition));
                    batcher.Text(_font, "|", new Vector2(consoleRect.X + 5 + _prefixSize.X + subPos.X, commandY), Color.White);
                }
            }
        }

        #region Commands

        [Command("fullscreen", "Toggle the fullscreen mode")]
        private static void Fullscreen()
        {

        }

        #endregion
    }
}
