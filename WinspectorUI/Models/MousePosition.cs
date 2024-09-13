using System;
using System.Runtime.InteropServices;

namespace Winspector.Models
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

    public struct POINT
    {
        public int X;
        public int Y;
    }
}
