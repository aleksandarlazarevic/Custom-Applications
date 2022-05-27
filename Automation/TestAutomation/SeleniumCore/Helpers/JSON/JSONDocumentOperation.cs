using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SeleniumCore.Helpers.Serializable;
using SeleniumCore.Enums;

namespace SeleniumCore.Helpers.JSON
{
    public class JSONDocumentOperation
    {
        JToken _jObject;

        public JSONDocumentOperation(JToken jObject)
        {
            _jObject = jObject;
        }

        public void Add(JpathAndValue jpathAndValue)
        {
            throw new NotImplementedException();
        }

        public void Delete(JpathAndValue jpathAndValue)
        {
            _jObject.SelectToken(jpathAndValue.Jpath).Remove();
        }

        public void Edit(JpathAndValue jpathAndValue)
        {
            JToken token = _jObject.SelectToken(jpathAndValue.Jpath);
            JValue value = null;

            switch (token.Type)
            {
                case JTokenType.Boolean:
                    value = new JValue(Convert.ToBoolean(jpathAndValue.Value));
                    token.Replace(value);
                    break;

                case JTokenType.Float:
                    value = new JValue(Convert.ToDecimal(jpathAndValue.Value));
                    token.Replace(value);
                    break;

                case JTokenType.Integer:
                    value = new JValue(Convert.ToInt32(jpathAndValue.Value));
                    token.Replace(value);
                    break;

                case JTokenType.Array:
                    JArray jArray = token.Value<JArray>();
                    jArray.Add(new JValue(jpathAndValue.Value));
                    token.Replace(jArray);
                    break;

                default:
                    value = new JValue(jpathAndValue.Value);
                    token.Replace(value);
                    break;
            }
        }

        public JPathEvaluation EvaluateXpath(JpathAndValue jpathAndValue)
        {
            try
            {
                return JSONHelper.EvaluateJPath(_jObject.SelectToken(jpathAndValue.Jpath));
            }
            catch (Exception ex)
            {
                throw new JsonException($"Error occurred for the Jpath:[{jpathAndValue.Jpath}]", ex);
            }
        }

        public string Read(JpathAndValue jpathAndValue)
        {
            return _jObject.SelectToken(jpathAndValue.Jpath)?.Value<string>();
        }

        public bool Verify(JpathAndValue jpathAndValue, out string value)
        {
            value = _jObject.SelectToken(jpathAndValue.Jpath).Value<string>();
            return value.Equals(jpathAndValue.Value);
        }
    }
}
