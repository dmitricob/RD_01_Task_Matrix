using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RD_01_Task_Matrix
{
    class MatrixException : Exception
    {
        public MatrixException() : base()
        {

        }
        public MatrixException(string str) : base(str)
        {

        }

        public MatrixException(string str, Exception inner) : base(str, inner)
        {

        }
    }
}
