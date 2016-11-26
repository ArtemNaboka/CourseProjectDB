using CourseProjectSculptureWorks.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProjectSculptureWorks.Models.GraphModel
{
    public class Graph
    {
        private readonly Vertex[] _vertexList;
        private int _numberOfVertex;
        private int[,] _adjMatrix;

        public Graph(int n)
        {
            _numberOfVertex = 0;
            _adjMatrix = new int[n, n];
            for (int j = 0; j < n; j++)
            {
                for (int k = 0; k < n; k++)
                {
                    _adjMatrix[j, k] = 0;
                }
            }
        }


        public void AddVertex(Location location)
        {
            _vertexList[_numberOfVertex++] = new Vertex(location);
        }


        public void AddEdge(int start, int end, int weight)
        {
            _adjMatrix[start, end] = weight;
            _adjMatrix[end, start] = weight;
        }


        private int getUnvisitedVertex(int vertexNumber)
        {
            for (int j = 0; j < _numberOfVertex; j++)
                if (_adjMatrix[vertexNumber, j] != 0 && _vertexList[j].wasVisited == false)
                    return j;
            return -1;
        }
    }
}
