using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentimentService.ContentRetrieval
{
    public class ExpressArticleRetriever
    {
        private string home = "http://www.trinidadexpress.com/news";
        private string dateFormat = "MMM d, yyyy hh:mm tt";

        public ExpressArticleRetriever()
        {
        }

        public Dictionary<string, WebNewsArticle> RetrieveArticles()
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument homePage = web.Load(home);
            HtmlNode homePageBody = homePage.DocumentNode.
                Element("html").Element("body");

            List<HtmlAttribute> articleLinks = GetArticleLinks(homePageBody);
            return GetArticles(articleLinks);
        }

        private List<HtmlAttribute> GetArticleLinks(HtmlNode homePage)
        {
            List<HtmlAttribute> links = new List<HtmlAttribute>();
            foreach (HtmlNode node in homePage.DescendantsAndSelf("a"))
            {
                HtmlAttribute href = node.Attributes["href"];
                if (href != null && href.Value.Contains("/news/"))
                {
                    HtmlAttribute title = node.Attributes["title"];
                    if (title != null)
                    {
                        links.Add(href);
                    }
                }
            }
            return links;
        }

        private Dictionary<string, WebNewsArticle> GetArticles(
            List<HtmlAttribute> links)
        {
            HtmlWeb web = new HtmlWeb();
            Dictionary<string, WebNewsArticle> articles =
                new Dictionary<string, WebNewsArticle>();

            foreach (HtmlAttribute attr in links)
            {
                HtmlDocument article = web.Load(attr.Value);
                HtmlNode articleBody = article.DocumentNode.Element(
                    "html").Element("body");
                string title = GetArticleTitle(articleBody);
                if (title != null && !articles.ContainsKey(title))
                {
                    DateTime created = GetArticleCreationDate(articleBody);
                    string text = GetArticleText(articleBody);
                    WebNewsArticle wna = new WebNewsArticle(attr.Value,
                        articleBody.InnerHtml.Trim(), title, created,
                        DateTime.Now, text);
                    articles.Add(title, wna);
                }
            }

            return articles;
        }

        private DateTime GetArticleCreationDate(HtmlNode articleBody)
        {
            DateTime articleCreated = new DateTime();
            foreach (HtmlNode dnode in
                articleBody.Descendants("span"))
            {
                HtmlAttribute classAtt = dnode.Attributes["class"];
                if (classAtt != null &&
                    classAtt.Value.Equals("createdate"))
                {
                    string dateStr = dnode.InnerText.Replace(
                        "Story Created:", "");
                    dateStr = dateStr.Replace("ECT", "").
                        Replace("at ", "").Trim();
                    if (!DateTime.TryParseExact(dateStr,
                        dateFormat, null, DateTimeStyles.None,
                        out articleCreated))
                    {
                        articleCreated = DateTime.Now;
                    }
                }
            }
            return articleCreated;
        }

        private string GetArticleTitle(HtmlNode articleBody)
        {
            foreach (HtmlNode node in articleBody.Descendants("h1"))
            {
                HtmlAttribute classAtt = node.Attributes["class"];
                if (classAtt != null &&
                    classAtt.Value.Equals("title"))
                {
                    return node.InnerText.Trim();
                }
            }
            
            return null;
        }

        private string GetArticleText(HtmlNode articleBody)
        {
            foreach (HtmlNode anode in articleBody.Descendants("div"))
            {
                HtmlAttribute classAtt = anode.Attributes["class"];
                if (classAtt != null &&
                    classAtt.Value.Equals("storybody"))
                {
                    return anode.InnerText.Trim();
                }
            }
            return "";
        }
    }
}
