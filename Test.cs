namespace MagicSquare
{
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

        public static void TestMatrix(int[,] matrix)
        {
            bool isNonNullNonEmptyMatrix = Helper.IsNonEmptyNonNullMatrix(matrix);
            if (isNonNullNonEmptyMatrix)
            {
                Matrix matrixObj = new Matrix(matrix);
                bool isNormalMagicSquare = matrixObj.IsNormalMagicSquare();
                System.Console.WriteLine(isNormalMagicSquare);
            }
            else
            {
                System.Console.WriteLine("Matrix is either null or empty. Please submit a valid matrix.");
            }
        }

        public static void EmptyMatrixCase() { TestMatrix(emptyMatrix); }
        public static void ValidMatrixCase() { TestMatrix(validMatrix); }
        public static void NullMatrixCase() { TestMatrix(nullMatrix); }
        public static void NegativeElementInMatrixCase() { TestMatrix(negativeElementInMatrix); }
        public static void NonSquareMatrixCase() { TestMatrix(nonSquareMatrix); }
        public static void ZeroElementInMatrix() { TestMatrix(zeroElementInMatrix); }
        public static void NonUniqueElementInMatrixCase() { TestMatrix(nonUniqueElementInMatrix); }
        public static void OutOfRangeElementInMatrixCase() { TestMatrix(outOfRangeElementInMatrix); }
    }  
}