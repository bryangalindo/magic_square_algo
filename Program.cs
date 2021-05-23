namespace MagicSquare
{
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