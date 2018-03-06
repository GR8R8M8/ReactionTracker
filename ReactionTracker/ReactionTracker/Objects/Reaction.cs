using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ReactionTracker
{
    class Reaction
    {
        public string reactionName { get; set; }
        public string inputIdentifier { get; set; }
        public string outputIdentifier { get; set; }

        public Reaction(string Name, string iUrl, string oUrl)
        {
            reactionName = Name;
            inputIdentifier = Cut(iUrl);
            outputIdentifier = Cut(oUrl);
        }

        public string GetProfit()
        {
            double profit = GetData(reqType.outputItems) - GetData(reqType.inputItems);
            return String.Format("{0:C}", profit);
        }

        public double GetData(reqType reqType)
        {
            string json = GetJson(reqType);
            JObject jObj = JObject.Parse(json);
            
            if (reqType == reqType.inputItems)
            {
                return jObj.SelectToken("totals.buy").Value<double>();
            }
            else
            {
                return jObj.SelectToken("totals.sell").Value<double>();
            }
        }

        private string GetJson(reqType reqType)
        {
            string reqIdentifier = null;

            if (reqType == reqType.inputItems)
            {
                reqIdentifier = inputIdentifier;
            }
            else if (reqType == reqType.outputItems)
            {
                reqIdentifier = outputIdentifier;
            }

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://evepraisal.com/a/" + reqIdentifier + ".json?live=yes");
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        private string Cut(string url)
        {
            string[] cUrl = url.Split('/');
            return cUrl[4];
        }

    }
}
