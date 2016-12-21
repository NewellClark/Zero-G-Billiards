using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Codonbyte.SpaceBilliards.Arena.Balls
{
    internal class VerticeTetrahedron
    {
        private static readonly Vector3 downOneLayer = new Vector3(0, -.5f * Mathf.Sqrt(3), .5f);

        public VerticeTetrahedron(int numLayers, float cellRadius, Vector3 top)
        {
            CellRadius = cellRadius;
            Top = top;
            NumLayers = numLayers;

            for (int layer = 0; layer < NumLayers; layer++)
            {
                Vector3 tip = top + 2 * cellRadius * layer * downOneLayer;
                var triangle = new VerticeTriangle(layer + 1, cellRadius, tip);
                vertices.AddRange(triangle.Vertices);
                layers.Add(triangle);
            }
        }

        public float CellRadius { get; private set; }
        public Vector3 Top { get; private set; }
        public int NumLayers { get; private set; }
        public float Height
        {
            get
            {
                return Mathf.Abs(NumLayers * downOneLayer.y * CellRadius * 2);
            }
        }

        public IList<Vector3> Vertices { get { return vertices.AsReadOnly(); } }
        public IList<VerticeTriangle> Layers { get { return layers.AsReadOnly(); } }

        private List<Vector3> vertices = new List<Vector3>();
        private List<VerticeTriangle> layers = new List<VerticeTriangle>();
    }

    internal class VerticeTriangle
    {
        private static readonly Vector3 backOneRow = new Vector3(-.5f, 0, -.5f * Mathf.Sqrt(3));
        private static readonly Vector3 oneVerticeRight = new Vector3(1, 0, 0);

        public VerticeTriangle(int numRows, float cellRadius, Vector3 tip)
        {
            this.CellRadius = cellRadius;
            this.Tip = tip;
            this.NumRows = numRows;

            for (int row = 0; row < numRows; row++)
            {
                for (int cellInRow = 0; cellInRow <= row; cellInRow++)
                {
                    var vertice = tip + 2 * CellRadius * (row * backOneRow + cellInRow * oneVerticeRight);
                    vertices.Add(vertice);
                }
            }
        }

        public float CellRadius { get; private set; }
        public Vector3 Tip { get; private set; }
        public int NumRows { get; private set; }

        public Vector3 this[int index]
        {
            get { return vertices[index]; }
        }

        public IList<Vector3> Vertices
        {
            get { return vertices.AsReadOnly(); }
        }

        private List<Vector3> vertices = new List<Vector3>();

    }
}
