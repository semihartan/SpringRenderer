using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpringRenderer
{
    internal static class ResourceFactory
    {
        static ResourceFactory()
        {
            s_pen = new Pen(Color.Black, 1.0f);
        }
        private static Pen s_pen;

        public static Pen UsePen(Color color, float width)
        {
            s_pen.Color = color;
            s_pen.Width = width;
            return s_pen;
        }
    }
}