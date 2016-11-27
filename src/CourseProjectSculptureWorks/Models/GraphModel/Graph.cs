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
        private int _currentVert;
        private int _nTree;
        private int[,] _adjMatrix;
        private PriorityQ _thePQ;

        public Graph(List<Location> locations)
        {
            var n = locations.Count;
            _vertexList = new Vertex[n];
            _numberOfVertex = 0;
            _adjMatrix = new int[n, n];
            for (int j = 0; j < n; j++)
            {
                for (int k = 0; k < n; k++)
                    _adjMatrix[j, k] = Int32.MaxValue;
                _thePQ = new PriorityQ();
                AddVertex(locations[j]);
            }
        }


        public void AddVertex(Location location)
        {
            _vertexList[_numberOfVertex++] = new Vertex(location);
        }


        public void AddEdge(int start, int end, int weight = 1)
        {
            _adjMatrix[start, end] = weight;
            _adjMatrix[end, start] = weight;
        }


        public List<Location> MstW()           
        {
            var resultList = new List<Location>();
            _currentVert = 0;          

            while (_nTree < _numberOfVertex - 1)   
            {                     
                _vertexList[_currentVert].IsInTree = true;
                _nTree++;

                for (int j = 0; j < _numberOfVertex; j++)  
                {
                    if (j == _currentVert)         
                        continue;
                    if (_vertexList[j].IsInTree) 
                        continue;
                    int distance = _adjMatrix[_currentVert, j];
                    if (distance == Int32.MaxValue)  
                        continue;
                    putInPQ(j, distance);     
                }
                if (_thePQ.Size() == 0)         
                {
                    throw new Exception("GRAPH IS NOT CONNECTED");
                }

                Edge theEdge = _thePQ.RemoveMin();
                int sourceVert = theEdge.SrcVert;
                _currentVert = theEdge.DestVert;
                resultList.Add(_vertexList[sourceVert].Location);
                Console.Write(_vertexList[sourceVert].Location.LocationName);
                Console.Write(_vertexList[_currentVert].Location.LocationName);
                Console.Write(" ");
            }  

            //for (int j = 0; j < _numberOfVertex; j++)     
            //    _vertexList[j].IsInTree = false;
            return resultList;
        }


        public void putInPQ(int newVert, int newDist)
        {
            int queueIndex = _thePQ.Find(newVert);
            if (queueIndex != -1)              
            {
                Edge tempEdge = _thePQ.PeekN(queueIndex);  
                int oldDist = tempEdge.Distance;
                if (oldDist > newDist)          
                {
                    _thePQ.RemoveN(queueIndex);  
                    Edge theEdge =
                                new Edge(_currentVert, newVert, newDist);
                    _thePQ.Insert(theEdge);      
                }
            }  
            else  
            {                              
                Edge theEdge = new Edge(_currentVert, newVert, newDist);
                _thePQ.Insert(theEdge);
            }
        }
    }
}
