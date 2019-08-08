using System.Xml;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace FCT.WebAPI.Helpers {
    public static class Extensions {
        public static void AddApplicationError (this HttpResponse response, string message) {
            response.Headers.Add ("Application-Error", message);
            response.Headers.Add ("Access-Control-Expose-Headers", "Application-Error");
            response.Headers.Add ("Access-Control-Allow-Origin", "*");
        }

        public static bool IsValidJson (this string value) {
            try {
                var json = JContainer.Parse (value);
                return true;
            } catch {
                return false;
            }
        }
        public static bool IsValidXml (this string value) {
            try {
                // Check we actually have a value
                if (string.IsNullOrEmpty (value) == false) {
                    // Try to load the value into a document
                    XmlDocument xmlDoc = new XmlDocument ();

                    xmlDoc.LoadXml (value);

                    // If we managed with no exception then this is valid XML!
                    return true;
                } else {
                    // A blank value is not valid xml
                    return false;
                }
            } catch (System.Xml.XmlException) {
                return false;
            }
        }

    }
}