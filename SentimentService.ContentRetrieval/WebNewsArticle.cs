using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentimentService.ContentRetrieval
{
    public class WebNewsArticle : TextContent
    {
        public string Source { get; private set; }
        public string OriginalContent { get; private set; }

        public WebNewsArticle(string src, string org, string title,
            DateTime created, DateTime retrieved, string content)
            : base(title, created, retrieved, content)
        {
            this.Source = src;
            this.OriginalContent = org;
        }
    }
}
