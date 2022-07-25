namespace Algorithms
{
    using Grid;
    using System.Collections.Generic;

    public class SearchDetails
    {
        public bool PathFound { get; set; }
        public Coord[] Path { get; set; }
        public Node LastNode { get; set; }
        public int CurrentNodeId { get; set; }
        public int CountSortedListNode { get; set; }
    }
}
