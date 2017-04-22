using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Moves_Online_Downloader
{
    public abstract class ICommonRobot
    {
        public abstract void Download(Action<string, ConcurrentBag<Movie>> done, string yearText, string categoryText, string subtittleText);

        protected int NormalizationLength(string len)
        {
            int lenInt = 0;
            if (Regex.IsMatch(len, @"^\d+\s*(h|hr|hour|hours) \d+\s*(m|min|minute|minutes)$", RegexOptions.Compiled | RegexOptions.IgnoreCase))
            {
                string[] lens = len.Split();
                lenInt = (Int32.Parse(Regex.Match(lens.First(), @"\d+").Value)*60)
                + (Int32.Parse(Regex.Match(lens.Last(), @"\d+").Value));

            }else if (Regex.IsMatch(len, @"^\d+\s*(m|min|minute|minutes)$", RegexOptions.Compiled | RegexOptions.IgnoreCase))
            {
                lenInt = (Int32.Parse(Regex.Match(len, @"\d+").Value));
            }
            return lenInt;
        }
        
    }
}
