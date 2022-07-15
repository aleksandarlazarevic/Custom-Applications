using System;
using System.Collections.Generic;
using System.Text;

namespace IOTests.Utils
{
    public class Constants
    {
        public static string AmazingCoinHash = "2048c7e09308f9138cef8f1a81733b72e601d016eea5eef759ff2933416d617a696e67436f696e";
        public static string GoodCoinHash = "94d4cdbcffb09ebd4780d94f932a657dc4852530fa8013df66c72d4c676f6f64636f696e";
        public static string NoAssetNameCoinHash = "789ef8ae89617f34c07f7f6a12e4d65146f958c0bc15a97b4ff169f1";

        public static string NoAssetNameCoinPutSubjectsRequest = "{\"subjects\": [\"789ef8ae89617f34c07f7f6a12e4d65146f958c0bc15a97b4ff169f16861707079636f696e\",\"789ef8ae89617f34c07f7f6a12e4d65146f958c0bc15a97b4ff169f1\"]}";
        public static string NoAssetNameCoinPutSubjectsWithPropertiesRequest = "{\"subjects\": [\"789ef8ae89617f34c07f7f6a12e4d65146f958c0bc15a97b4ff169f16861707079636f696e\",\"789ef8ae89617f34c07f7f6a12e4d65146f958c0bc15a97b4ff169f1\",\"94d4cdbcffb09ebd4780d94f932a657dc4852530fa8013df66c72d4c676f6f64636f696e\"],\"properties\": [\"name\", \"description\", \"url\"]}";

        public static object UrlProperty = "url";
        public static object NameProperty = "name";
        public static object TickerProperty = "ticker";
        public static object DecimalsProperty = "decimals";
        public static object PolicyProperty = "policy";
        public static object LogoProperty = "logo";
        public static object DescriptionProperty = "description";
    }
}
