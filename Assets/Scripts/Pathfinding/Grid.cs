using System;

namespace Logic.Pathfinding
{
    public class Grid<TGridObject>
    {
        private int GridWidth { get; set; }
        private int GridHeight { get; set; }
        private TGridObject[,] GridArray { get; set; }

        public Grid(int width, int height, Func<Grid<TGridObject>, int, int, TGridObject> createGridObject)
        {
            GridWidth = width;
            GridHeight = height;

            GridArray = new TGridObject[GridWidth, GridHeight];

            for (int x = 0; x < GridArray.GetLength(0); x++)
            {
                for (int y = 0; y < GridArray.GetLength(1); y++)
                {
                    GridArray[x, y] = createGridObject(this, x, y);
                }
            }
        }

        public int GetWidth()
        {
            return GridWidth;
        }

        public int GetHeight()
        {
            return GridHeight;
        }

        public void SetGridObject(int width, int height, TGridObject value)
        {
            if (width >= 0 && height >= 0 && width < GridWidth && height < GridHeight)
            {
                GridArray[width, height] = value;
            }
        }

        public TGridObject GetGridObject(int width, int height)
        {
            if (width >= 0 && height >= 0 && width < GridWidth && height < GridHeight)
            {
                return GridArray[width, height];
            }

            return default;
        }
    }
}
