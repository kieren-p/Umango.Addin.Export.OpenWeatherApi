using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Umango.Addin.Export.OpenWeatherApi
{
    // Here is the SettingsClass we defined in Connector.cs
    // All the values client side are saved and serialized in here for use within the connector
    [Serializable()]
    [XmlInclude(typeof(SettingsClass))]
    [DataContract]
    public class SettingsClass
    {
        // Parameterless constructor required for serialization
        public SettingsClass()
        {

        }

        /// <summary>
        /// The city to look up weather for
        /// </summary>
        [DataMember]
        public string CitySelection { get; set; }
    }
}