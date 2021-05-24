using System;

namespace MagicSquare
{
    public class NormalMagicMatrix : MagicMatrix
    {
        public NormalMagicMatrix(int[,] matrix) : base(matrix) {}

        public bool IsNormalMagicMatrix()
        {
            bool isNormalMatrix = this.IsNormalMatrix();
            bool isMagicMatrix = base.IsMagicMatrix();
            bool isNormalMatrixMagicMatrix = true;

            if(!(isNormalMatrix && isMagicMatrix))
            {
                isNormalMatrixMagicMatrix = false;
            }

            return isNormalMatrixMagicMatrix;
        }

        private bool IsNormalMatrix()
        {
            bool isNormalMatrix = true;
            bool containsValidElements = this.ContainsUniquePositiveInRangeElements();

            if (!containsValidElements)
            {
                isNormalMatrix = false;
            }

            return isNormalMatrix;
        }

        private bool ContainsUniquePositiveInRangeElements()
        {   
            int[] elements = Helper.ConvertMatrixToArray(base.matrix);
            Array.Sort(elements);

            bool containsUniquePositiveInRangeElements = true;

            for (int i = 0; i < base.Size; i++)
            {
                int element = elements[i];

                bool isPositive = this.IsPositive(element);   
                bool isInRange = this.IsInRange(element);
                bool isUnique = this.IsUnique(i, elements);

                if (!(isPositive && isUnique && isInRange))
                {
                    containsUniquePositiveInRangeElements = false;
                    break;
                }
            }
            return containsUniquePositiveInRangeElements;
        }

        private bool IsInRange(int element)
        {
            bool isInRange = true;

            if (element > base.Size)
            {
                isInRange = false;
            }

            return isInRange;
        }
        
        private bool IsPositive(int element)
        {
            bool isPositive = true;

            if (element <= 0)
            {
                isPositive = false;
            }

            return isPositive;
        }
        
        private bool IsUnique(int elementIndex, int[] elements)
        {
            bool isValidIndex = Helper.IsValidIndex(elementIndex, elements.Length); // Prevents IndexOutOfRangeException error
            bool isUnique = true;

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