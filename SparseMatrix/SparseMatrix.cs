namespace SparseMatrix
{
    public class MatrixElement
    {
        public int Value { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }

        public MatrixElement NextRow { get; set; } = null;
        public MatrixElement NextColumn { get; set; } = null;

        public MatrixElement(int value, int row, int column) => (Value, Row, Column) = (value, row, column);
        public override string ToString() => $"{Value} ";

    }
    public class SparseMatrix
    {
        MatrixElement[] rowHeads;
        MatrixElement[] columnHeads;
        public int Rows { get; private set; }
        public int Columns { get; private set; }

        public SparseMatrix(int rows, int columns)
        {
            (Rows, Columns) = (rows, columns);
            rowHeads = new MatrixElement[rows];
            columnHeads = new MatrixElement[columns];
        }

        public void AddElement(int value, int row, int col)
        {
            if (value == 0) throw new Exception("Can`t add 0");

            if (row > Rows - 1 || col > Columns - 1) throw new Exception("Can`t add: out of bounds");

            RemoveElement(row, col);

            MatrixElement newElement = new MatrixElement(value, row, col);

            if (rowHeads[row] == null || rowHeads[row].Column > col)
            {
                newElement.NextRow = rowHeads[row];
                rowHeads[row] = newElement;
            }
            else
            {
                MatrixElement current = rowHeads[row];
                while (current.NextRow != null && current.NextRow.Column < col)
                    current = current.NextRow;
                newElement.NextRow = current.NextRow;
                current.NextRow = newElement;
            }

            if (columnHeads[col] == null || columnHeads[col].Row > row)
            {
                newElement.NextColumn = columnHeads[col];
                columnHeads[col] = newElement;
            }
            else
            {
                MatrixElement current = columnHeads[col];
                while (current.NextColumn != null && current.NextColumn.Row < row)
                    current = current.NextColumn;
                newElement.NextColumn = current.NextColumn;
                current.NextColumn = newElement;
            }
        }

        void RemoveElement(int row, int col)
        {
            if (rowHeads[row] != null)
            {
                if (rowHeads[row].Column == col)
                    rowHeads[row] = rowHeads[row].NextRow;
                else
                {
                    MatrixElement current = rowHeads[row];
                    while (current.NextRow != null && current.NextRow.Column != col)
                        current = current.NextRow;
                    if (current.NextRow != null)
                        current.NextRow = current.NextRow.NextRow;
                }
            }

            if (columnHeads[col] != null)
            {
                if (columnHeads[col].Row == row)
                    columnHeads[col] = columnHeads[col].NextColumn;
                else
                {
                    MatrixElement current = columnHeads[col];
                    while (current.NextColumn != null && current.NextColumn.Row != row)
                        current = current.NextColumn;
                    if (current.NextColumn != null)
                        current.NextColumn = current.NextColumn.NextColumn;
                }
            }
        }

        int GetValue(int row, int col)
        {
            MatrixElement current = rowHeads[row];
            while (current != null)
            {
                if (current.Column == col) return current.Value;
                current = current.NextRow;
            }
            return 0;
        }

        public void MultiplyByNumber(int number)
        {
            for (int i = 0; i < Rows; i++)
            {
                MatrixElement current = rowHeads[i];
                while (current != null)
                {
                    current.Value *= number;
                    current = current.NextRow;
                }
            }
        }

        public SparseMatrix Transpose()
        {
            SparseMatrix res = new SparseMatrix(Columns, Rows);
            for (int i = 0; i < Rows; i++)
            {
                MatrixElement current = rowHeads[i];
                while (current != null)
                {
                    res.AddElement(current.Value, current.Column, current.Row);
                    current = current.NextRow;
                }
            }
            return res;
        }

        public SparseMatrix AddMatrix(SparseMatrix other)
        {
            if (Rows != other.Rows || Columns != other.Columns)
                throw new Exception("Need to be same size");

            SparseMatrix res = new SparseMatrix(Rows, Columns);

            for (int i = 0; i < Rows; i++)
            {
                MatrixElement rowAElement = rowHeads[i],
                    rowBElement = other.rowHeads[i];

                while (rowAElement != null || rowBElement != null)
                {
                    if (rowAElement != null && (rowBElement == null || rowAElement.Column < rowBElement.Column))
                    {
                        res.AddElement(rowAElement.Value, rowAElement.Row, rowAElement.Column);
                        rowAElement = rowAElement.NextRow;
                    }
                    else if (rowBElement != null && (rowAElement == null || rowBElement.Column < rowAElement.Column))
                    {
                        res.AddElement(rowBElement.Value, rowBElement.Row, rowBElement.Column);
                        rowBElement = rowBElement.NextRow;
                    }
                    else
                    {
                        res.AddElement(rowAElement.Value + rowBElement.Value, rowAElement.Row, rowAElement.Column);
                        rowAElement = rowAElement.NextRow;
                        rowBElement = rowBElement.NextRow;
                    }
                }
            }
            return res;
        }

        public void Print()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    Console.Write(GetValue(i, j) + " ");
                }
                Console.WriteLine();
            }
        }
    }
}