using System;
using System.Net;

namespace SMS.Core.Helpers
{
    public static class CoreUtils
    {
        ///
        /// Verify url endpoint exists
        ///
        public static bool UrlExists(string url)
        {           
            if (url != null && url != "")
            {
                Console.Write("***UrlExists ");
                try {
                    var uri = new Uri(url, UriKind.Absolute);
                    // using method head doesn't down load the resource, rather it just verifies its existence
                    WebRequest webRequest = WebRequest.Create(uri);
                    webRequest.Method = "HEAD";
                    webRequest.GetResponse();                            
                } 
                catch
                {
                    Console.WriteLine(false);
                    return false;
                }
                Console.WriteLine(false);
            };
           
            return true; 
        }

        public static bool IsValidUrl(string url)
        {      
            bool result = true;     
            if (url != null && url != "")
            {
                Uri uriResult;
                result = Uri.TryCreate(url, UriKind.Absolute, out uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
                Console.WriteLine("****Validating Url - " + result);
            };
            return result; 
        }

    }
}