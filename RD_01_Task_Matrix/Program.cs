using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace RD_01_Task_Matrix
{
    class Program
    {
        static void Main(string[] args)
        {
            Matrix m1 = new Matrix(2,2,1,1,1,1);
            Matrix m2 = new Matrix(2,2,10,10);
            //m1.Print();
            //m2.Print();
            var m = m1 - m2;
            //m.Print();


            Matrix m3 = new Matrix(3,3,
                1,0,0,
                0,1,0,
                0,0,1);
            Matrix m4 = new Matrix(3,3,
                13,9,7,
                8,7,4,
                6,4,0);
            Matrix mm = m3 * m4;
            
            mm.SerializeToXML("data.xml");
            mm = null;
            mm = Matrix.DeserializeFromXML("data.xml");

            Polynom p1 = new Polynom(
                0,
                20);
            Polynom p2 = new Polynom(
                1,
                10);
            Polynom p3 = new Polynom(
                2);
            p1.Print();
            p2.Print();
            p3.Print();
            var p = p2 * p1 * p3;
            p.Print();

            var pp = p1 + p2;
            var pp1 = p1 - p2;
            var pp2 = p1 * p2;

            //p1.Print();
            //p2.Print();
            //pp.Print();
            //pp1.Print();
            //pp2.Print();
        }
    }
}
 