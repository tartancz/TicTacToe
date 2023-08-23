using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class InvalidMoveException : Exception
    {
        public InvalidMoveException(string message): base(message) { }
    }
    public class FieldAlreadyTakenException : Exception
    {
        public FieldAlreadyTakenException(string message) : base(message) { }
    }
    public class GameIsOverException : Exception
    {
        public GameIsOverException(string message) : base(message) { }
    }
}
