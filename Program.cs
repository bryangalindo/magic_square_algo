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

        public bool ContainsDistinctPositiveIntegers()
        {            
            int[] points = this.ConvertMatrixToArray();
            Array.Sort(points);

            bool containsDistinctPositiveIntegers = true;

            while (containsDistinctPositiveIntegers)
            {
                for (int i=0; i < Size; i++)
                {
                    int point = points[i];
                    containsDistinctPositiveIntegers = this.IsPositive(point);

                    if ((containsDistinctPositiveIntegers) && (i != Size-1))
                    {
                        containsDistinctPositiveIntegers = this.IsUnique(points, i);
                        if(containsDistinctPositiveIntegers == false)
                        {
                            break;
                        }
                    }
                    else 
                    {
                        return containsDistinctPositiveIntegers;
                    }
                }
            }
            return containsDistinctPositiveIntegers;
        }

        public bool IsMagicSquare()
        {
            bool isMagicSquare = true;

            while (isMagicSquare)
            {
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

                int magicConstant = this.GetMagicConstant();

                int[][] ascendingDiagonalCoordinates = this.GetAscendingDiagonalCoordinates();
                int ascendingDiagonalSum = this.GetDiagonalSum(ascendingDiagonalCoordinates);

                if (ascendingDiagonalSum != magicConstant)
                {
                    return false;
                }

                int[][] descendingDiagonalCoordinates = this.GetDescendingDiagonalCoordinates();
                int descendingDiagonalSum = this.GetDiagonalSum(descendingDiagonalCoordinates);

                if (descendingDiagonalSum != magicConstant)
                {
                    return false;
                }

                int[] allRowSums = this.GetAllRowOrColumnSums(0);

                foreach (int sum in allRowSums)
                {
                    if (sum != magicConstant)
                    {
                        return false;
                    }
                }

                int[] allColumnSums = this.GetAllRowOrColumnSums(1);

                foreach (int sum in allColumnSums)
                {
                    if (sum != magicConstant)
                    {
                        return false;
                    }
                }
                return isMagicSquare;
            }
            return isMagicSquare;
        }

        public bool IsSquare()
        {
            if (Rows == Columns)
            {
                return true;
            }
            else 
            {
                return false;
            }
        }

        public int[] GetAllRowOrColumnSums(int sumType)
        {      
            int[] allSums = new int[this.Rows];

            int sum = 0;

            for (int i=0; i < this.Rows; i++)
            {   
                for (int j=0; j < this.Columns; j++)
                {   
                    if (sumType == 0) 
                    { 
                        sum += this.MatrixObj[i,j]; 
                    }
                    else if (sumType == 1) 
                    { 
                        sum += this.MatrixObj[j,i]; 
                    }
                }
                allSums[i] = sum;
                sum = 0;
            }
            return allSums;
         }  

        public int[][] GetAscendingDiagonalCoordinates()
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

        public int[][] GetDescendingDiagonalCoordinates()
        {
            int[][] descendingDiagonalCoordinates = new int[this.Rows][];

            for (int i = 0; i < this.Rows; i++)
            {
                int[] coordinate = new int[] {i,i};
                descendingDiagonalCoordinates[i] = coordinate;
            }
            return descendingDiagonalCoordinates;
        }

        public int GetDiagonalSum(int[][] diagonalPoints)
        {
            int diagonalSum = 0;

            foreach (int[] point in diagonalPoints)
            {
                diagonalSum += this.MatrixObj[point[0], point[1]];
            }

            return diagonalSum;
        }

        public int GetMagicConstant()
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

        private bool IsPositive(int i)
        {
            if (i <= 0)
            {
                return false;
            }
            else 
            {
                return true;
            }
        }

        private bool IsUnique(int[] array, int i)
        {
            if (array[i] == array[i+1]) 
            {
                return false;
            }
            else 
            {
                return true;
            }
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
    }

    class Program
    {
        static void Main(string[] args)
        {
            int[,] magicMatrix = new int[,] { {2, 7, 6}, {9, 5, 1}, {4, 3, 8} };
            int[,] nonMagicMatrix = new int[,] { {2, 7, 6}, {9, 5, 10}, {4, 3, 8} };

            Matrix magicMatrixObj = new Matrix(magicMatrix);
            bool isMagicSquare = magicMatrixObj.IsMagicSquare();
            System.Console.WriteLine(isMagicSquare);

            Matrix nonMagicMatrixObj = new Matrix(nonMagicMatrix);
            bool isNotMagicSquare = nonMagicMatrixObj.IsMagicSquare();
            System.Console.WriteLine(isNotMagicSquare);

        }
    }
}
