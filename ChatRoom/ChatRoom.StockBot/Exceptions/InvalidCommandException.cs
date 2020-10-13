using System;

namespace ChatRoom.StockBot.Exceptions
{
    public class InvalidCommandException : Exception
    {
        public override string Message => "The stock command is invalid.";
    }
}
