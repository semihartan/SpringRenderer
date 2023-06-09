using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpringRenderer
{
    internal class DragHandle : IHittable
    {
        private float m_radius;
        public Vector2 Position;
        public DragHandle(Vector2 position, float radius)
        {
            Position = position;
            m_radius = radius;
        }
        public float Radius
        {
            get
            {
                return m_radius;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentException("The radius must be positive.");
                m_radius = value;
            }
        }
        public bool Contains(Vector2 point, out Vector2 delta)
        {
            delta = point - Position;
            if (delta.LengthSquared() <= m_radius * m_radius)
                return true;
            return false;
        }

        public static explicit operator Vector2(DragHandle handle)
        {
            return handle.Position;
        }
    }
}
