using System;
using System.Drawing;
using System.Windows.Forms;

namespace TetrisProject
{
    public class Grid : Panel
    {
        private const int cellSize = 30;
        private const int columns = 10;
        private const int rows = 20;
        private Brush[,] grid;

        public Grid()
        {
            this.Width = columns * cellSize;
            this.Height = rows * cellSize;
            this.BackColor = Color.Black;
            grid = new Brush[columns, rows];
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawGrid(e.Graphics);
            DrawBlocks(e.Graphics);
        }

        private void DrawGrid(Graphics g)
        {
            Pen pen = new Pen(Color.Gray);
            for (int x = 0; x <= columns; x++)
            {
                g.DrawLine(pen, x * cellSize, 0, x * cellSize, rows * cellSize);
            }
            for (int y = 0; y <= rows; y++)
            {
                g.DrawLine(pen, 0, y * cellSize, columns * cellSize, y * cellSize);
            }
        }

        private void DrawBlocks(Graphics g)
        {
            for (int x = 0; x < columns; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    if (grid[x, y] != null)
                    {
                        g.FillRectangle(grid[x, y], x * cellSize, y * cellSize, cellSize, cellSize);
                    }
                }
            }
        }

        public void PlaceShape(Point[] blocks, Brush color)
        {
            foreach (Point block in blocks)
            {
                if (block.X >= 0 && block.X < columns && block.Y >= 0 && block.Y < rows)
                {
                    grid[block.X, block.Y] = color;
                }
            }
            Invalidate();
        }

        public void DrawShape(Shape shape)
        {
            using (Graphics g = CreateGraphics())
            {
                foreach (Point block in shape.Blocks)
                {
                    g.FillRectangle(Brushes.Blue, block.X * cellSize, block.Y * cellSize, cellSize, cellSize);
                }
            }
        }
              public void ClearLines()
        {
            for (int y = rows - 1; y >= 0; y--)
            {
                bool fullLine = true;
                for (int x = 0; x < columns; x++)
                {
                    if (grid[x, y] == null)
                    {
                        fullLine = false;
                        break;
                    }
                }
                if (fullLine)
                {
                    for (int row = y; row > 0; row--)
                    {
                        for (int x = 0; x < columns; x++)
                        {
                            grid[x, row] = grid[x, row - 1];
                        }
                    }
                    for (int x = 0; x < columns; x++)
                    {
                        grid[x, 0] = null;
                    }
                    y++;
                }
            }
            Invalidate();
        }

        public bool CheckCollision(Point[] blocks)
        {
            foreach (Point block in blocks)
            {
                if (block.X < 0 || block.X >= columns || block.Y >= rows)
                {
                    return true;
                }
                if (block.Y >= 0 && grid[block.X, block.Y] != null)
                {
                    return true;
                }
            }
            return false;
        }
    }
}