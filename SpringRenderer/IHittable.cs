using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpringRenderer
{
    internal interface IHittable
    {
        bool Contains(Vector2 point, out Vector2 delta);
    }
}
