using System;

namespace MagicSquare
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] matrix = new int[,] { {2, 7, 7}, {9, 5, 1}, {4, 3, 8} };
            bool squareCheck = IsSquare(matrix);
            bool isDistinctPositiveMatrix = ContainsDistinctPositiveIntegers(matrix);

            if (squareCheck == false)
            {
                System.Console.WriteLine("This is not a square matrix. Please try again.");
            }
            else if (isDistinctPositiveMatrix == false)
            {
                System.Console.WriteLine("The matrix does not contain positive or distinct values.");
            }
            else 
            {
                bool check = IsMagicSquare(matrix);
                System.Console.WriteLine(check);
            }

        }

        static bool IsMagicSquare(int[,] matrix)
        {
            bool isMagicSquare = true;
            
            while (isMagicSquare)
            {
                int numRows = matrix.GetLength(0);

                int[][] ascendingDiagonalPoints = GetAscendingDiagonalCoordinates(numRows);
                int[][] descendingDiagonalPoints = GetDescendingDiagonalCoordinates(numRows);

                int ascendingDiagonalSum = GetDiagonalSum(matrix, ascendingDiagonalPoints);
                int descendingDiagonalSum = GetDiagonalSum(matrix, descendingDiagonalPoints);

                var columnSums = GetAllRowOrColumnSums(matrix, 0);
                foreach (var sum in columnSums)
                {
                    if (sum != ascendingDiagonalSum)
                    {
                        isMagicSquare = false;
                        break;
                    }
                }
                break;
            }
            return isMagicSquare;
        }

        static bool IsSquare(int[,] matrix)
        {
            int numRows = matrix.GetLength(0);
            int numColumns = matrix.GetLength(1);

            if (numRows == numColumns)
            {
                return true;
            }
            else{
                return false;
            }
        }

        static bool ContainsDistinctPositiveIntegers(int[,] matrix)
        {
            int numRows = matrix.GetLength(0);
            int numColumns = matrix.GetLength(1);
            int totalIntegers = numRows * numColumns;

            int[] uniqueValues = new int[totalIntegers];

            bool containsDistinctPositiveIntegers = true;

            for (int i=0; i < numRows; i++)
            {   
                for (int j=0; j < numColumns; j++)
                {   
                    int cellValue = matrix[i,j];
                    int checkIndexInArray = Array.IndexOf(uniqueValues, cellValue);

                    uniqueValues[i] = cellValue;

                    if (cellValue <0)
                    {
                        containsDistinctPositiveIntegers = false;
                    }

                    if (checkIndexInArray != -1) 
                    {
                        containsDistinctPositiveIntegers = false;
                    }

                }

            }
            return containsDistinctPositiveIntegers;
             
        }

        static int GetDiagonalSum(int[,] matrix, int[][] diagonalPoints)
        {
            int diagonalSum = 0;

            foreach (int[] point in diagonalPoints)
            {
                diagonalSum += matrix[point[0], point[1]];
            }

            return diagonalSum;
        }

        static int[] GetAllRowOrColumnSums(int[,] matrix, int sumType)
        {   
            int numRows = matrix.GetLength(0);
            int numColumns = matrix.GetLength(1);
            
            int[] allSums = new int[numRows];

            int sum = 0;

            for (int i=0; i < numRows; i++)
            {   
                for (int j=0; j < numColumns; j++)
                {   
                    if (sumType == 0) { sum += matrix[i,j]; }
                    else if (sumType == 1) { sum += matrix[j,i]; }
                    else { System.Console.WriteLine("Enter a valid sum type (e.g., 0, 1)"); }
                }
                allSums[i] = sum;
                sum = 0;
            }
            return allSums;
         }

        static int[][] GetAscendingDiagonalCoordinates(int numRows)
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

        static int[][] GetDescendingDiagonalCoordinates(int numRows)
         {
             int[][] descendingDiagonalCoordinates = new int[numRows][];

             for (int i = 0; i < numRows; i++)
             {
                 int[] coordinate = new int[] {i,i};
                 descendingDiagonalCoordinates[i] = coordinate;
             }
             return descendingDiagonalCoordinates;
         }      
    }
}
