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
            bool isValidIndex = true;
            if (elementIndex >= arraySize - 1)
            {
                isValidIndex = false;
            }
            return isValidIndex;
        }

        public static bool IsNonEmptyNonNullMatrix(int[,] matrix)
        {
            bool isNonEmptyNonNullMatrix = true;
            if (matrix == null || matrix.Length == 0)
            {
                isNonEmptyNonNullMatrix = false;
            }
            return isNonEmptyNonNullMatrix;
        }
    }
}