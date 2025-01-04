using System;
using System.Drawing;
using System.Windows.Forms;
using TetrisProject;  // Import de la classe Grid

namespace TetrisProject
{
    public class MainForm : Form
    {
        private Grid gameGrid;
        private Game game;
        private Label scoreLabel;
        private Button restartButton;

        public MainForm()
        {
            Text = "Tetris Project";
            Width = 400;
            Height = 700;
            InitializeComponents();
            this.KeyDown += new KeyEventHandler(OnKeyDown);
            this.KeyPreview = true;
        }

        private void InitializeComponents()
        {
            gameGrid = new Grid
            {
                Location = new Point(50, 100),
                Size = new Size(300, 600)
            };

            scoreLabel = new Label
            {
                Text = "Score: 0",
                Location = new Point(50, 20),
                Size = new Size(200, 30),
                Font = new Font("Arial", 16)
            };

            restartButton = new Button
            {
                Text = "Restart",
                Location = new Point(270, 20),
                Size = new Size(80, 30)
            };
            restartButton.Click += (sender, e) => RestartGame();

            Controls.Add(gameGrid);
            Controls.Add(scoreLabel);
            Controls.Add(restartButton);

            game = new Game(gameGrid, scoreLabel);
        }

        private void RestartGame()
        {
            game.StartNewGame();
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (game != null)
            {
                switch (e.KeyCode)
                {
                    case Keys.Left:
                        game.MoveShapeLeft();
                        break;
                    case Keys.Right:
                        game.MoveShapeRight();
                        break;
                    case Keys.Up:
                        game.RotateShape();
                        break;
                    case Keys.Down:
                        game.MoveShapeDown();
                        break;
                }
                gameGrid.Invalidate();
            }
        }
    }
}