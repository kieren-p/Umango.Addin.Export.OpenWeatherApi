using System;
using System.Collections.Generic;
using System.Net.Http;
using Umango.Addin.SDK.ExportConnector.Interfaces;

namespace Umango.Addin.Export.OpenWeatherApi
{
    public class SettingsManager : ISettingsResources
    {
        private static HttpClient httpClient = new HttpClient();
        private string API_KEY = "78756486a1e5e302c510165a051fa3dc";
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
                return GetFileContent("ConnectorHTML.html");
            }

        }

        public string MethodCaller(string ConnectionMethod, List<KeyValuePair<string, string>> Params)
        {

            if (ConnectionMethod.ToLower() == "get_weather")
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
            string MethodResult = "";
            string citySelection = "";

            foreach (KeyValuePair<string, string> pair in Params)
            {
                if (pair.Key.ToString().ToUpper() == "CITY")
                {
                    citySelection = pair.Value;
                }
            }

            if (string.IsNullOrEmpty(citySelection))
            {
                return "{\"succeeded\": false, \"message\": \"You forgot to enter a city, silly!\"}";
            }

            string apiUrl = $"http://api.openweathermap.org/data/2.5/weather?q={citySelection}&appid={API_KEY}";

            try
            {
                HttpResponseMessage response = httpClient.GetAsync(apiUrl).GetAwaiter().GetResult();
                response.EnsureSuccessStatusCode();
                string responseBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                MethodResult = responseBody;
            }
            catch (Exception ex)
            {
                MethodResult = $"{{\"succeeded\": false, \"message\": \"{ex.Message}\"}}";
            }

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
