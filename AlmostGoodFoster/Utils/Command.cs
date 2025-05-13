using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlmostGoodFoster.Utils
{
    public struct Command
    {
        public Action<string[]> Action { get; set; }
        public string Usage { get; set; }
        public string Description { get; set; }
    }
}
