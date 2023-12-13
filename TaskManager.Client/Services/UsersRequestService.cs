using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TaskManager.Client.Models;

namespace TaskManager.Client.Services
{
    class UsersRequestService
    {
        private const string HOST = "http://localhost:5129/api/";

        private string GetDataByUrl(string url, string userName, string password)
        {
            string result = string.Empty;

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "POST";
            if (userName != null && password != null)
            {
                string encoded = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1")
                    .GetBytes(userName + ":" + password));
                request.Headers.Add("Authorization", "Basic " + encoded);

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    string responseStr = reader.ReadToEnd();
                    return responseStr;
                }
            }

            return result;
        }

        public AuthToken GetToken(string userName, string password)
        {
            string url = HOST + "/Account/token";
            string resultStr = GetDataByUrl(url, userName, password);
            AuthToken token = JsonConvert.DeserializeObject<AuthToken>(resultStr);
             
            return token;
        }
    }
}
