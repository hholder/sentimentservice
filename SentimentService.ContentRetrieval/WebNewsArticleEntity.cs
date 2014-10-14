using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentimentService.ContentRetrieval
{
    public class WebNewsArticleEntity : TableEntity
    {
        public string Title { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateRetrieved { get; set; }
        public string Content { get; set; }
        public string Source { get; set; }

        public WebNewsArticleEntity()
        {

        }
        public WebNewsArticleEntity(string partitionKey, 
            WebNewsArticle article)
        {
            this.PartitionKey = partitionKey;
            this.RowKey = article.Title;
            this.Title = article.Title;
            this.DateCreated = article.DateCreated;
            this.DateRetrieved = article.DateRetrieved;
            this.Content = article.Content;
            this.Source = article.Source;
        }
    }
}
