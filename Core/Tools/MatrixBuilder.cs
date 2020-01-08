namespace Core.Tools
{
    public static class MatrixBuilder
    {
        public static Matrix<char> BuildCharMatrix(string input)
        {
            var matrix = new Matrix<char>();
            var rows = input.Trim().Split('\n');
            var y = 0;
            foreach (var row in rows)
            {
                var x = 0;
                var chars = row.Trim().ToCharArray();
                foreach (var c in chars)
                {
                    matrix.MoveTo(x, y);
                    matrix.WriteValue(c);
                    x += 1;
                }

                y += 1;
            }

            return matrix;
        }
    }
}