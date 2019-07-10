using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Weather
{
    class CurentLocation
    {
        IPServerAnswer answer;

        public CurentLocation()
        {
            HttpClient client = new HttpClient();
            string json = "";
            string myIP = new WebClient().DownloadString("http://icanhazip.com");
            json = client.GetStringAsync("http://free.ipwhois.io/json/" + myIP).Result;
            answer = JsonConvert.DeserializeObject<IPServerAnswer>(json);
        }

        public string GetLocation() => answer.city;
        
    }
}
