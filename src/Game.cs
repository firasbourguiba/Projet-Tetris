using System;
using System.Drawing;
using System.Windows.Forms;

namespace TetrisProject
{
    public class Game
    {
        private Grid gameGrid;
        private Shape currentShape;
        private System.Windows.Forms.Timer gameTimer;
        private bool[,] gridMatrix;
        private int score;
        private Label scoreLabel;

        public Game(Grid grid, Label scoreLabel)
        {
            gameGrid = grid;
            this.scoreLabel = scoreLabel;
            gridMatrix = new bool[10, 20];
            score = 0;
            StartNewGame();
        }

        public void StartNewGame()
        {
            currentShape = Shape.GetRandomShape();
            gameGrid.DrawShape(currentShape);
            gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = 300;
            gameTimer.Tick += Update;
            gameTimer.Start();
        }
        public void PauseGame()
        {
            gameTimer.Stop();
        }

        public void ResumeGame()
        {
            gameTimer.Start();
        }

        private void RestartGame()
        {
            gridMatrix = new bool[10, 20];
            score = 0;
            StartNewGame();
        }

     private void Update(object sender, EventArgs e)
        {
            Point[] movedBlocks = new Point[currentShape.Blocks.Length];
            for (int i = 0; i < currentShape.Blocks.Length; i++)
            {
                movedBlocks[i] = new Point(currentShape.Blocks[i].X, currentShape.Blocks[i].Y + 1);
            }

            if (!gameGrid.CheckCollision(movedBlocks))
            {
                MoveShapeDown();
                gameGrid.DrawShape(currentShape);
            }
            else
            {
                gameGrid.PlaceShape(currentShape.Blocks, Brushes.Blue);
                LockShape();
                gameGrid.ClearLines();
                UpdateScore();

                if (CheckGameOver())
                {
                    gameTimer.Stop();
                    DialogResult result = MessageBox.Show($"Game Over! Score: {score} \nRestart the game?", "Game Over", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        RestartGame();
                    }
                    return;
                }
                currentShape = Shape.GetRandomShape();
                gameGrid.DrawShape(currentShape);
            }
            //gameGrid.Invalidate();
        }

         public void MoveShapeDown()
        {
            for (int i = 0; i < currentShape.Blocks.Length; i++)
            {
                currentShape.Blocks[i].Y += 1;
            }
        }

        public void UpdateScore()
        {
            scoreLabel.Text = $"Score: {score}";
        }
        public void RotateShape()
        {
            Point pivot = currentShape.Blocks[1];
            Point[] rotatedBlocks = new Point[currentShape.Blocks.Length];

            for (int i = 0; i < currentShape.Blocks.Length; i++)
            {
                int x = currentShape.Blocks[i].Y - pivot.Y;
                int y = currentShape.Blocks[i].X - pivot.X;
                rotatedBlocks[i] = new Point(pivot.X - x, pivot.Y + y);
            }

            if (!gameGrid.CheckCollision(rotatedBlocks))
            {
                currentShape.Blocks = rotatedBlocks;
            }
        }

        private bool CheckGameOver()
        {
            foreach (Point block in currentShape.Blocks)
            {
                if (block.Y < 0)
                {
                    return true;
                }
            }
            return false;
        }
         private void LockShape()
        {
            foreach (Point block in currentShape.Blocks)
            {
                if (block.Y >= 0)
                {
                    gridMatrix[block.X, block.Y] = true;
                }
            }
        }

         public void MoveShapeLeft()
        {
            Point[] movedBlocks = new Point[currentShape.Blocks.Length];
            for (int i = 0; i < currentShape.Blocks.Length; i++)
            {
                movedBlocks[i] = new Point(currentShape.Blocks[i].X - 1, currentShape.Blocks[i].Y);
            }

            if (!gameGrid.CheckCollision(movedBlocks))
            {
                for (int i = 0; i < currentShape.Blocks.Length; i++)
                {
                    currentShape.Blocks[i].X -= 1;
                }
            }
        }

         public void MoveShapeRight()
        {
            Point[] movedBlocks = new Point[currentShape.Blocks.Length];
            for (int i = 0; i < currentShape.Blocks.Length; i++)
            {
                movedBlocks[i] = new Point(currentShape.Blocks[i].X + 1, currentShape.Blocks[i].Y);
            }

            if (!gameGrid.CheckCollision(movedBlocks))
            {
                for (int i = 0; i < currentShape.Blocks.Length; i++)
                {
                    currentShape.Blocks[i].X += 1;
                }
            }
        }



        
        private bool CheckCollision(int offsetX, int offsetY)
        {
            foreach (Point block in currentShape.Blocks)
            {
                int newX = block.X + offsetX;
                int newY = block.Y + offsetY;
                if (newX < 0 || newX >= 10 || newY >= 20 || newY < 0 || gridMatrix[newX, newY])
                {
                    return true;
                }
            }
            return false;
        }

       

        private void ClearLines()
        {
            for (int y = 19; y >= 0; y--)
            {
                bool fullLine = true;
                for (int x = 0; x < 10; x++)
                {
                    if (!gridMatrix[x, y])
                    {
                        fullLine = false;
                        break;
                    }
                }
                if (fullLine)
                {
                    for (int row = y; row > 0; row--)
                    {
                        for (int x = 0; x < 10; x++)
                        {
                            gridMatrix[x, row] = gridMatrix[x, row - 1];
                        }
                    }
                    y++;
                    score += 100;
                }
            }
        }

        
    }
}