using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Moves_Online_Downloader
{
    public static class Extenction
    {

        public static List<HtmlAgilityPack.HtmlNode> Search(this HtmlAgilityPack.HtmlNode node, string attribute, string value)
        {
            List<HtmlAgilityPack.HtmlNode> listTemp = new List<HtmlAgilityPack.HtmlNode>();
            List<HtmlAgilityPack.HtmlNode> retNodeChields = new List<HtmlAgilityPack.HtmlNode>();
            if (node == null)
            {
                return retNodeChields;
            }
            if ((node.Attributes[attribute] != null) && (node.Attributes[attribute].Value.Equals(value)))
            {
                listTemp.Add(node);
                return listTemp;
            }
            for (int i = 0; i < node.ChildNodes.Count; i++)
            {
                List<HtmlAgilityPack.HtmlNode> retNodeChield = Search(node.ChildNodes[i], attribute, value);
                if (retNodeChield != null)
                {
                    retNodeChields.AddRange(retNodeChield);
                }
            }
            if (retNodeChields.Count() == 0)
            {
                retNodeChields = null;
            }
            return retNodeChields;
        }

        public static void AddRange<T>(this ConcurrentBag<T> @this, IEnumerable<T> toAdd)
        {
            toAdd.AsParallel().ForAll(t => @this.Add(t));
        }

        public static string DownloadStringHtmlPageFromUrl(this string url)
        {
            WebClient webClient = new WebClient();
            webClient.Proxy = null;
                string res = string.Empty;
                int i = 0;
                do
                {
                    try
                    {
                        res = webClient.DownloadString(url);
                    }
                    catch (WebException ex)
                    {
                        
                        res = string.Empty;
                    }

                } while (res.Equals(string.Empty) && 20 > i++);
                return res;
            }

        public static string[] Split(this string self, string splitBy)
        {
            return self.Split( new string[] { splitBy } , StringSplitOptions.None);
        }

        public static string Remove(this string self, string stringToRemove)
        {
            return self.Replace(stringToRemove, string.Empty);
        }

        public static string Join(this List<string> self, string joinBy)
        {
            return string.Join(joinBy, self);
        }

        public static string Join(this string[] self, string joinBy)
        {
            return string.Join(joinBy, self);
        }

        public static bool EqualsValue(this string self,string stringToCompare)
        {
            return self.ToLower().Equals(stringToCompare.ToLower());
        }

        public static string Join(this IEnumerable<string> self, string joinBy)
        {
            return string.Join(joinBy, self);
        }

        public static T FromJsonToObject<T>(this string self)
        {
            T obj = JsonConvert.DeserializeObject<T>(self);
            return obj;
        }
        
        public static string ToViewLength(this int len)
        {
            return (len % 60 == 0) ? $"{len / 60}h" :
                               (len < 60 ? $"{len % 60}min" : $"{len / 60}h {len % 60}min");
        }

        public static int ToComparableLength(this string len)
        {

            if (new [] {"h","min"}.All(x=>len.Contains(x)))
            {
                string[] splited = len.Split();
                return Int32.Parse(splited.First().Remove("h"))*60 +
                    Int32.Parse(splited.Last().Remove("min"));
            }
            else if (len.Contains("h"))
            {
                return Int32.Parse(len.Remove("h")) * 60;
            }else
            {
                return Int32.Parse(len.Remove("min"));
            }
        }
        

    }


}

