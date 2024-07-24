using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umango.Addin.SDK.ExportConnector.Interfaces;

namespace Umango.Addin.Export.OpenWeatherApi
{
    public class SettingsManager : ISettingsResources
    {
        public string Javascript
        {
            get
            {
                return GetFileContent("Javascript.js");
            }

        }

        public string Html
        {
            get
            {
                return GetFileContent("ConnectorHtml.html");
            }

        }

        public string MethodCaller(string ConnMethod, List<KeyValuePair<string, string>> Params)
        {

            if (ConnMethod.ToLower() == "get_weather")
            {
                return GetWeather(Params);
            }
            else
            {
                return "";
            }

        }

        private string GetWeather(List<KeyValuePair<string, string>> Params)
        {

            if (Params.Count < 1)
            {
                return "";
            }

            string MethodResult = "";
            string citySelection = "";

            foreach (KeyValuePair<string, string> pair in Params)
            {
                if (pair.Key.ToString().ToUpper() == "CITY")
                {
                    citySelection = pair.Value;
                }
            }

            // Do some API stuff here!?

            return MethodResult;

        }

        private string GetFileContent(string FileName)
        {
            string FileContent = "";

            try
            {
                System.Reflection.Assembly MyAssembly = System.Reflection.Assembly.GetExecutingAssembly();
                string MyssemblyPath = MyAssembly.GetName().Name.Replace(" ", "_");

                // Set an embedded bitmap resource
                using (System.IO.Stream TextFileStream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(MyssemblyPath + "." + FileName))
                {
                    using (System.IO.StreamReader reader = new System.IO.StreamReader(TextFileStream))
                    {
                        FileContent = reader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return FileContent;
        }

    }
}
