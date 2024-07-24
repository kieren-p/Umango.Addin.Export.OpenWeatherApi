using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Umango.Addin.Export.OpenWeatherApi
{
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