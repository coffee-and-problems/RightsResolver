using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actualizer
{
    
    class Position
    {
        public int[] Departments { get; }
        public string Post { get; }

        public Position(int[] departments, string post)
        {
            Departments = departments;
            Post = post;
        }
    }
}
