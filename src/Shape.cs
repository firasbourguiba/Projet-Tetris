using System;
using System.Drawing;

namespace TetrisProject
{
    public class Shape
    {
        public Point[] Blocks { get; private set; }
        public Color ShapeColor { get; private set; }

        public Shape(Point[] blocks, Color color)
        {
            Blocks = blocks;
            ShapeColor = color;
        }

        public void Draw(Graphics g, int cellSize)
        {
            foreach (Point block in Blocks)
            {
                g.FillRectangle(new SolidBrush(ShapeColor), 
                                block.X * cellSize, 
                                block.Y * cellSize, 
                                cellSize, 
                                cellSize);
            }
        }

        public static Shape GetRandomShape()
        {
            Random rand = new Random();
            int shapeType = rand.Next(0, 5);

            switch (shapeType)
            {
                case 0:
                    return new Shape(new Point[] { new Point(0, 0), new Point(1, 0), new Point(2, 0), new Point(3, 0) }, Color.Cyan); // Ligne
                case 1:
                    return new Shape(new Point[] { new Point(0, 0), new Point(0, 1), new Point(1, 1), new Point(2, 1) }, Color.Blue); // L
                case 2:
                    return new Shape(new Point[] { new Point(1, 0), new Point(0, 1), new Point(1, 1), new Point(2, 1) }, Color.Purple); // T
                case 3:
                    return new Shape(new Point[] { new Point(0, 0), new Point(1, 0), new Point(0, 1), new Point(1, 1) }, Color.Yellow); // Carré
                case 4:
                    return new Shape(new Point[] { new Point(1, 0), new Point(2, 0), new Point(0, 1), new Point(1, 1) }, Color.Red); // Z
                default:
                    return new Shape(new Point[] { new Point(0, 0) }, Color.White);
            }
        }
    }
}