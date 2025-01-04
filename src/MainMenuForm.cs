using System;
using System.Windows.Forms;

namespace TetrisProject
{
    public class MainMenuForm : Form
    {
        public MainMenuForm()
        {
            Text = "Tetris Project - Main Menu";
            Width = 400;
            Height = 500;

            Button startButton = new Button()
            {
                Text = "Start Game",
                Location = new System.Drawing.Point(150, 200),
                Width = 100
            };

            Button quitButton = new Button()
            {
                Text = "Quit",
                Location = new System.Drawing.Point(150, 250),
                Width = 100
            };

            startButton.Click += (sender, e) =>
            {
                MainForm mainForm = new MainForm();
                mainForm.Show();
                this.Hide();
            };

            quitButton.Click += (sender, e) => Application.Exit();

            Controls.Add(startButton);
            Controls.Add(quitButton);
        }
    }
}