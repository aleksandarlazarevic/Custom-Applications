using Binance.Net;
using Binance.Net.Objects.Spot;
using CryptoExchange.Net.Authentication;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using Binance.Net.Enums;
using CryptoExchange.Net.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using Binance.Net.Objects.Spot.SpotData;
using CryptoExchange.Net.Objects;

namespace WebActions
{
    public class BinanceApi
    {
        static string ComputeContentHash(string content)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(content));
                return Convert.ToBase64String(hashedBytes);
            }
        }
        static string ComputeSignature(string stringToSign)
        {
            string secret = "resourceAccessKey";
            using (var hmacsha256 = new HMACSHA256(Convert.FromBase64String(secret)))
            {
                var bytes = Encoding.ASCII.GetBytes(stringToSign);
                var hashedBytes = hmacsha256.ComputeHash(bytes);
                return Convert.ToBase64String(hashedBytes);
            }
        }

        public static void SubscribeToPriceChanges()
        {
            //var socketClient = new BinanceSocketClient();

            //socketClient.Spot.SubscribeToAllBookTickerUpdates(data =>
            //{
            //    Values.ValueChanges.Add(data.BestBidPrice);
            //});
            ////var client = new BinanceClient(new BinanceClientOptions
            ////{
            ////    ApiCredentials = new ApiCredentials("oh25XCVTR19w4uzz9QyVjDjRuHan8RcLra4cekSDg9HfIfSzZL6pc3nQLYjnO3DQ", "Y0a1Dg8MriaS9LejfvTkiNqZAyMukGWFaaYK2KGN8X9o4PcvOc3OVR8CZXxanTgy"),
            ////    AutoTimestamp = true,
            ////    AutoTimestampRecalculationInterval = TimeSpan.FromMinutes(30),
            ////    BaseAddress = "https://api.binance.com",
            ////    LogVerbosity = LogVerbosity.Debug,
            ////    LogWriters = new List<TextWriter> { Console.Out }
            ////});

            ////var zz = client.Spot.Market.GetBookPrice("ADAUSDT");
        }
        public async static void TestApi()
        {
            // TODO: Dispose BinanceClient and BinanceSocketClient at some point.
            var client = new BinanceClient(new BinanceClientOptions
            {
                ApiCredentials = new ApiCredentials(Trading.apiKey, Trading.secretKey),
                AutoTimestamp = true,
                AutoTimestampRecalculationInterval = TimeSpan.FromMinutes(30),
                BaseAddress = "https://api.binance.com",
                LogVerbosity = LogVerbosity.Debug,
                LogWriters = new List<TextWriter> { Console.Out }
            });

            WebCallResult<BinanceAccountInfo> accountInfo = client.General.GetAccountInfo();
            decimal usdtBalance = accountInfo.Data.Balances.Where(p => p.Asset.Equals("USDT")).Select(t => t.Free).First();
            decimal adaBalance = accountInfo.Data.Balances.Where(p => p.Asset.Equals("ADA")).Select(t => t.Free).First();
            
            int rounded = (int)Math.Floor(adaBalance* BinanceApi.GetCurrentValue(Coin.Pair));
            var result = await client.Spot.Order.PlaceOrderAsync("ADAUSDT", OrderSide.Buy, OrderType.Market, quoteOrderQuantity: usdtBalance).ConfigureAwait(false);

            try
            {
                var result2 = await client.Spot.Order.PlaceOrderAsync("ADAUSDT", OrderSide.Sell, OrderType.Market, quoteOrderQuantity: rounded).ConfigureAwait(false);
            }
            catch (Exception ex)
            {

                var error = ex.Message;
            }

            if (result.Success)
            {
                MessageBox.Show("Order placed!");
            }
            else
            {
                MessageBox.Show("Order placing Failed");
            }
            client.Dispose();
        }
        public static decimal GetCurrentValue(string coinToCheck)
        {
            WebRequest webrequest = WebRequest.Create("https://api.binance.com/api/v3/ticker/price?symbol=" + coinToCheck);
            TokenValueFromApi tokenValue = new TokenValueFromApi();

            using (HttpWebResponse response = (HttpWebResponse)webrequest.GetResponse())
            {
                string status = ((HttpWebResponse)response).StatusDescription;

                if (status.Equals("OK"))
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(response.CharacterSet)))
                    {
                        string tmpStreamData = reader.ReadToEnd();
                        tokenValue = JToken.Parse(tmpStreamData).ToObject<TokenValueFromApi>();
                    }
                }
                else
                {
                    throw new Exception("Api response is not OK");
                }
            }

            return decimal.Parse(tokenValue.price, CultureInfo.InvariantCulture);
        }
    }
}
