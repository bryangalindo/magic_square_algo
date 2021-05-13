using System;

namespace MagicSquare
{
    public class Helper
    {
        public static bool IsInMatrixSizeRange(int element, int matrixSize)
        {
            bool isInMatrixSizeRange;
            if (element <= matrixSize)
            {
                isInMatrixSizeRange = true;
            }
            else
            {
                isInMatrixSizeRange = false;
            }

            return isInMatrixSizeRange;
        }

        public static bool IsPositive(int element)
        {
            bool isPositive;
            if (element > 0)
            {
                isPositive = true;
            }
            else 
            {
                isPositive = false;
            }

            return isPositive;
        }

        public static bool IsUnique(int elementIndex, int[] elements)
        {
            bool isUnique = true;
            bool isValidIndex = IsValidIndex(elementIndex, elements.Length);

            if (isValidIndex)
            {
                if (elements[elementIndex] != elements[elementIndex+1])
                {
                    isUnique = true;
                }
                else
                {
                    isUnique = false;
                }
            }
            return isUnique;
        }

        public static bool IsValidIndex(int elementIndex, int arraySize)
        {
            bool isValidIndex;
            if (elementIndex < arraySize - 1)
            {
                isValidIndex = true;
            }
            else
            {
                isValidIndex = false;
            }

            return isValidIndex;
        }

        public static int[] ConvertMatrixToArray(int[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int columns = matrix.GetLength(1);
            int size = rows * columns;

            int[] array = new int[size];
            int counter = 0;

            for (int i=0; i < rows; i++)
            {
                for (int j=0; j < columns; j++)
                {
                    int cellValue = matrix[i,j];
                    array[counter] = cellValue;
                    counter++;
                }
            }
            return array;
        }

        public static bool IsNonEmptyMatrix (int[,] matrix)
        {
            bool isNonEmptyMatrix;
            if (matrix.Length > 0)
            {
                isNonEmptyMatrix = true;
            }
            else
            {
                isNonEmptyMatrix = false;
            }

            return isNonEmptyMatrix;
        }

    }

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
            bool isNormalMagicSquare = true;
            while (isNormalMagicSquare)
            {
                bool isNonEmptyMatrix = Helper.IsNonEmptyMatrix(this.MatrixObj);
                if (!isNonEmptyMatrix)
                {
                    isNormalMagicSquare = false;
                    break;
                }

                int magicConstant = this.GetMagicConstant();

                bool isSquare = this.IsSquare();
                bool isDistinctPositiveInRange = this.ContainsUniquePositiveInRangeIntegers();
                if (!(isSquare && isDistinctPositiveInRange))
                {
                    isNormalMagicSquare = false;
                    break;
                }

                int ascendingDiagonalSum = this.GetDiagonalSum(0);
                int descendingDiagonalSum = this.GetDiagonalSum(1);
                if ((descendingDiagonalSum != magicConstant) || (ascendingDiagonalSum != magicConstant))
                {
                    isNormalMagicSquare = false;
                    break;
                }

                int allRowSum = this.GetSumOfAllRowsOrAllColumns(0);
                int allColumnSum = this.GetSumOfAllRowsOrAllColumns(1);
                if (((allColumnSum / this.Columns) != magicConstant) || ((allRowSum / this.Rows) != magicConstant))
                {
                    isNormalMagicSquare = false;
                    break;
                }
                break;
            }
            return isNormalMagicSquare;
        }

        private bool ContainsUniquePositiveInRangeIntegers()
        {   
            bool containsUniquePositiveInRangeIntegers = true;

            int[] elements = Helper.ConvertMatrixToArray(this.MatrixObj);
            Array.Sort(elements);

            for (int i=0; i < this.Size; i++)
            {
                int element = elements[i];

                bool isPositive = Helper.IsPositive(element);   
                bool isUnique = Helper.IsUnique(i, elements);
                bool isInMatrixSizeRange = Helper.IsInMatrixSizeRange(element, this.Size);

                if (!(isPositive && isUnique && isInMatrixSizeRange))
                {
                    containsUniquePositiveInRangeIntegers = false;
                    break;
                }
                else
                {
                    containsUniquePositiveInRangeIntegers = true;
                }
            }
            return containsUniquePositiveInRangeIntegers;
        }

        private bool IsSquare()
        {
            bool isSquare = (this.Rows == this.Columns) ? true : false;
            return isSquare;
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
    }

    public class Test
    {
        public static int[,] validMatrix = new int[,] { {2, 7, 6}, {9, 5, 1}, {4, 3, 8} };
        public static int[,] emptyMatrix = new int[,] {};
        public static int[,] nonSquareMatrix = new int[,] { {2, 7, 6}, {9, 1, 1} };
        public static int[,] negativeElementInMatrix = new int[,] { {2, 7, 6}, {9, 5, -1}, {4, 3, 8} };
        public static int[,] zeroElementInMatrix = new int[,] { {2, 7, 6}, {9, 5, 0}, {4, 3, 8} };
        public static int[,] nonUniqueElementInMatrix = new int[,] { {2, 7, 6}, {9, 1, 1}, {4, 3, 8} };
        public static int[,] outOfRangeElementInMatrix = new int[,] { {2, 7, 6}, {9, 1, 10}, {4, 3, 8} };

        public static void TestMatrix(int [,] matrix)
        {
            Matrix matrixObj = new Matrix(matrix);
            bool isNormalMagicSquare = matrixObj.IsNormalMagicSquare();
            System.Console.WriteLine(isNormalMagicSquare);
        }

        public static void EmptyMatrixCase()
        {
            TestMatrix(emptyMatrix);
        }

        public static void ValidMatrixCase()
        {
            TestMatrix(validMatrix);
        }

        public static void NegativeElementInMatrixCase()
        {
            TestMatrix(negativeElementInMatrix);
        }

        public static void NonSquareMatrixCase()
        {
            TestMatrix(nonSquareMatrix);
        }

        public static void ZeroElementInMatrix()
        {
            TestMatrix(zeroElementInMatrix);
        }

        public static void NonUniqueElementInMatrix()
        {
            TestMatrix(nonUniqueElementInMatrix);
        }

        public static void OutOfRangeElementInMatrix()
        {
            TestMatrix(outOfRangeElementInMatrix);
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            Test.ValidMatrixCase();
            Test.EmptyMatrixCase();
            Test.NonSquareMatrixCase();
            Test.NegativeElementInMatrixCase();
            Test.NonUniqueElementInMatrix();
            Test.OutOfRangeElementInMatrix();
        }
    }
}