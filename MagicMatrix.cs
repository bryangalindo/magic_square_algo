namespace MagicSquare 
{
    public class MagicMatrix : Matrix
    {
        internal int descendingDiagonalSum = 0;
        internal int ascendingDiagonalSum = 0;
        internal int rowSum = 0;
        internal int columnSum = 0;
        internal int magicConstant;

        public MagicMatrix(int[,] matrix) : base(matrix)
        {
            magicConstant = base.numRows * (base.Size + 1) / 2;
        }

        public bool IsMagicMatrix()
        {
            bool isMagicMatrix = true;
            
            while (isMagicMatrix)
            { 
                int ascendingDiagonalSum = this.GetAscendingDiagonalSum();
                int descendingDiagonalSum = this.GetDescendingDiagonalSum();
                bool ContainsRowsColumnsEqualToMagicConstant = this.ContainsRowsColumnsEqualToMagicConstant();

                if ((descendingDiagonalSum != magicConstant) || (ascendingDiagonalSum != magicConstant) || (!ContainsRowsColumnsEqualToMagicConstant))
                {
                    isMagicMatrix = false;
                    break;
                }

                break;
            }

            return isMagicMatrix;
        }

        private int GetDescendingDiagonalSum()
        {
            for (int i = 0; i < base.numRows; i++)
            {
                for (int j = 0; j < base.numColumns; j++)
                {
                    if (i == j)
                    {
                        descendingDiagonalSum += base.matrix[i, j];
                    }
                }
            }
            return descendingDiagonalSum;
        }

        private int GetAscendingDiagonalSum()
        {
            for (int i = 0; i < base.numRows; i++)
            {
                for (int j = base.numColumns - 1; j >= 0;)
                {
                    ascendingDiagonalSum += base.matrix[i, j];
                    j--; // Moved decrement inside the loop to avoid "unreachable code" linter message
                    break;
                }
            }
            return ascendingDiagonalSum;
        } 

        private bool ContainsRowsColumnsEqualToMagicConstant()
        {      
            bool ContainsRowsColumnsEqualToMagicConstant = true;
            for (int i = 0; i < base.numRows; i++)
            {   
                for (int j = 0; j < base.numColumns; j++)
                {   
                    rowSum += base.matrix[i,j];
                    columnSum += base.matrix[j,i];
                }

                bool checkSumsEqualMagicConstant = Helper.CheckSumsEqualToConstant(rowSum, columnSum, magicConstant);
                if (!checkSumsEqualMagicConstant)
                {
                    ContainsRowsColumnsEqualToMagicConstant = false;
                    break;
                }

                rowSum = 0; // Resets sum for next row
                columnSum = 0; // Resets sum for next column
            }
            return ContainsRowsColumnsEqualToMagicConstant;
         }
    }
}