namespace MagicSquare 
{
    public class Matrix
    {
        private int descendingDiagonalSum = 0;
        private int ascendingDiagonalSum = 0;
        private int rowSum = 0;
        private int columnSum = 0;
        private int magicConstant;
        private int numRows;
        private int numColumns;
        private int size;
        private int[,] matrixObj;

        public Matrix(int[,] matrix)
        {   
            matrixObj = matrix;
            numRows = matrix.GetLength(0);
            numColumns = matrix.GetLength(1);
            size = numRows * numColumns;
            magicConstant = numRows * (size + 1) / 2;
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

                int ascendingDiagonalSum = GetAscendingDiagonalSum();
                int descendingDiagonalSum = GetDescendingDiagonalSum();
                if ((descendingDiagonalSum != magicConstant) || (ascendingDiagonalSum != magicConstant))
                {
                    isNormalMagicSquare = false;
                    break;
                }

                bool allRowsAllColumnsEqualMagicConstant = AllRowsAllColumnsEqualMagicConstant();
                if (!allRowsAllColumnsEqualMagicConstant)
                {
                    isNormalMagicSquare = false;
                    break;
                }
                break;
            }
            return isNormalMagicSquare;
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
            for (int i = 0; i < numRows; i++)
            {
                for (int j = numColumns - 1; j >= 0;)
                {
                    ascendingDiagonalSum += matrixObj[i, j];
                    j--; // Moved decrement inside the loop to avoid "unreachable code" linter message
                    break;
                }
            }
            return ascendingDiagonalSum;
        } 

        private bool AllRowsAllColumnsEqualMagicConstant()
        {      
            bool allRowsAllColumnsEqualMagicConstant = true;
            for (int i = 0; i < numRows; i++)
            {   
                for (int j = 0; j < numColumns; j++)
                {   
                    rowSum += matrixObj[i,j];
                    columnSum += matrixObj[j,i];
                }

                if ((rowSum != magicConstant) || (columnSum != magicConstant))
                {
                    allRowsAllColumnsEqualMagicConstant = false;
                    break;
                }
                
                rowSum = 0; // Resets sum for next row
                columnSum = 0; // Resets sum for next column
            }
            return allRowsAllColumnsEqualMagicConstant;
         }     
    }
}