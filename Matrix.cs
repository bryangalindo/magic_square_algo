namespace MagicSquare 
{
    public class Matrix
    {
        internal int numRows;
        internal int numColumns;
        internal int Size;
        internal int[,] matrix;

        public Matrix(int[,] matrix)
        {
            this.matrix = matrix;
            this.numRows = matrix.GetLength(0);
            this.numColumns = matrix.GetLength(1);
            this.Size = this.numRows * this.numColumns;
        }

        public bool IsSquareMatrix()
        {
            bool isSquareMatrix = true;

            if (this.numRows != this.numColumns)
            {
                isSquareMatrix = false;
            }
            
            return isSquareMatrix;
        }
    }
}