using System;

namespace ChatRoom.StockBot.Exceptions
{
    public class InvalidStockArgumentException : Exception
    {
        public override string Message => "Invalid stock argument.";
    }
}
