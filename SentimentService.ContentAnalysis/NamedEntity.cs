using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentimentService.ContentAnalysis
{
    public class NamedEntity : TableEntity
    {
        public string EntityName { get; set; }
        public string EntityType { get; set; }
        public int Occurences { get; set; }
        public int TotalSentiment { get; set; }

        public NamedEntity()
        {

        }

        public NamedEntity(string type, string name, int occurs, int sent)
        {
            this.PartitionKey = type;
            this.RowKey = name;
            this.EntityName = name;
            this.EntityType = type;
            this.Occurences = occurs;
            this.TotalSentiment = sent;
        }
    }
}
