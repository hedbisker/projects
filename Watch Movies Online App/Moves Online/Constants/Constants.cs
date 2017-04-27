using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moves_Online_Downloader
{
    public static class Data
    {
        public static HashSet<string> YearsLimit { get; set; }

        public static HashSet<string> LengthLimit { get; set; }

        public static HashSet<string> RateLimit { get; set; }

        static Data()
        {
            YearsLimit = new HashSet<string>();
            
            for (int year = 1900; year <= DateTime.Now.Year; year++)//specific year
            {
                YearsLimit.Add(Convert.ToString(year));
            }

            for (int yearLower = 1900; yearLower < DateTime.Now.Year; yearLower++)//lower & upper bounds
            {
                for (int yearUpper = yearLower + 1; yearUpper <= DateTime.Now.Year; yearUpper++)
                {
                    YearsLimit.Add(Convert.ToString(yearLower) + "-"+Convert.ToString(yearUpper));
                }
            }

            LengthLimit = new HashSet<string>();
            for (int i = 1; i < 600; i++)
            {
                for (int j = i+1; j <= 600; j++)
                {
                    LengthLimit.Add($"{i.ToViewLength()}-{j.ToViewLength()}");
                }
            }

            RateLimit = new HashSet<string>();
            for (double i = 0; i < 10; i += 0.1)
            {
                string s = $"{i.ToString("##.#")}";
                if (i < 1)
                {
                    RateLimit.Add($"0{s}");
                }
                for (double j = i + 0.1; j <= 10; j += 0.1)
                {
                    string iStr = $"{i.ToString("##.#")}";
                    string jStr = $"{j.ToString("##.#")}";
                    if (i < 1)
                    {
                        iStr = $"0{iStr}";
                    }
                    if (j < 1)
                    {
                        jStr = $"0{jStr}";
                    }
                    RateLimit.Add($"{iStr}-{jStr}");
                }
            }
        }

        public static readonly HashSet<string> moviesCategory = new HashSet<string>{
            "action", "thriller", "mystery", "comedy", "romance", "adventure", "horror",
            "sci-fi", "fantasy", "animation", "biography", "crime", "drama", "family",
            "history", "music", "musical", "sport", "war", "western", "documentary"
        };

        public static readonly HashSet<string> subtittles = new HashSet<string> {"English",
            "Afrikaans","Albanian","Arabic","Armenian","Basque","Bengali","Bosnian",
            "Breton","Bulgarian","Burmese","Catalan", "Chinese","Chinese(simplified)",
            "Chinese(traditional)","Chinese + English","Chinese bilingual","Croatian",
            "Czech","Danish","Dutch","English","Estonian","Finnish","French","Georgian",
            "German","Greek","Hebrew", "Hindi","Hungarian","Icelandic","Indonesian","Thai",
            "Italian","Japanese","Khmer","Korean","Latvian","Lithuanian","Macedonian","Urdu",
            "Malay","Malayalam","Norwegian","Persian","Polish","Portuguese","Portuguese-BR",
            "Romanian","Russian","Serbian", "Sinhalese","Slovak","Slovenian","Spanish",
            "Swahili","Swedish","Tagalog","Tamil","Telugu","Turkish","Ukrainian","Vietnamese"};

    }
}

