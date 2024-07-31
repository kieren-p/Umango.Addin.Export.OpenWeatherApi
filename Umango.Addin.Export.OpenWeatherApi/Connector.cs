using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umango.Addin.SDK.ExportConnector;
using Umango.Addin.SDK.ExportConnector.Interfaces;

namespace Umango.Addin.Export.OpenWeatherApi
{
    public class Connector : SDK.ExportConnector.Interfaces.IConnector
    {
        /// <summary>
        /// We need to tell Umango what class type will be holding the unique settings
        /// </summary>
        public Type ConnectorSettingsObjectType
        {
            get
            {
                return typeof(SettingsClass);
            }
        }

        public string ConnectorDescription
        {
            get
            {
                return "Check the weather inside of Umango";
            }
        }

        /// <summary>
        /// Returns the list of export fields that can be assigned in this connector
        /// In our example we only have 1 but there can be an unlimited number of fields for
        /// different types of searches
        /// </summary>
        public Microsoft.VisualBasic.Collection ExportFields
        {
            get
            {
                Collection FieldTypes = new Collection();

                return FieldTypes;
            }
        }

        public System.Drawing.Image Icon
        {
            get
            {
                Bitmap MyIcon;
                System.Reflection.Assembly MyAssembly = System.Reflection.Assembly.GetExecutingAssembly();
                string MyssemblyPath = MyAssembly.GetName().Name.Replace(" ", "_");
                // Set an embedded bitmap resource
                MyIcon = new Bitmap(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream((MyssemblyPath + ".Resources.weather.png")));
                return MyIcon;
            }
        }

        public string ConnectorName
        {
            get
            {
                return "Weather Check";
            }
        }

        /// <summary>
        /// eg. Microsoft (For connectors such as SharePoint, OneDrive etc)
        /// </summary>
        public string Company
        {
            get
            {
                return "OpenWeather";
            }
        }

        public Industry IndustryVertical
        {
            get
            {
                return Industry.Other;
            }
        }

        public Category ProductCategory
        {
            get
            {
                return Category.Miscellaneous;
            }
        }

        public event EventHandler<ExportProgressEventArgs> ExportProgress;

        /// <summary>
        /// This is now obsolete as our product is fully web based now. In production it will never be called
        /// </summary>
        /// <param name="CurrentSettings"></param>
        /// <returns></returns>
        public System.Windows.Forms.DialogResult DisplayConfigurator(ref Settings CurrentSettings)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            // Nothing to do here
        }

        /// <summary>
        /// This is the most important function. This is what does all the magic when Umango exports a batch
        /// </summary>
        /// <param name="PathToTempFile">The temporary file that is being exported.
        /// The format and file extension is in whatever format was selected in the configuration within Umango</param>
        /// <param name="JobSettings">All the settings related to this connector and the users settings for the job the connector has been assigned to</param>
        /// <returns></returns>
        public ExportResult ProcessDocument(string PathToTempFile, Settings JobSettings)
        {
            try
            {
                ExportSuccessResultExt successState = new ExportSuccessResultExt();
                
                return successState;
            }
            catch (ExportErrorResult.ExportFailedException exFinal)
            {
                ExportErrorResult FailedState = new ExportErrorResult();
                FailedState.Successful = false;
                FailedState.FailureException = exFinal;

                return FailedState;
            }
        }
    }
}
