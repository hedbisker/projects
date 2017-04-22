using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moves_Online_Downloader
{
    public class HelperFunctions
    {
        public static T[] FeachLimitsFromStr<T>(string valueStr, T defaultMin,T defaultMax,Func<string,string> runOn = null)
        {
            T[] valueArray = new T[2];
            if (valueStr.Contains("-"))
            {
                string[] splited = valueStr.Split("-");
                valueArray[1] = (T)Convert.ChangeType(runOn == null? splited.Last(): runOn(splited.Last()), typeof(T));
                valueArray[0] = (T)Convert.ChangeType(runOn == null ? splited.First() : runOn(splited.First()), typeof(T));
            }
            else
            {
                if (valueStr.Equals(string.Empty))
                {
                    valueArray[0] = defaultMin;
                    valueArray[1] = defaultMax;
                }
                else
                {
                    valueArray[0] = valueArray[1] = (T)Convert.ChangeType(runOn == null? valueStr: runOn(valueStr), typeof(T));
                }
            }
            return valueArray;
        }

    }
}
