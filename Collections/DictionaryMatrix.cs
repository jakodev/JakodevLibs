using System;
using System.Collections.Generic;
using System.Text;

namespace JakodevLibs.Collections
{
    public class DictionaryMatrix<TRow, TCol, TValue>
    {

        internal readonly Dictionary<TRow, Dictionary<TCol, TValue>> _matrix = new Dictionary<TRow, Dictionary<TCol, TValue>>();

        private List<TRow> _rowsHeader = new List<TRow>();
        private List<TCol> _colsHeader = new List<TCol>();

        public Dictionary<TRow, Dictionary<TCol, TValue>> TableMatrix
        {
            get { return _matrix; }
        }

        public List<TRow> RowsHeader
        {
            get { return _rowsHeader; }
            private set { }
        }

        public List<TCol> ColsHeader
        {
            get { return _colsHeader; }
            private set { }
        }

        private void _addRow(TRow row)
        {
            if (!_rowsHeader.Contains(row))
            {
                _rowsHeader.Add(row);
            }
        }

        private void _addColumn(TCol col)
        {
            if (!_colsHeader.Contains(col))
            {
                _colsHeader.Add(col);
            }
        }

        public void Add(TRow row, TCol col, TValue value)
        {
            
            if (!_matrix.ContainsKey(row))
            {
                
                Dictionary<TCol, TValue> valueDictionary = new Dictionary<TCol, TValue>();
                valueDictionary.Add(col, value);

                _matrix.Add(row, valueDictionary);

                _addRow(row);
                _addColumn(col);

            }
            else
            {
                Dictionary<TCol, TValue> colDictionary = _matrix[row];
                colDictionary.Add(col, value);

                _matrix[row] = colDictionary;

                _addColumn(col);
            }

        }

        public TValue GetValue(TRow row, TCol col)
        {

            if (_matrix.ContainsKey(row))
            {
                Dictionary<TCol, TValue> rowDictionary = _matrix[row];

                if (rowDictionary.ContainsKey(col))
                {
                    return rowDictionary[col];
                }
            }

            return default(TValue);
        }

    }
}
