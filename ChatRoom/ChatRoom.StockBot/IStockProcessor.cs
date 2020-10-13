namespace ChatRoom.StockBot
{
    public interface IStockProcessor
    {
        StockInfo GetStockInfo(string command);
    }
}
