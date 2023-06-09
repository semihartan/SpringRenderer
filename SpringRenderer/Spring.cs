using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpringRenderer
{
    internal class Spring : IDraggable
    {
        private DragHandle[] m_handles;
        private Vector2 m_delta;
        private float m_padding = 6.0f;
        private int m_nodeCount;
        private PointF[] m_geometryData = null;
        private float m_amplitude;
        private int m_hitIndex = -1;
        public Spring(float startX, float startY, float endX, float endY, int nodeCount, float amplitude = 40.0f, float padding = 20.0f) :
            this(new Vector2(startX, startY), new Vector2(endX, endY), nodeCount, amplitude, padding)
        {

        }
        public Spring(Vector2 start, Vector2 end, int nodeCount, float amplitude = 40.0f, float padding = 20.0f)
        {
            m_handles = new DragHandle[2];
            m_handles[0] = new DragHandle(start, 10.0f);
            m_handles[1] = new DragHandle(end, 10.0f);
            m_nodeCount = nodeCount;
            m_padding = padding;
            m_amplitude = amplitude;
            int pointCount = 2;
            if (m_padding > 0)
                pointCount += 2;

            pointCount += nodeCount * 2;

            m_geometryData = new PointF[pointCount];
            BuildGeometry();
        }

        private void BuildGeometry()
        {
            Vector2 directionVector = (Vector2)m_handles[1] - (Vector2)m_handles[0];
            float length = directionVector.Length();
            Vector2 directionVectorNormal = Vector2.Normalize(directionVector);
            float tStart = m_padding;
            float tEnd = length - m_padding;
            float tStep = (tEnd - tStart) / (4 * m_nodeCount);
            Vector2 nodeStart = (Vector2)m_handles[0] + directionVectorNormal * m_padding;
            Vector2 nodeEnd = (Vector2)m_handles[1] - directionVectorNormal * m_padding;
            m_geometryData[0] = (Vector2)m_handles[0];
            m_geometryData[m_geometryData.Length - 1] = (Vector2)m_handles[1];
            int startIndex = 0;
            if (m_padding > 0)
            {
                m_geometryData[1] = nodeStart;
                m_geometryData[m_geometryData.Length - 2] = nodeEnd;
                startIndex += 1;
            }
            Vector2 leftPerp = new Vector2(directionVectorNormal.Y, -directionVectorNormal.X);
            Vector2 rightPerp = new Vector2(-directionVectorNormal.Y, directionVectorNormal.X);
            for (int i = 0; i < m_nodeCount; i++)
            {
                int i0 = i * 4 + 1;
                int i1 = i0 + 2;
                m_geometryData[startIndex + i * 2 + 1] = nodeStart + i0 * tStep * directionVectorNormal + leftPerp * m_amplitude;
                m_geometryData[startIndex + i * 2 + 2] = nodeStart + i1 * tStep * directionVectorNormal + rightPerp * m_amplitude;
            }
        }
        public void Draw(Graphics g, Color color, float width)
        {
            g.DrawLines(ResourceFactory.UsePen(color, width), m_geometryData);
        }

        public void OnMouseDown(MouseEventArgs e)
        {
            for (int i = 0; i < 2; i++)
                if (m_handles[i].Contains(e.Location, out m_delta))
                {
                    m_hitIndex = i;
                    break;
                }
        }

        public void OnMouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && m_hitIndex != -1)
            {
                m_handles[m_hitIndex].Position = (Vector2)e.Location - m_delta;
                BuildGeometry();
            }
        }

        public void OnMouseUp(MouseEventArgs e)
        {
            m_hitIndex = -1;
        }
    }
}
