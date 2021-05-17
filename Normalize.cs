using System;

namespace MagicSquare
{
    public class Normalize
    {
        public static bool IsNormalMatrix(int[,] matrix)
        {
            bool isNormalMatrix = true;
            while (isNormalMatrix)
            { 
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
            int size = matrix.GetLength(0) * matrix.GetLength(1);
            int[] elements = Helper.ConvertMatrixToArray(matrix);
            Array.Sort(elements);

            bool containsUniquePositiveInRangeIntegers = true;
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
            }
            return containsUniquePositiveInRangeIntegers;
        }

        private static bool IsInRange(int element, int matrixSize)
        {
            bool isInRange = true;
            if (element > matrixSize)
            {
                isInRange = false;
            }
            return isInRange;
        }
        
        private static bool IsPositive(int element)
        {
            bool isPositive = true;
            if (element <= 0)
            {
                isPositive = false;
            }
            return isPositive;
        }
        
        private static bool IsSquareMatrix(int[,] matrix)
        {
            int numRows = matrix.GetLength(0);
            int numColumns = matrix.GetLength(1);

            bool isSquareMatrix = true;
            if(numRows != numColumns)
            {
                isSquareMatrix = false;
            }
            return isSquareMatrix;
        }

        private static bool IsUnique(int elementIndex, int[] elements)
        {
            bool isUnique = true;
            bool isValidIndex = Helper.IsValidIndex(elementIndex, elements.Length); // Prevents IndexOutOfRangeException error

            if (isValidIndex)
            {
                if (elements[elementIndex] == elements[elementIndex + 1])
                {
                    isUnique = false;
                }
            }
            return isUnique;
        }
    }
}