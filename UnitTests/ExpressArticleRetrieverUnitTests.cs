using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SentimentService.ContentRetrieval;
using System.Collections.Generic;
using HtmlAgilityPack;

namespace UnitTests
{
    [TestClass]
    public class ExpressArticleRetrieverUnitTests
    {
        [TestMethod]
        public void RetrieveArticlesTest()
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument homePage = web.Load(
                "http://www.trinidadexpress.com/news");

            ExpressArticleRetriever retriever = new ExpressArticleRetriever();
            Dictionary<string, WebNewsArticle> articles =
                retriever.RetrieveArticles();

            foreach (string title in articles.Keys)
            {
                Assert.IsTrue(homePage.DocumentNode.InnerHtml.Contains(
                    title));
                WebNewsArticle wna = articles[title];
                HtmlDocument article = web.Load(wna.Source);
                Assert.IsTrue(article.DocumentNode.InnerHtml.Contains(
                    wna.OriginalContent));
            }
        }
    }
}
