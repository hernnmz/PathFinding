namespace Algorithms
{
    using Grid;
    using System.Collections.Generic;
    using System.Linq;

    public class LongestRoute
    {
        private readonly List<Node> _openList = new List<Node>();
        private readonly List<Coord> _neighbours;
        private int _nodesCount;
        private int _manhattanDistance;
        private Cell _cellOrigin;
        public string AlgorithmName;
        protected List<Node> Closed;
        protected List<Node> ListClosed;
        protected int Id;
        protected Coord Origin;
        protected Node CurrentNode;
        protected List<Coord> Path;
        private readonly int _rows;
        private readonly int _columns;
        private Matrix _dataMatrix;
        protected int CurrentNodeId;

        public LongestRoute(Matrix dataMatrix)
        {
            AlgorithmName = "Longest Route";
            _neighbours = new List<Coord>();
            _rows = dataMatrix.Rows;
            _columns = dataMatrix.Columns;

            _dataMatrix = new Matrix();
            _dataMatrix = dataMatrix;

            // Put the origin on the open list
            InitializeOrigin();
            _openList.Add(new Node(Id++, null, Origin, 0, 0));
        }

        public Matrix GetDataMatrix()
        {
            return _dataMatrix;
        }

        public List<Node> GetListClosed()
        {
            return ListClosed;
        }

        public void InitializeOrigin()
        {
            Closed = new List<Node>();
            Origin = new Coord(_dataMatrix.SortedListNode[0].Coord.X, _dataMatrix.SortedListNode[0].Coord.Y);
            Id = 1;
            _cellOrigin = _dataMatrix.SortedListNode[0];
            _dataMatrix.SortedListNode.Remove(_cellOrigin);
        }

        public void InitializeOpenList()
        {
            // Put the origin on the open list
            _openList.Add(new Node(Id++, null, Origin, 0, 0));
        }

        public SearchDetails GetPathTick()
        {
            SearchDetails searchDateils = ProcessCurrentNode();
            if (searchDateils != null)
                return searchDateils;

            if (_neighbours.Any())
            {
                var thisNeighbour = _neighbours.First();

                // Get the cost of the current node plus the extra step
                int manhattanDistance = _cellOrigin.Weight - _dataMatrix.Data[thisNeighbour.X, thisNeighbour.Y];
                int countNodes = CurrentNode.G == 0 ? 1 : CurrentNode.G + 1;

                _openList.Add(new Node(Id++, CurrentNode.Id, thisNeighbour, countNodes, manhattanDistance));
                _neighbours.Remove(thisNeighbour);

                ProccessPath(thisNeighbour, countNodes, manhattanDistance);
            }
            else
            {
                CurrentNode = null;
                return GetPathTick();
            }

            return GetSearchDetails();
        }

        private SearchDetails ProcessCurrentNode()
        {
            SearchDetails searchDateils = null;

            if (CurrentNode == null)
            {
                if (!_openList.Any())
                {
                    searchDateils = GetSearchDetails();
                    searchDateils.PathFound = true;
                }
                else
                {
                    // Take the current node off the open list to be examined
                    CurrentNode = _openList.OrderByDescending(x => x.G).First();
                    // Move it to the closed list so it doesn't get examined again
                    _openList.Remove(CurrentNode);
                    Closed.Add(CurrentNode);
                    _neighbours.AddRange(GetNeighbours(CurrentNode));
                }
            }

            return searchDateils;
        }

        private void ProccessPath(Coord neighbour, int countNodes, int manhattanDistance)
        {
            // If the neighbour is the destination
            if (!_neighbours.Any())
            {
                if ((neighbour.X >= 0 && neighbour.Y >= 0 && countNodes > _nodesCount) || (countNodes == _nodesCount && manhattanDistance > _manhattanDistance))
                {
                    _manhattanDistance = manhattanDistance;
                    _nodesCount = countNodes;
                    Path = new List<Coord> { neighbour };
                    CurrentNodeId = CurrentNode.Id;
                    ListClosed = new List<Node>();
                    ListClosed.AddRange(Closed);
                }
            }
        }

        protected SearchDetails GetSearchDetails()
        {
            return new SearchDetails
            {
                Path = Path?.ToArray(),
                LastNode = CurrentNode,
                CountSortedListNode = _dataMatrix.SortedListNode.Count,
                CurrentNodeId = CurrentNodeId
            };
        }

        /// <summary>
        /// Find the coords that are above, below, left, and right of the current cell, assuming they are valid
        /// </summary>
        /// <param name="current"></param>
        /// <returns>The valid coords around the current cell</returns>
        protected virtual IEnumerable<Coord> GetNeighbours(Node current)
        {
            var neighbours = new List<Coord>();
            int coordXMinus = current.Coord.X - 1;
            int coordXAdd = current.Coord.X + 1;
            int coordYMinus = current.Coord.Y - 1;
            int coordYAdd = current.Coord.Y + 1;

            //Left
            if (coordXMinus >= 0 && _dataMatrix.Data[coordXMinus, current.Coord.Y] < _dataMatrix.Data[current.Coord.X, current.Coord.Y])
            {
                neighbours.Add(new Coord(coordXMinus, current.Coord.Y));
            }

            //Right
            if (coordXAdd < _columns && _dataMatrix.Data[coordXAdd, current.Coord.Y] < _dataMatrix.Data[current.Coord.X, current.Coord.Y])
            {
                neighbours.Add(new Coord(coordXAdd, current.Coord.Y));
            }

            //Up
            if (coordYMinus >= 0 && _dataMatrix.Data[current.Coord.X, coordYMinus] < _dataMatrix.Data[current.Coord.X, current.Coord.Y])
            {
                neighbours.Add(new Coord(current.Coord.X, coordYMinus));
            }

            //Down
            if (coordYAdd < _rows && _dataMatrix.Data[current.Coord.X, coordYAdd] < _dataMatrix.Data[current.Coord.X, current.Coord.Y])
            {
                neighbours.Add(new Coord(current.Coord.X, coordYAdd));
            }

            return neighbours.ToArray();
        }

    }
}
