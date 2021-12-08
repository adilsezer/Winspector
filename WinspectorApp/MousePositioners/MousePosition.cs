using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MousePositioners
{
    public static class MousePosition
    {
        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out POINT lpPoint);

        public static Tuple<int, int> GetMousePosition()
        {
            POINT point;
            GetCursorPos(out point);
            return new Tuple<int, int>(point.X, point.Y);
        }
    }
}