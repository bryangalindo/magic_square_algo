using System;

namespace MagicSquare
{
    public class Helper
    {
        public static int[] ConvertMatrixToArray(int[,] matrix)
        {
            int numRows = matrix.GetLength(0);
            int numColumns = matrix.GetLength(1);
            int size = numRows * numColumns;
            int arrayIndex = 0;

            int[] array = new int[size];

            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < numColumns; j++)
                {
                    int element = matrix[i,j];
                    array[arrayIndex] = element;
                    arrayIndex++;
                }
            }
            return array;
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

        public static int[,] ConvertNullMatrixToEmptyMatrix(int[,] matrix)
        {
            if (matrix == null)
            {
                matrix = new int[,] {};
            }
            return matrix;
        }
    }

    public class Normalize
    {
        public static bool IsNormalMatrix(int[,] matrix)
        {
            bool isNormalMatrix = true;
            while (isNormalMatrix)
            { 
                bool isNonEmptyMatrix = IsNonEmptyMatrix(matrix);
                if (!isNonEmptyMatrix)
                {
                    isNormalMatrix = false;
                    break;
                }

                bool isSquare = IsSquareMatrix(matrix);
                bool isDistinctPositiveInRange = ContainsUniquePositiveInRangeIntegers(matrix);
                if (!(isSquare && isDistinctPositiveInRange))
                {
                    isNormalMatrix = false;
                    break;
                }
                break;
            }
            return isNormalMatrix;
        }

        private static bool ContainsUniquePositiveInRangeIntegers(int[,] matrix)
        {   
            bool containsUniquePositiveInRangeIntegers = true;
            int size = matrix.GetLength(0) * matrix.GetLength(1);

            int[] elements = Helper.ConvertMatrixToArray(matrix);
            Array.Sort(elements);

            for (int i = 0; i < size; i++)
            {
                int element = elements[i];

                bool isPositive = IsPositive(element);   
                bool isUnique = IsUnique(i, elements);
                bool isInRange = IsInRange(element, size);

                if (!(isPositive && isUnique && isInRange))
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

        private static bool IsInRange(int element, int matrixSize)
        {
            bool isInRange;
            if (element <= matrixSize)
            {
                isInRange = true;
            }
            else
            {
                isInRange = false;
            }
            return isInRange;
        }

        private static bool IsNonEmptyMatrix(int[,] matrix)
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
        
        private static bool IsPositive(int element)
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
        
        private static bool IsSquareMatrix(int[,] matrix)
        {
            int numRows = matrix.GetLength(0);
            int numColumns = matrix.GetLength(1);

            bool isSquareMatrix;
            if(numRows == numColumns)
            {
                isSquareMatrix = true;
            }
            else
            {
                isSquareMatrix = false;
            }
            return isSquareMatrix;
        }

        private static bool IsUnique(int elementIndex, int[] elements)
        {
            bool isUnique = true;
            bool isValidIndex = Helper.IsValidIndex(elementIndex, elements.Length);

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
    }

    public class Matrix
    {
        private int magicConstant = 0;
        private int descendingDiagonalSum = 0;
        private int ascendingDiagonalSum = 0;
        private int numRows;
        private int numColumns;
        private int size;
        private int[,] matrixObj;

        public Matrix(int[,] matrix)
        {   
            matrix = Helper.ConvertNullMatrixToEmptyMatrix(matrix);
            matrixObj = matrix;
            numRows = matrix.GetLength(0);
            numColumns = matrix.GetLength(1);
            size = numRows * numColumns;
        }

        public bool IsNormalMagicSquare()
        {
            bool isNormalMagicSquare = true;
            while (isNormalMagicSquare)
            { 
                bool isNormalMatrix = Normalize.IsNormalMatrix(matrixObj);
                if (!isNormalMatrix)
                {
                    isNormalMagicSquare = false;
                    break;
                }

                int magicConstant = GetMagicConstant();

                int ascendingDiagonalSum = GetAscendingDiagonalSum();
                int descendingDiagonalSum = GetDescendingDiagonalSum();
                if ((descendingDiagonalSum != magicConstant) || (ascendingDiagonalSum != magicConstant))
                {
                    isNormalMagicSquare = false;
                    break;
                }

                int allRowsum = GetSumOfAllRowsOrAllColumns(0);
                int allColumnSum = GetSumOfAllRowsOrAllColumns(1);
                if (((allColumnSum / numColumns) != magicConstant) || ((allRowsum / numRows) != magicConstant))
                {
                    isNormalMagicSquare = false;
                    break;
                }
                break;
            }
            return isNormalMagicSquare;
        }

        private int[][] GetAscendingDiagonalCoordinates()
        {
            int[][] ascendingDiagonalCoordinates = new int[numRows][];
            int j = numRows - 1;

            for (int i = 0; i < numRows; i++)
            {
                int[] coordinate = new int[] {i,j};
                ascendingDiagonalCoordinates[i] = coordinate;
                j--;
            }
            return ascendingDiagonalCoordinates;
        }

        private int GetDescendingDiagonalSum()
        {

            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < numColumns; j++)
                {
                    if (i == j)
                    {
                        descendingDiagonalSum += matrixObj[i, j];
                    }
                }
            }
            return descendingDiagonalSum;
        }

        private int GetAscendingDiagonalSum()
        {
            int[][] diagonalCoordinates = GetAscendingDiagonalCoordinates();

            foreach (int[] coordinate in diagonalCoordinates)
            {
                ascendingDiagonalSum += matrixObj[coordinate[0], coordinate[1]];
            }
            return ascendingDiagonalSum;
        }

        private int GetMagicConstant()
        {
            for (int i = 0; i < numColumns; i++)
            {
                int element = matrixObj[0, i];
                magicConstant += element;
            }
            return magicConstant;
        }

        private int GetSumOfAllRowsOrAllColumns(int sumType)
        {      
            int sum = 0;
            for (int i = 0; i < numRows; i++)
            {   
                for (int j = 0; j < numColumns; j++)
                {   
                    sum = (sumType == 0) ? sum + matrixObj[i,j] : sum + matrixObj[j,i];
                }
            }
            return sum;
         }     
    }

    public class Test
    {
        public static int[,] validMatrix = new int[,] { {2, 7, 6}, {9, 5, 1}, {4, 3, 8} };
        public static int[,] emptyMatrix = new int[,] {};
        public static int[,] nullMatrix = null;
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

        public static void NullMatrixCase()
        {
            TestMatrix(nullMatrix);
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

        public static void NonUniqueElementInMatrixCase()
        {
            TestMatrix(nonUniqueElementInMatrix);
        }

        public static void OutOfRangeElementInMatrixCase()
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
            Test.NullMatrixCase();
            Test.NonSquareMatrixCase();
            Test.NegativeElementInMatrixCase();
            Test.NonUniqueElementInMatrixCase();
            Test.OutOfRangeElementInMatrixCase();
        }
    }
}