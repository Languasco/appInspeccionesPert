using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Negocios.Mantenimiento
{
    public class Dominio_BL
    {
        public string CheckValidDomain(string dominio)
        {
            string result = "";
            string url_https = "https://www." + dominio;
            string url_http = "http://www." + dominio;
            WebResponse response = null;
            string data = string.Empty;
            try
            {
                if (url_https != "")
                {
                    WebRequest request = WebRequest.Create(url_https);
                    response = request.GetResponse();
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        data = reader.ReadToEnd();
                    }
                    // domain exists, this is valid domain
                    result = "ok";
                }
            }
            catch (WebException)
            {
                if (url_http != "")
                {
                    WebRequest request = WebRequest.Create(url_http);
                    response = request.GetResponse();
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        data = reader.ReadToEnd();
                    }
                    // domain exists, this is valid domain
                    result = "ok";
                }
                else
                {
                    result = "No existe este dominio";
                }
            }
            catch (Exception)
            {
                // Some error occured, the domain might exists 
                result = "A ocurrido un error!";
            }
            finally
            {
                if (response != null) response.Close();
            }
            return result;
        }

        public static bool ValidHttpURL(string s, out Uri resultURI)
        {
            if (!Regex.IsMatch(s, @"^https?:\/\/", RegexOptions.IgnoreCase))
                s = "http://" + s;

            if (Uri.TryCreate(s, UriKind.Absolute, out resultURI))
                return (resultURI.Scheme == Uri.UriSchemeHttp ||
                        resultURI.Scheme == Uri.UriSchemeHttps);

            return false;
        }
        public bool ValidateUrl(string dominio)
        {
            Uri uriResult;
            bool result = ValidHttpURL(dominio, out uriResult);

            return result;
        }
    }
}
