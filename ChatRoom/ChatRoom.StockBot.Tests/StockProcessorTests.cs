using System;
using Moq;
using NUnit.Framework;

namespace ChatRoom.StockBot.Tests
{
    [TestFixture(TestOf = typeof(StockProcessor))]
    public class StockProcessorTests
    {
        [TestCase("show")]
        [TestCase("/")]
        [TestCase("/stock")]
        [TestCase("/stock=")]
        public void GetStockInfo_WhenCommandExpressionIsInvalid_ThrowsInvalidCommandExpressionException(string command)
        {
            var sp = new StockProcessor(new AppWebClient());

            Assert.Throws<Exceptions.InvalidCommandExpressionException>(() => sp.GetStockInfo(command));
        }

        [TestCase("/show=aap.us")]
        public void GetStockInfo_WhenCommandIsNotAvailable_ThrowsInvalidCommandException(string command)
        {
            var sp = new StockProcessor(new AppWebClient());

            Assert.Throws<Exceptions.InvalidCommandException>(() => sp.GetStockInfo(command));
        }

        [TestCase("/stock=aapl.fr")]
        public void GetStockInfo_WhenArgumentIsInvalid_ThrowsInvalidStockArgumentException(string command)
        {
            const string stockCsv = "Symbol,Date,Time,Open,High,Low,Close,Volume\nAAPL.FR,N/D,N/D,N/D,N/D,N/D,N/D,N/D";

            var webClientMock = new Mock<IWebClient>();

            webClientMock.Setup(x => x.DownloadString(It.IsAny<string>())).Returns(stockCsv);

            var sp = new StockProcessor(webClientMock.Object);

            Assert.Throws<Exceptions.InvalidStockArgumentException>(() => sp.GetStockInfo(command));
        }

        [TestCase("/stock=aapl.us")]
        public void GetStockInfo_When_ThrowsInvalidCommandException(string command)
        {
            const string stockCsv = "Symbol,Date,Time,Open,High,Low,Close,Volume\nAAPL.US,2020-10-09,22:00:01,115.28,117,114.92,116.97,100506865";

            var webClientMock = new Mock<IWebClient>();

            webClientMock.Setup(x => x.DownloadString(It.IsAny<string>())).Returns(stockCsv);

            var sp = new StockProcessor(webClientMock.Object);

            var stockInfo = sp.GetStockInfo(command);

            Assert.AreEqual("AAPL.US", stockInfo.Symbol);
            Assert.AreEqual(116.97D, stockInfo.Close);
            Assert.AreEqual(new DateTime(2020, 10, 9), stockInfo.Date);
        }
    }
}
