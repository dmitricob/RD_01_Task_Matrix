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
    [Serializable()]
    class Matrix : ICloneable
    {
        private List<List<int>> matrixData = new List<List<int>>();

        private int columns;
        public int Columns
        {
            get { return columns; }
            private set { columns = value; }
        }
        public int Rows
        {
            get;
            private set;
        }


        public Matrix(int rows, int columns)
        {
            if (columns < 1 || rows < 1)
                throw new MatrixException("invalide initial matrix sizes");
            this.Columns = columns;
            this.Rows = rows;
            for (int i = 0; i < rows; i++)
            {
                matrixData.Add(new List<int>(columns));
                for (int j = 0; j < columns; j++)
                {
                    matrixData[i].Add(0);
                }
            }
            //matrixData.
        }
        public Matrix(int rows, int columns, params int[] matrixValues ) : this(rows,columns)
        {            
            for (int i = 0; i < matrixValues.Length && i < rows * columns; i++)
            {
                    matrixData[i / columns][i % columns] = matrixValues[i];
            }
        }
        public Matrix(int rows, int columns, List<List<int>> matrixData) : this(rows,columns)
        {
            this.matrixData = new List<List<int>>(matrixData);
        }
        

        public int GetAt(int row, int column) {
            return matrixData[row][column];
        }
        public void SetAt(int x, int y, int data)
        {
            matrixData[x][y] = data;
        }


        public static Matrix operator +( Matrix left , Matrix right)
        {
            if (left.Rows != right.Rows || left.Columns != right.Columns)
                throw new MatrixException("sizes missmatch");

            int rows = left.Rows;
            int columns = left.Columns;

            List<int> tempData = new List<int>();
            for (int i = 0; i < rows * columns; i++)
                tempData.Add(left.GetAt(i / columns,i % columns) + right.GetAt(i / columns,i % columns));
           
            return new Matrix(columns, rows, tempData.ToArray());

        }
        public static Matrix operator -( Matrix left , Matrix right)
        {
            if (left.Rows != right.Rows || left.Columns != right.Columns)
                throw new MatrixException("sizes missmatch");

            int rows = left.Rows;
            int columns = left.Columns;

            List<int> tempData = new List<int>();
            for (int i = 0; i < rows * columns; i++)
                tempData.Add(left.GetAt(i / columns,i % columns) - right.GetAt(i / columns,i % columns));
            
            return new Matrix(columns, rows, tempData.ToArray());
        }
        public static Matrix operator *( Matrix left , int right)
        {
            int rows = left.Rows;
            int columns = left.Columns;

            List<int> tempData = new List<int>();
            for (int i = 0; i < rows * columns; i++)
                tempData.Add(left.GetAt(i / columns,i % columns) * right);

            return new Matrix(columns, rows, tempData.ToArray());
        }
        public static Matrix operator *(Matrix left, Matrix right)
        {
            int rowsL = left.Rows;
            int columnsL = left.Columns;
            int rowsR = right.Rows;
            int columnsR = right.Columns;

            if (columnsL != rowsR)
                throw new MatrixException("sizes missmatch");

            List<int> tempData = new List<int>();
            for (int i = 0; i < rowsL; i++)
            {
                for (int j = 0; j < columnsR; j++)
                {
                    int sum = 0;
                    for (int k = 0; k < columnsL; k++)
                        sum += left.GetAt(i, k) * right.GetAt(k,j);
                    tempData.Add(sum);                    
                }
            }
            return new Matrix(rowsL, columnsR, tempData.ToArray());
        }


        public object Clone() {
            return new Matrix(this.Rows, this.Columns, matrixData);
        }

        public void SerializeToXML(string name)
        {
            Stream stream = File.Open(name, FileMode.Create);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, this);
            stream.Close();
        }
        public static Matrix DeserializeFromXML(string name)
        {
            Stream stream = File.Open(name, FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            var deserializedObg = (Matrix)formatter.Deserialize(stream);
            stream.Close();
            return deserializedObg;
        }

        public void Print()
        {
            for (int i = 0; i < this.Rows; i++)
            {
                for (int j = 0; j < this.Columns; j++)
                {
                    Debug.Write(matrixData[i][j] + " ");
                }
                Debug.WriteLine("");
            }
            Debug.WriteLine("");
        }

    }
}
