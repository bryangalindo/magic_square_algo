using System;

namespace MagicSquare
{
    public class NormalMagicMatrix : MagicMatrix
    {
        public NormalMagicMatrix(int[,] matrix) : base(matrix) {}

        public bool IsNormalMagicMatrix()
        {
            bool isNormalMatrixMagicMatrix = true;
            
            while (isNormalMatrixMagicMatrix)
            {
                bool isNormalMatrix = this.IsNormalMatrix();
                bool isMagicMatrix = base.IsMagicMatrix();

                if(!(isNormalMatrix && isMagicMatrix))
                {
                    isNormalMatrixMagicMatrix = false;
                    break;
                }
                
                break;
            }

            return isNormalMatrixMagicMatrix;
        }

        private bool IsNormalMatrix()
        {
            bool isNormalMatrix = true;

            while (isNormalMatrix)
            { 
                bool isSquare = this.IsSquare();
                bool isDistinctPositiveInRange = this.ContainsUniquePositiveInRangeIntegers();

                if (!(isSquare && isDistinctPositiveInRange))
                {
                    isNormalMatrix = false;
                    break;
                }

                break;
            }

            return isNormalMatrix;
        }

        private bool ContainsUniquePositiveInRangeIntegers()
        {   
            int[] elements = Helper.ConvertMatrixToArray(base.matrix);
            Array.Sort(elements);

            bool containsUniquePositiveInRangeIntegers = true;

            for (int i = 0; i < base.Size; i++)
            {
                int element = elements[i];

                bool isPositive = this.IsPositive(element);   
                bool isUnique = this.IsUnique(i, elements);
                bool isInRange = this.IsInRange(element);

                if (!(isPositive && isUnique && isInRange))
                {
                    containsUniquePositiveInRangeIntegers = false;
                    break;
                }
            }
            return containsUniquePositiveInRangeIntegers;
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
        
        private bool IsSquare()
        {
            bool isSquare = true;

            if(base.numRows != base.numColumns)
            {
                isSquare = false;
            }

            return isSquare;
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