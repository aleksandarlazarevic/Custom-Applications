using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebActions
{
    public class BinanceApi
    {
        public static void TestApi() 
        {
            WebRequest webrequest = WebRequest.Create("https://api.binance.com/api/v3/ticker/price?symbol=ADAUSDT");

            using (HttpWebResponse response = (HttpWebResponse)webrequest.GetResponse())
            {
                string status = ((HttpWebResponse)response).StatusDescription;

                if (status.Equals("OK"))
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(response.CharacterSet)))
                    {
                        string tmpStreamData = reader.ReadToEnd();
                        TokenValueFromApi tokenValue = JToken.Parse(tmpStreamData).ToObject<TokenValueFromApi>();
                    }
                }
                else
                {
                    throw new Exception("Api response is not OK");
                }
            }
        }
        public static decimal GetCurrentValue()
        {
            WebRequest webrequest = WebRequest.Create("https://api.binance.com/api/v3/ticker/price?symbol=ADAUSDT");
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
