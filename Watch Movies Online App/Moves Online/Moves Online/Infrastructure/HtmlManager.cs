using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moves_Online_Downloader
{
    public class HtmlManager
    {
        public string AlbumsSectionClassName { get; set; }
        public HtmlManager(string yearText, string categoryText)
        {
            init(yearText, categoryText);
        }
        private void init(string yearText, string categoryText)
        {
            if (yearText.Length == 0 && categoryText.Length == 0)
            {
                AlbumsSectionClassName = "main main_content_without_one_sidebar";
            }
            else
            {
                AlbumsSectionClassName = "main main_content_full";
            }
        }
    }
}
