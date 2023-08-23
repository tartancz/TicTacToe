using System.Diagnostics;
using System.Windows.Forms;

namespace TicTacToe;
public partial class Form1 : Form
{
    private Label LabelState; 
    protected IGame Game;
    public Form1(IGame game)
    {
        this.Game = game;
        InitializeComponent();
        myInit();
    }

    public void myInit()
    {
        for (int field = 0; field < 9; field++)
        {
            Button button = new Button();
            button.Location = new Point(
                90 * (field % 3),
                90 * (int)(field / 3)
                );
            button.Name = $"field{field}";
            button.Size = new Size(90, 90);
            button.UseVisualStyleBackColor = true;
            button.BackColor = Color.White;
            button.Click += clicked;
            Controls.Add(button);
        }

        LabelState = new();
        LabelState.Location = new Point(0, 270);
        LabelState.Size = new Size(ClientSize.Width, 30);
        LabelState.Name = "GameState";
        LabelState.TextAlign = ContentAlignment.MiddleCenter;
        LabelState.Font = new Font("Arial", 24, FontStyle.Bold);
        LabelState.Text = "";
        Controls.Add(LabelState);



        Button RstButton = new Button();
        RstButton.Location = new Point(0, 300);
        RstButton.Size = new Size(ClientSize.Width, 30);
        RstButton.Font = new Font("Arial", 12, FontStyle.Bold);
        RstButton.Text = "RESET";
        RstButton.Click += reset;
        Controls.Add(RstButton);

        void clicked(object? sender, EventArgs e)
        {
            if (sender is Button button)
            {
                int id = int.Parse(button.Name.Replace("field", ""));
                try { Game.Move(id); }
                catch (FieldAlreadyTakenException ex) { MessageBox.Show(ex.ToString()); return; }
                catch (GameIsOverException ex) { this.reset(); return; }
                switch (Game.State)
                {
                    case GameState.PlayX:
                        LabelState.Text = "O";
                        button.BackgroundImage = Properties.Resources.X;
                        break;
                    case GameState.PlayO:
                        LabelState.Text = "X";
                        button.BackgroundImage = Properties.Resources.O;
                        break;
                    case GameState.Draw:
                        LabelState.Text = "DRAW";
                        break;
                    case GameState.WinX:
                        LabelState.Text = "WINNER X";
                        button.BackgroundImage = Properties.Resources.X;
                        break;
                    case GameState.WinO:
                        LabelState.Text = "WINNER O";
                        button.BackgroundImage = Properties.Resources.O;
                        break;
                }
                Game.changePlayer();
            }
        }

        void reset(object? sender, EventArgs e)
        {
            this.reset();
        }
    }
    public void reset()
    {
        Game.Reset();
        for (int i = 0; i < 9; i++)
        {
            if (Controls[$"field{i}"] is Button button)
            {
                button.BackgroundImage = null;
            }
        }
    }
}
