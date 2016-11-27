using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProjectSculptureWorks.Models.GraphModel
{
    public class PriorityQ
    {
        private const int SIZE = 20;
        private Edge[] _queArray;
        private int _size;

        public PriorityQ()            
        {
            _queArray = new Edge[SIZE];
            _size = 0;
        }

        public void Insert(Edge item)  
        {
            int j;

            for (j = 0; j < _size; j++)          
                if (item.Distance >= _queArray[j].Distance)
                    break;

            for (int k = _size - 1; k >= j; k--)    
                _queArray[k + 1] = _queArray[k];

            _queArray[j] = item;
            _size++;
        }

        public Edge RemoveMin() => _queArray[--_size];           


        public void RemoveN(int n)         
        {
            for (int j = n; j < _size - 1; j++)     
                _queArray[j] = _queArray[j + 1];
            _size--;
        }


        public Edge PeekMin() => _queArray[_size - 1];


        public int Size() => _size;


        public bool IsEmpty()  => (_size == 0);    


        public Edge PeekN(int n) => _queArray[n];


        public int Find(int findDex)  
        {                          
            for (int j = 0; j < _size; j++)
                if (_queArray[j].DestVert == findDex)
                    return j;
            return -1;
        }
    }
}
