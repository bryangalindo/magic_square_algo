using System;

namespace MagicSquare
{
    public class Matrix
    {
        public int Rows { get; }
        public int Columns { get; }
        public int Size { get; }
        public int[,] MatrixObj { get; }

        public Matrix(int[,] matrix)
        {
            MatrixObj = matrix;
            Rows = matrix.GetLength(0);
            Columns = matrix.GetLength(1);
            Size = Rows * Columns;
        }

        public bool IsNormalMagicSquare()
        {
            int magicConstant = this.GetMagicConstant();

            bool isSquare = this.IsSquare();
            if (isSquare == false)
            {
                return false;
            }

            bool isDistinctPositive = this.ContainsDistinctPositiveIntegers();
            if (isDistinctPositive == false)
            {
                return false;
            }

            int ascendingDiagonalSum = this.GetDiagonalSum(0);
            if (ascendingDiagonalSum != magicConstant)
            {
                return false;
            }

            int descendingDiagonalSum = this.GetDiagonalSum(1);
            if (descendingDiagonalSum != magicConstant)
            {
                return false;
            }

            int allRowSum = this.GetSumOfAllRowsOrAllColumns(0);
            if ((allRowSum / this.Rows) != magicConstant)
            {
                return false;
            }

            int allColumnSum = this.GetSumOfAllRowsOrAllColumns(1);
            if ((allColumnSum / this.Columns) != magicConstant)
            {
                return false;
            }

            return true;
        }

        private bool ContainsDistinctPositiveIntegers()
        {            
            int[] points = this.ConvertMatrixToArray();
            Array.Sort(points);

            for (int i=0; i < Size - 1; i++)
            {
                int point = points[i];

                bool isPositive = this.IsPositive(point);
                if (isPositive == false)
                {
                    return false;
                }
                
                bool isUnique = this.IsUnique(points, i);
                if(isUnique == false)
                {
                    return false;
                }
            }

            return true;
        }

        private int[] ConvertMatrixToArray()
        {
            int[] array = new int[this.Size];
            int counter = 0;

            for (int i=0; i < this.Rows; i++)
            {
                for (int j=0; j < this.Columns; j++)
                {
                    int cellValue = this.MatrixObj[i,j];
                    array[counter] = cellValue;
                    counter++;
                }
            }

            return array;
        }

        private bool IsPositive(int i)
        {
            bool isPositive = (i > 0) ? true : false;
            return isPositive;
        }

        private bool IsSquare()
        {
            bool isSquare = (this.Rows == this.Columns) ? true : false;
            return isSquare;
        }

        private bool IsUnique(int[] array, int i)
        {
            bool isUnique = (array[i] != array[i+1]) ? true : false;
            return isUnique;
        }

        private int GetSumOfAllRowsOrAllColumns(int sumType)
        {      
            int sum = 0;

            for (int i=0; i < this.Rows; i++)
            {   
                for (int j=0; j < this.Columns; j++)
                {   
                    sum = (sumType == 0) ? sum + this.MatrixObj[i,j] : sum +this.MatrixObj[j,i];
                }
            }

            return sum;
         }  

        private int[][] GetAscendingDiagonalCoordinates()
        {
            int[][] ascendingDiagonalCoordinates = new int[this.Rows][];
            int j = Rows - 1;

            for (int i = 0; i < Rows; i++)
            {
                int[] coordinate = new int[] {i,j};
                ascendingDiagonalCoordinates[i] = coordinate;
                j--;
            }
            return ascendingDiagonalCoordinates;
        }

        private int[][] GetDescendingDiagonalCoordinates()
        {
            int[][] descendingDiagonalCoordinates = new int[this.Rows][];

            for (int i = 0; i < this.Rows; i++)
            {
                int[] coordinate = new int[] {i,i};
                descendingDiagonalCoordinates[i] = coordinate;
            }
            return descendingDiagonalCoordinates;
        }

        private int GetDiagonalSum(int diagonalType)
        {
            int diagonalSum = 0;
            int[][] diagonalPoints = (diagonalType == 0) ? this.GetAscendingDiagonalCoordinates() : this.GetDescendingDiagonalCoordinates();

            foreach (int[] point in diagonalPoints)
            {
                diagonalSum += this.MatrixObj[point[0], point[1]];
            }

            return diagonalSum;
        }

        private int GetMagicConstant()
        {
            int i = 0;
            int sum = 0;

            for (int j=0; j < this.Columns; j++)
            {
                int cellValue = this.MatrixObj[i,j];
                sum += cellValue;
            }

            return sum;
        }    
    }

    class Program
    {
        static void Main(string[] args)
        {
            int[,] matrix = new int[,] { {2, 7, 6}, {9, 5, 1}, {4, 3, 8} };
            // int[,] matrix = new int[,] { {2, 7, 6}, {9, 5, 10}, {4, 3, 8} };
            // int[,] matrix = new int[,] {
            //     {1, 35, 34, 3, 32, 6},
            //     {30, 8, 28, 27, 11, 7},
            //     {24, 23, 15, 16, 14, 19},
            //     {13, 17, 21, 22, 20, 18},
            //     {12, 26, 9, 10, 29, 25},
            //     {31, 2, 4, 33, 5, 36},
            // };

            Matrix matrixObj = new Matrix(matrix);
            bool isNormalMagicSquare = matrixObj.IsNormalMagicSquare();
            System.Console.WriteLine(isNormalMagicSquare);
        }
    }
}
