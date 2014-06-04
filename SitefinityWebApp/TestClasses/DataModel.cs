using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.News.Model;
using Telerik.Sitefinity.RelatedData;

namespace SitefinityWebApp.TestClasses
{
    public class DataModel
    {
        private DynamicContent dynamicItem;

        public DataModel(DynamicContent dynamicItem)
        {
            this.dynamicItem = dynamicItem;
        }

        public IList<NewsItem> RelatedNews
        {
            get
            {
                return dynamicItem.GetRelatedItems<NewsItem>("NewsRelatedDataMultiple").ToList();
            }
        }

        public NewsItem RelatedNewsItem
        {
            get
            {
                return dynamicItem.GetRelatedItems<NewsItem>("NewsRelatedDataSingle").SingleOrDefault();
            }
        }
    }
}