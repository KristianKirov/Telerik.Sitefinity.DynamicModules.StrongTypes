using SitefinityWebApp.FullModule;
using SitefinityWebApp.FullModule.Data;
using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.GeoLocations.Model;
using Telerik.Sitefinity.Locations;
using Telerik.Sitefinity.Locations.Configuration;
using System.Linq;
using Telerik.Sitefinity.News.Model;
using SitefinityWebApp.Relatedmodule;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Modules.News;
using SitefinityWebApp.Relatedmodule.Data;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Taxonomies;
using SitefinityWebApp.Data;
using Telerik.Sitefinity.DynamicModules.StrongTypes.Data;
using Telerik.Sitefinity.DynamicModules.StrongTypes;

namespace SitefinityWebApp
{
    public partial class Tests : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void TestGetButton_Click(object sender, EventArgs e)
        {
            IDynamicItemsRepositoryBase repo = RepositoryResolver.Resolve(FullItem.DYNAMIC_TYPE_NAME);
            repo.GetPublishedItems();

            //FullItemDataRepository dataRepository = new FullItemDataRepository();
            IList<DynamicTypeBase> publishedItems = repo.GetAll();
            int i = 0;
            foreach (var fullItem in publishedItems)
            {
                i++;
            }
        }

        protected void TestCreateButton_Click(object sender, EventArgs e)
        {
            FullItemDataRepository dataRepository = new FullItemDataRepository();

            //FullItem item1 = this.CreateItem("new 11 :)", dataRepository);
            //FullItem item2 = this.CreateItem("new 22 :)", dataRepository);

            //dataRepository.Commit();

            //dataRepository.Publish(item1);
            //dataRepository.Commit();

            FullItem item = dataRepository.GetItemById(new Guid("42dec287-ae97-616c-accb-ff00009b0b8b"));
            dataRepository.Publish(item);
            dataRepository.Commit();
            
        }

        private FullItem CreateItem(string title, FullItemDataRepository dataRepository)
        {
            FullItem newItem = dataRepository.Create();

            newItem.ShortText = title;
            newItem.LongText = title;
            newItem.SetChoicesSingle("2");
            newItem.SetChoicesMultiple(new string[] { "1", "2" });
            newItem.YesNo = true;
            newItem.DateTime = new DateTime(1991, 12, 23);
            newItem.Number = 999;
            newItem.GuidField = new Guid("22200000-0000-0000-0000-000000000222");
            newItem.ArrayOfGuids = new Guid[] { newItem.GuidField, newItem.GuidField, newItem.GuidField };

            newItem.Address = this.GetAddress();

            newItem.AddRelatedDynamicModuleMultiple(this.GetRelatedItem(0));
            newItem.AddRelatedDynamicModuleMultiple(this.GetRelatedItem(1));

            newItem.SetRelatedDynamicModuleSingle(this.GetRelatedItem(2));

            newItem.AddNewsRelatedDataMultiple(this.GetNewItem(0));
            newItem.AddNewsRelatedDataMultiple(this.GetNewItem(1));

            newItem.SetNewsRelatedDataSingle(this.GetNewItem(2));

            newItem.SetFirstClassification(this.GetFirstTaxonItem(0).Id);

            newItem.AddSecondClassification(this.GetSecondTaxonItem(1).Id, this.GetSecondTaxonItem(2).Id);

            newItem.AddMultipleImageField(this.GetImage(0));
            newItem.AddMultipleImageField(this.GetImage(1));

            newItem.SetSingleImageField(this.GetImage(3));

            newItem.AddMultipleVideoField(this.GetVideo(0));
            newItem.AddMultipleVideoField(this.GetVideo(1));

            newItem.SetSingleVideoField(this.GetVideo(3));

            newItem.AddMultipleFileField(this.GetDocument(0));
            newItem.AddMultipleFileField(this.GetDocument(1));

            newItem.SetSingleFileField(this.GetDocument(3));

            return newItem;
        }

        protected void TestUpdate_Click(object sender, EventArgs e)
        {
            FullItemDataRepository dataRepository = new FullItemDataRepository();

            FullItem item = dataRepository.GetItemById(new Guid("42dec287-ae97-616c-accb-ff00009b0b8b"));

            item.ShortText = "new 22 ;) updated";
            item.LongText = "uhaaaa";
            item.SetChoicesSingle("1");
            item.SetChoicesMultiple(new string[] { "2" });
            item.YesNo = false;
            item.DateTime = new DateTime(2000, 1, 1);
            item.GuidField = new Guid("32200000-0000-0000-0000-000000000223");
            item.ArrayOfGuids = new Guid[] { item.GuidField, item.GuidField, item.GuidField };

            Address oldAddress = item.Address;
            oldAddress.City = "Kanzas";
            item.Address = oldAddress;

            ////////

            item.ClearRelatedDynamicModuleMultiple();
            item.AddRelatedDynamicModuleMultiple(this.GetRelatedItem(2));

            item.SetRelatedDynamicModuleSingle(this.GetRelatedItem(0));

            item.AddNewsRelatedDataMultiple(this.GetNewItem(2));

            item.SetNewsRelatedDataSingle(this.GetNewItem(2));

            item.SetFirstClassification(this.GetFirstTaxonItem(1).Id);

            item.ClearSecondClassification();
            item.AddSecondClassification(this.GetSecondTaxonItem(0).Id, this.GetSecondTaxonItem(1).Id);

            item.ClearMultipleImageField();
            item.AddMultipleImageField(this.GetImage(2));
            item.AddMultipleImageField(this.GetImage(3));

            item.SetSingleImageField(this.GetImage(1));

            item.AddMultipleVideoField(this.GetVideo(2));

            item.ClearSingleVideoField();

            ///////

            dataRepository.Commit();

            dataRepository.Publish(item);
            dataRepository.Commit();
        }

        private Address GetAddress()
        {
            Address address = new Address();
            CountryElement addressCountry = Config.Get<LocationsConfig>().Countries.Values.First(x => x.Name == "United States");
            address.CountryCode = addressCountry.IsoCode;
            address.StateCode = addressCountry.StatesProvinces.Values.First().Abbreviation;
            address.City = "Some City";
            address.Street = "Some Street";
            address.Zip = "12345";

            return address;
        }

        private NewsItem GetNewItem(int index)
        {
            if (this.newsItems == null)
            {
                this.newsItems = NewsManager.GetManager().GetNewsItems().Where(i => i.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master).ToList();
            }

            return this.newsItems[index];
        }

        private RelatedItem GetRelatedItem(int index)
        {
            if (this.relatedItems == null)
            {
                RelatedItemDataRepository repo = new RelatedItemDataRepository();
                this.relatedItems = repo.GetItemsForEdit();
            }

            return this.relatedItems[index];
        }

        private Image GetImage(int index)
        {
            if (this.images == null)
            {
                this.images = LibrariesManager.GetManager().GetImages().Where(i => i.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master).ToList();
            }

            return this.images[index];
        }

        private Video GetVideo(int index)
        {
            if (this.videos == null)
            {
                this.videos = LibrariesManager.GetManager().GetVideos().Where(i => i.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master).ToList();
            }

            return this.videos[index];
        }

        private Document GetDocument(int index)
        {
            if (this.documents == null)
            {
                this.documents = LibrariesManager.GetManager().GetDocuments().Where(i => i.Status == Telerik.Sitefinity.GenericContent.Model.ContentLifecycleStatus.Master).ToList();
            }

            return this.documents[index];
        }

        private Taxon GetFirstTaxonItem(int index)
        {
            if (this.firstTaxonItems == null)
            {
                this.firstTaxonItems = TaxonomyManager.GetManager().GetTaxa<FlatTaxon>().Where(t => t.Taxonomy.Name == "FirstClassification").ToList();
            }

            return this.firstTaxonItems[index];
        }

        private Taxon GetSecondTaxonItem(int index)
        {
            if (this.secondTaxonItems == null)
            {
                this.secondTaxonItems = TaxonomyManager.GetManager().GetTaxa<HierarchicalTaxon>().Where(t => t.Taxonomy.Name == "SecondClassification").ToList();
            }

            return this.secondTaxonItems[index];
        }

        private IList<NewsItem> newsItems;
        private IList<RelatedItem> relatedItems;
        private IList<Image> images;
        private IList<Video> videos;
        private IList<Document> documents;
        private IList<FlatTaxon> firstTaxonItems;
        private IList<HierarchicalTaxon> secondTaxonItems;
    }
}