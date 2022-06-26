using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SeleniumCore.Helpers.JSON
{
    public class JSONDocumentFileLoader
    {
        #region fields
        private JToken _jObject;
        private string _fileAbsolutePath;
        #endregion

        #region constructors
        public JSONDocumentFileLoader(string fileAbsolutePath)
        {
            if (string.IsNullOrEmpty(fileAbsolutePath))
                throw new ArgumentNullException("Missing file path");

            if (!File.Exists(fileAbsolutePath))
                throw new FileNotFoundException($"File is not found for the path[{fileAbsolutePath}]");

            _fileAbsolutePath = fileAbsolutePath;
        }
        #endregion

        #region Methods Public
        public JToken LoadJSONDoucumentIntoObject()
        {
            using (StreamReader streamReader = File.OpenText(_fileAbsolutePath))
            {
                using (JsonTextReader jsonReader = new JsonTextReader(streamReader))
                {
                    _jObject = JToken.Load(jsonReader);
                }
            }

            return _jObject;
        }

        public void SaveJSONDocumentIntoFile()
        {
            File.WriteAllText(_fileAbsolutePath, _jObject.ToString());
        }
        #endregion
    }
}
