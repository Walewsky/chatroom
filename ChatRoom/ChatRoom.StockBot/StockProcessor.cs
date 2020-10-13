using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using ChatRoom.StockBot.Exceptions;
using ChoETL;

namespace ChatRoom.StockBot
{
    public class StockProcessor : IStockProcessor
    {
        private readonly List<string> _availableCommands = new List<string> { "stock" };
        private readonly IWebClient _webClient;

        public StockProcessor(IWebClient webClient)
        {
            _webClient = webClient;
        }

        public StockInfo GetStockInfo(string command)
        {
            if (!command.StartsWith("/"))
                throw new InvalidCommandExpressionException();

            command = command.Substring(1);

            var commandContext = GetCommandContext(command);

            if (!IsValidCommand(commandContext.Command))
                throw new InvalidCommandException();

            var stockUri = $"https://stooq.com/q/l/?s={commandContext.Argument}&f=sd2t2ohlcv&h&e=csv";

            var stockCsv  = _webClient.DownloadString(stockUri);

            var stockInfo = ChoCSVReader<StockInfo>.LoadText(stockCsv).WithFirstLineHeader().First();

            if (stockInfo.Date == DateTime.MinValue)
                throw new InvalidStockArgumentException();

            return stockInfo;
        }

        private CommandContext GetCommandContext(string command)
        {
            var context = command.Split("=");

            if (context.Length != 2 || context.Any(string.IsNullOrEmpty))
                throw new InvalidCommandExpressionException();

            return new CommandContext { Command = context[0], Argument = context[1] };
        }

        private bool IsValidCommand(string command)
        {
            return _availableCommands.Contains(command);
        }
    }
}
