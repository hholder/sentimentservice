using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentimentService.ContentRetrieval
{
    public abstract class TextContent
    {
        public string Title { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateRetrieved { get; set; }
        public string Content { get; set; }

        protected TextContent(string title, DateTime created,
            DateTime retrieved, string content)
        {
            this.Title = title;
            this.DateCreated = created;
            this.DateRetrieved = retrieved;
            this.Content = content;
        }
    }
}
