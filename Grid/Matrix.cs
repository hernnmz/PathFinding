
using System.Collections.Generic;

namespace Grid
{
    public class Matrix
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int[,] Data { get; set; }
        public List<Cell> SortedListNode = new List<Cell>();
    }
}
