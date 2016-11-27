using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProjectSculptureWorks.Models.GraphModel
{
    public class Edge
    {
        public int SrcVert { get; set; }   
        public int DestVert { get; set; }  
        public int Distance { get; set; }  
                              
        public Edge(int sv, int dv, int d)  
        {
            SrcVert = sv;
            DestVert = dv;
            Distance = d;
        }
    }
}
