using System;

namespace ChatRoom.StockBot.Exceptions
{
    public class InvalidCommandExpressionException : Exception
    {
        public override string Message => "The command expression is invalid.";
    }
}
