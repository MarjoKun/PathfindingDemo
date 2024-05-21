namespace Logic.Pathfinding
{
    public class PathNode
    {
        private Grid<PathNode> PathNodeGrid { get; set; }
        public int nodeWidth { get; private set; }
        public int nodeHeight { get; private set; }

        public int GCost { get; set; }
        public int HCost { get; set; }
        public int FCost { get; set; }

        public bool IsObstacle { get; set; }
        public PathNode CameFromNode { get; set; }

        public PathNode(Grid<PathNode> pathNodeGrid, int width, int height)
        {
            PathNodeGrid = pathNodeGrid;
            nodeWidth = width;
            nodeHeight = height;
            IsObstacle = false;
        }

        public void CalculateFCost()
        {
            FCost = GCost + HCost;
        }
    }
}
