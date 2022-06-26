using Newtonsoft.Json.Linq;
using SeleniumCore.Enums;

namespace SeleniumCore.Helpers.JSON
{
    public static class JSONHelper
    {
        public static JPathEvaluation EvaluateJPath(JToken expression)
        {
            JPathEvaluation jPathEvaluation;

            switch (expression?.Type)
            {
                case JTokenType.Object:
                    jPathEvaluation = JPathEvaluation.Object;
                    break;

                case JTokenType.Array:
                    jPathEvaluation = JPathEvaluation.Array;
                    break;

                case JTokenType.Date:
                    jPathEvaluation = JPathEvaluation.Date;
                    break;

                case JTokenType.Boolean:
                case JTokenType.Integer:
                case JTokenType.Float:
                case JTokenType.String:
                    jPathEvaluation = JPathEvaluation.Value;
                    break;

                case null:
                case JTokenType.Null:
                    jPathEvaluation = JPathEvaluation.NodeIsEmpty;
                    break;

                default:
                    jPathEvaluation = JPathEvaluation.NotSpecified;
                    break;
            }

            return jPathEvaluation;
        }
    }
}
