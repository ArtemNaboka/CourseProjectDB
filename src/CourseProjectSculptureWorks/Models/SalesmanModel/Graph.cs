using CourseProjectSculptureWorks.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProjectSculptureWorks.Models.SalesmanModel
{
    public class Graph
    {
        private readonly Vertex[] _vertexList;
        private int _numberOfVertex;
        private int[,] _adjMatrix;
        
        public Graph(List<Location> locations)
        {
            var numberOfLocations = locations.Count;
            _vertexList = new Vertex[numberOfLocations];
            _adjMatrix = new int[numberOfLocations, numberOfLocations];
            for(var i = 0; i < numberOfLocations; i++)
            {
                for (int j = 0; j < numberOfLocations; j++)
                    _adjMatrix[i, j] = Int32.MaxValue;

            }
        }
        
        
        private void addVertex(Location location)
        {
            _vertexList[_numberOfVertex++] = new Vertex(location);
        }
        
        
        public void AddEdge(int start, int end, int weight = int.MaxValue)
        {
            _adjMatrix[start, end] = weight;
            _adjMatrix[end, start] = weight;
        } 
    }
}
