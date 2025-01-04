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
        private Button pauseButton;
        private PictureBox nextShapePreview;
        private bool isPaused = false;

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

            pauseButton = new Button
            {
                Text = "Pause",
                Location = new Point(270, 60),
                Size = new Size(80, 30)
            };
            pauseButton.Click += (sender, e) => TogglePause();

            nextShapePreview = new PictureBox
            {
                Location = new Point(50, 50),
                Size = new Size(100, 100),
                BackColor = Color.Gray
            };

            Controls.Add(gameGrid);
            Controls.Add(scoreLabel);
            Controls.Add(restartButton);
            Controls.Add(pauseButton);
            Controls.Add(nextShapePreview);

            game = new Game(gameGrid, scoreLabel);
        }

        private void RestartGame()
        {
            game.StartNewGame();
        }

        private void TogglePause()
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                game.PauseGame();
                pauseButton.Text = "Resume";
            }
            else
            {
                game.ResumeGame();
                pauseButton.Text = "Pause";
            }
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (game != null && !isPaused)
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

        public void UpdateScore(int newScore)
        {
            scoreLabel.Text = $"Score: {newScore}";
        }
        
    }
}