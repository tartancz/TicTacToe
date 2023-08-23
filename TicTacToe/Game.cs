namespace TicTacToe
{
    public interface IGame
    {
        public GameState State { get; set; }

        void Move(int pos);
        void changePlayer();
        bool IsGameOver();
        void Reset();
    }
    public enum GameState
    {
        PlayX,
        PlayO,
        Draw,
        WinX,
        WinO,
    }

    public enum Field
    {
        Empty,
        X,
        O,
    }

    internal class Game : IGame
    {
        private static readonly (int, int, int)[] WinCombination = new (int, int, int)[] { (0, 1, 2), (3, 4, 5), (6, 7, 8), (0, 3, 6), (1, 4, 7), (2, 5, 8), (0, 4, 8), (2, 4, 6) };

        private Field[] Fields = new Field[9];
        private Field WhoStarted;
        private int PlayedRound
        {
            get
            {
                int count = 0;
                foreach (Field field in Fields)
                {
                    if (field != Field.Empty)
                        count++;
                }
                return count;
            }
        }

        public GameState State { get; set; }

        public Game()
        {
            this.State = GameState.PlayX;
            this.WhoStarted = Field.X;
        }

        public void Reset()
        {
            for (int i = 0; i < Fields.Length; i++)
            {
                Fields[i] = Field.Empty;
            }
            switch (WhoStarted)
            {
                case Field.X:
                    State = GameState.PlayX;
                    break;
                case Field.O:
                    State = GameState.PlayO;
                    break;
            }
        }

        public void Move(int pos)
        {
            if (IsGameOver())
                throw new GameIsOverException("Game is over, you cant make move");
            setFieldMove(pos);
            if (IsWinner())
                SetWinner();
            else if (Fields.Length == PlayedRound)
            {
                State = GameState.Draw;
            }
        }

        public void changePlayer()
        {
            switch (State)
            {
                case GameState.PlayX:
                    State = GameState.PlayO;
                    break;
                case GameState.PlayO:
                    State = GameState.PlayX;
                    break;
            }
        }

        private void setFieldMove(int pos)
        {
            if (Fields[pos] != Field.Empty)
                throw new FieldAlreadyTakenException($"field on pos X-{pos % 3} Y-{(int)(pos / 3)} already taken.");
            switch (State)
            {
                case GameState.PlayX:
                    Fields[pos] = Field.X;
                    break;
                case GameState.PlayO:
                    Fields[pos] = Field.O;
                    break;
                default: return;
            }
        }

        private void SetWinner()
        {
            switch (State)
            {
                case GameState.PlayX:
                    State = GameState.WinX;
                    break;
                case GameState.PlayO:
                    State = GameState.WinO;
                    break;
                default: return;
            }
        }
        private bool IsWinner()
        {
            foreach (var combi in Game.WinCombination)
            {
                bool IsRowCompleted =
                    Fields[combi.Item1] == Fields[combi.Item2]
                    && Fields[combi.Item2] == Fields[combi.Item3]
                    && Fields[combi.Item1] != Field.Empty
                    && Fields[combi.Item2] != Field.Empty
                    && Fields[combi.Item3] != Field.Empty;
                if (IsRowCompleted)
                    return true;
            }
            return false;
        }


        public bool IsGameOver()
        {
            return State != GameState.PlayX && State != GameState.PlayO;
        }
    }
}
