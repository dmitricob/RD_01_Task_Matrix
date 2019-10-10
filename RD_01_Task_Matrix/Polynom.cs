using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RD_01_Task_Matrix
{
    class Polynom
    {
        private List<int> koefs;

        public int Pow
        {
            get;
            private set;
        }

        public Polynom(int power, params int[] koefs)
        {
            if (power < 0)
                throw new ArgumentException("invalid initial polynom power");

            this.Pow = power;
            this.koefs = new List<int>(power + 1);
            for (int i = 0; i < power + 1; i++)
            {
                if(i < koefs.Length)
                    this.koefs.Add(koefs[i]);
                else
                    this.koefs.Add(0);
            }            
        }

        public int GetKoefOfPow(int pow)
        {
            return this.koefs[pow];
        }
        public void Print()
        {
            for (int i = 0; i < this.Pow+1; i++)
            {
                //Debug.Write($"{koefs[i]}x^{i} + ");
                Debug.Write($"{koefs[i]}x^{i} {(i <this.Pow?"+":"")} ");
                //Debug.Write(koefs[i] + " x^" + i + " ");
            }
            Debug.WriteLine("");
        }
        
        public static Polynom operator + (Polynom left, Polynom right)
        {
            var bigestPolynome = left;
            var smallestPolynome = right;

            if(right.Pow > left.Pow)
            {
                bigestPolynome = right;
                smallestPolynome = left;
            }
            List<int> temp = new List<int>();
            for (int i = 0; i < bigestPolynome.Pow + 1; i++)
            {
                int sum = 0;
                if (i < smallestPolynome.Pow + 1)
                    sum += smallestPolynome.GetKoefOfPow(i);
                sum += bigestPolynome.GetKoefOfPow(i);
                temp.Add(sum);
            }
            return new Polynom(bigestPolynome.Pow, temp.ToArray());
        }
        public static Polynom operator -(Polynom left, Polynom right)
        {
            List<int> temp = new List<int>();

            for (int i = 0; i < left.Pow + 1; i++)
            {
                temp.Add(left.GetKoefOfPow(i));
            }

            for (int i = 0; i < right.Pow + 1; i++)
            {
                if (i >= temp.Count)
                    temp.Add(-1 * right.GetKoefOfPow(i));
                else
                    temp[i] -= right.GetKoefOfPow(i);
            }
            return new Polynom(temp.Count-1, temp.ToArray());
        }
        public static Polynom operator * (Polynom left, Polynom right)
        {
            int maxNewPow = left.Pow + right.Pow;
            List<int> temp = new List<int>(maxNewPow);
            for (int i = 0; i < maxNewPow+1; i++)
            {
                temp.Add(0);
            }

            for (int i = 0; i < left.Pow+1; i++)
            {
                for (int j = 0; j < right.Pow+1; j++)
                {
                    var newPow = i + j;
                    temp[newPow] += left.GetKoefOfPow(i) * right.GetKoefOfPow(j);

                }
            }
            return new Polynom(maxNewPow,temp.ToArray());
        }

    }
}
