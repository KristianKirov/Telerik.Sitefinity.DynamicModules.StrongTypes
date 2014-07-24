using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.DynamicModules.Data;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;

namespace Telerik.Sitefinity.DynamicModules.StrongTypes.Core.Data
{
    public abstract class DynamicItemsRepositoryBase<T> : IDynamicItemsRepositoryBase where T : DynamicTypeBase, new()
    {
        private readonly DynamicModuleManager manager;
        private readonly Type itemType;
        private readonly IDynamicTypeFactory<T> itemsFactory;

        public DynamicItemsRepositoryBase()
        {
            this.manager = DynamicModuleManager.GetManager(this.ProviderName);
            this.itemType = TypeResolutionService.ResolveType(this.ItemTypeName);
            this.itemsFactory = this.ResolveItemsFactory();
        }

        public abstract string ItemTypeName { get; }

        protected DynamicModuleManager Manager
        {
            get
            {
                return this.manager;
            }
        }

        protected virtual string ProviderName
        {
            get
            {
                return string.Empty;
            }
        }

        protected Type ItemType
        {
            get
            {
                return this.itemType;
            }
        }

        protected virtual IDynamicTypeFactory<T> ResolveItemsFactory()
        {
            return new DynamicTypeFactory<T>(this.Manager, this.ItemType);
        }

        public T Create()
        {
            T newItem = this.itemsFactory.CreateItem();

            return newItem;
        }

        public T Create(Guid itemId)
        {
            return this.itemsFactory.CreateItem(itemId);
        }

        public void Delete(Guid itemId)
        {
            this.Manager.DeleteDataItem(this.ItemType, itemId);
        }

        public void Delete(T item)
        {
            this.RequireMasterItem(item.DynamicContent);
            this.Manager.DeleteDataItem(item.DynamicContent);
        }

        public void Publish(T item)
        {
            DynamicContent dynamicItem = item.DynamicContent;
            this.RequireMasterItem(dynamicItem);
            this.Manager.Lifecycle.Publish(dynamicItem);

            dynamicItem.SetWorkflowStatus(this.Manager.Provider.ApplicationName, "Published");
        }

        public void Unpublish(T item)
        {
            DynamicContent dynamicItem = item.DynamicContent;
            this.RequireMasterItem(dynamicItem);
            if (dynamicItem.GetWorkflowItemStatus() == "Published")
            {
                ILifecycleDataItem liveItem = this.manager.Lifecycle.GetLive(dynamicItem);
                if (liveItem != null)
                {
                    this.Manager.Lifecycle.Unpublish(liveItem);

                    dynamicItem.SetWorkflowStatus(this.Manager.Provider.ApplicationName, "Unpublished");
                }
            }
        }

        public void MarkAsDraft(T item)
        {
            DynamicContent dynamicItem = item.DynamicContent;
            this.RequireMasterItem(dynamicItem);
            dynamicItem.SetWorkflowStatus(this.Manager.Provider.ApplicationName, "Draft");
        }

        public void SchedulePublish(T item, DateTime publishDate)
        {
            DynamicContent dynamicItem = item.DynamicContent;
            this.RequireMasterItem(dynamicItem);
            this.Manager.Lifecycle.PublishWithSpecificDate(dynamicItem, DateTime.Now.AddMinutes(5));

            dynamicItem.SetWorkflowStatus(this.Manager.Provider.ApplicationName, "Scheduled");
        }

        private void RequireMasterItem(DynamicContent item)
        {
            if (item.Status != ContentLifecycleStatus.Master)
            {
                throw new InvalidOperationException("Required item status: Master");
            }
        }

        public void Commit()
        {
            using (new ElevatedModeRegion(this.Manager))
            {
                this.Manager.SaveChanges();
            }
        }

        protected IList<T> Typify(IQueryable<DynamicContent> dynamicItems)
        {
            return dynamicItems.ToList().Select(dc => this.Typify(dc)).ToList();
        }

        protected T Typify(DynamicContent dynamicContent)
        {
            return this.itemsFactory.BuildTypedItem(dynamicContent);
        }

        protected IQueryable<DynamicContent> GetUntypedPublishedItems()
        {
            return this.GetAllUntyped().Where(dc => dc.Status == ContentLifecycleStatus.Live && dc.Visible == true);
        }

        public IList<T> GetPublishedItems()
        {
            return this.Typify(this.GetUntypedPublishedItems());
        }

        protected IQueryable<DynamicContent> GetUntypedItemsForEdit()
        {
            return this.GetAllUntyped().Where(dc => dc.Status == ContentLifecycleStatus.Master);
        }

        public IList<T> GetItemsForEdit()
        {
            return this.Typify(this.GetUntypedItemsForEdit());
        }

        protected IQueryable<DynamicContent> GetAllUntyped()
        {
            return this.Manager.GetDataItems(this.ItemType);
        }

        public IList<T> GetAll()
        {
            return this.Typify(this.GetAllUntyped());
        }

        public T GetItemById(Guid id)
        {
            DynamicContent dynamicItem = this.GetAllUntyped().FirstOrDefault(dc => dc.Id == id);
            if (dynamicItem == null)
            {
                return default(T);
            }

            return this.Typify(dynamicItem);
        }

        public T GetItemByUrlName(string urlName)
        {
            DynamicContent dynamicItem = this.GetUntypedPublishedItems().FirstOrDefault(dc => dc.UrlName == urlName);
            if (dynamicItem == null)
            {
                return default(T);
            }

            return this.Typify(dynamicItem);
        }

        public IList<T> GetItemsByUrlNames(params string[] urlNames)
        {
            IQueryable<DynamicContent> dynamicItems = this.GetUntypedPublishedItems().Where(dc => urlNames.Contains((string)dc.UrlName));

            return this.Typify(dynamicItems);
        }

        public T GetItemFromUrl(string url, bool published)
        {
            string defaultUrl;
            DynamicContent dynamicItem = this.Manager.Provider.GetItemFromUrl(this.ItemType, url, published, out defaultUrl) as DynamicContent;
            if (dynamicItem == null)
            {
                return default(T);
            }

            return this.Typify(dynamicItem);
        }

        public T GetEditableItem(T originalItem)
        {
            if (originalItem.DynamicContent.Status == ContentLifecycleStatus.Master)
            {
                return originalItem;
            }

            DynamicContent masterItem = (DynamicContent)this.Manager.Lifecycle.GetMaster(originalItem.DynamicContent);

            return this.Typify(masterItem);
        }


        DynamicTypeBase IDynamicItemsRepositoryBase.Create()
        {
            return this.Create();
        }

        DynamicTypeBase IDynamicItemsRepositoryBase.Create(Guid itemId)
        {
            return this.Create(itemId);
        }

        public void Delete(DynamicTypeBase item)
        {
            this.Delete(item);
        }

        IList<DynamicTypeBase> IDynamicItemsRepositoryBase.GetAll()
        {
            return this.GetAll().Cast<DynamicTypeBase>().ToList();
        }

        public DynamicTypeBase GetEditableItem(DynamicTypeBase originalItem)
        {
            return this.GetEditableItem(originalItem);
        }

        DynamicTypeBase IDynamicItemsRepositoryBase.GetItemById(Guid id)
        {
            return this.GetItemById(id);
        }

        DynamicTypeBase IDynamicItemsRepositoryBase.GetItemByUrlName(string urlName)
        {
            return this.GetItemByUrlName(urlName);
        }

        IList<DynamicTypeBase> IDynamicItemsRepositoryBase.GetItemsByUrlNames(params string[] urlNames)
        {
            return this.GetItemsByUrlNames(urlNames).Cast<DynamicTypeBase>().ToList();
        }

        DynamicTypeBase IDynamicItemsRepositoryBase.GetItemFromUrl(string url, bool published)
        {
            return this.GetItemFromUrl(url, published);
        }

        IList<DynamicTypeBase> IDynamicItemsRepositoryBase.GetItemsForEdit()
        {
            return this.GetItemsForEdit().Cast<DynamicTypeBase>().ToList();
        }

        IList<DynamicTypeBase> IDynamicItemsRepositoryBase.GetPublishedItems()
        {
            return this.GetPublishedItems().Cast<DynamicTypeBase>().ToList();
        }

        public void MarkAsDraft(DynamicTypeBase item)
        {
            this.MarkAsDraft(item);
        }

        public void Publish(DynamicTypeBase item)
        {
            this.Publish(item);
        }

        public void SchedulePublish(DynamicTypeBase item, DateTime publishDate)
        {
            this.SchedulePublish(item, publishDate);
        }

        public void Unpublish(DynamicTypeBase item)
        {
            this.Unpublish(item);
        }
    }
}
