using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.DynamicModules.StrongTypes.Core.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.RelatedData;

namespace Telerik.Sitefinity.DynamicModules.StrongTypes.Core
{
    public abstract class DynamicTypeBase
    {
        protected internal DynamicContent DynamicContent { get; private set; }

        protected internal DynamicTypeBase()
        {
        }

        internal void Initialize(DynamicContent dynamicContent)
        {
            this.DynamicContent = dynamicContent;
        }

        private void RequireEditMode()
        {
            if (this.DynamicContent.Status != ContentLifecycleStatus.Master)
            {
                throw new InvalidOperationException("Can edin only Master items. To get editable item use DataRepository.GetEditableItem.");
            }
        }

        protected internal T GetDynamicFieldValue<T>(string fieldName)
        {
            return this.DynamicContent.GetValue<T>(fieldName);
        }

        protected void SetDynamicFieldValue(string fieldName, object fieldValue)
        {
            this.RequireEditMode();

            this.DynamicContent.SetValue(fieldName, fieldValue);
        }

        protected void AddClassificationValue(string fieldName, params Guid[] taxonId)
        {
            this.RequireEditMode();

            this.DynamicContent.Organizer.AddTaxa(fieldName, taxonId);
        }

        protected void ClearClassificationField(string fieldName)
        {
            this.RequireEditMode();

            this.DynamicContent.Organizer.Clear(fieldName);
        }

        protected void AddImageValue(string fieldName, Guid imageId, string librariesProviderName = "")
        {
            this.RequireEditMode();

            this.DynamicContent.AddImage(fieldName, imageId, librariesProviderName);
        }

        protected void AddImageValue(string fieldName, Image image)
        {
            this.RequireEditMode();

            this.DynamicContent.AddImage(fieldName, image);
        }

        protected void ClearImages(string fieldName)
        {
            this.RequireEditMode();

            this.DynamicContent.ClearImages(fieldName);
        }

        protected void AddVideoValue(string fieldName, Guid videoId, string librariesProviderName = "")
        {
            this.RequireEditMode();

            this.DynamicContent.AddVideo(fieldName, videoId, librariesProviderName);
        }

        protected void AddVideoValue(string fieldName, Video video)
        {
            this.RequireEditMode();

            this.DynamicContent.AddVideo(fieldName, video);
        }

        protected void ClearVideos(string fieldName)
        {
            this.RequireEditMode();

            this.DynamicContent.ClearVideos(fieldName);
        }

        protected void AddFileValue(string fieldName, Guid fileId, string librariesProviderName = "")
        {
            this.RequireEditMode();

            this.DynamicContent.AddFile(fieldName, fileId, librariesProviderName);
        }

        protected void AddFileValue(string fieldName, Document file)
        {
            this.RequireEditMode();

            this.DynamicContent.AddFile(fieldName, file);
        }

        protected void ClearFiles(string fieldName)
        {
            this.RequireEditMode();

            this.DynamicContent.ClearFiles(fieldName);
        }

        protected IList<T> GetRelatedDynamicItems<T>(string fieldName) where T : DynamicTypeBase, new()
        {
            IQueryable<DynamicContent> relatedItems = this.DynamicContent.GetRelatedItems<DynamicContent>(fieldName);
            if (relatedItems == null)
            {
                return null;
            }

            IDynamicTypeConverter converter = new DynamicTypeConverter();

            return relatedItems.Select(dc => converter.BuildTypedItem<T>(dc)).ToList();
        }

        protected T GetRelatedDynamicItem<T>(string fieldName) where T : DynamicTypeBase, new()
        {
            DynamicContent relatedItem = this.DynamicContent.GetRelatedItems<DynamicContent>(fieldName).SingleOrDefault();
            if (relatedItem == null)
            {
                return null;
            }

            IDynamicTypeConverter converter = new DynamicTypeConverter();

            return converter.BuildTypedItem<T>(relatedItem);
        }

        protected IList<T> GetRelatedKnowTypeItems<T>(string fieldName) where T : IDataItem
        {
            IQueryable<T> relatedItems = this.DynamicContent.GetRelatedItems<T>(fieldName);
            if (relatedItems == null)
            {
                return null;
            }

            return relatedItems.ToList();
        }

        protected T GetRelatedKnowTypeItem<T>(string fieldName) where T : IDataItem
        {
            T relatedItem = this.DynamicContent.GetRelatedItems<T>(fieldName).SingleOrDefault();
            if (relatedItem == null)
            {
                return default(T);
            }

            return (T)relatedItem;
        }

        protected void AddRelatedItem(string fieldName, DynamicTypeBase relatedItem)
        {
            this.RequireEditMode();

            this.AddRelatedItem(fieldName, relatedItem.DynamicContent);
        }

        protected void SetRelatedItem(string fieldName, DynamicTypeBase relatedItem)
        {
            this.RequireEditMode();

            this.SetRelatedItem(fieldName, relatedItem.DynamicContent);
        }

        protected void AddRelatedItem(string fieldName, IDataItem relatedItem)
        {
            this.RequireEditMode();

            ILifecycleDataItem lifecycleItem = relatedItem as ILifecycleDataItem;
            if (lifecycleItem != null)
            {
                if (lifecycleItem.Status != ContentLifecycleStatus.Master)
                {
                    throw new InvalidOperationException("Relate only Master items. After publish relations to the Live items will be automatically created.");
                }
            }

            this.DynamicContent.CreateRelation(relatedItem, fieldName);
        }

        protected void SetRelatedItem(string fieldName, IDataItem relatedItem)
        {
            this.RequireEditMode();

            this.DynamicContent.DeleteRelations(fieldName);
            this.DynamicContent.CreateRelation(relatedItem, fieldName);
        }

        protected void ClearRelatedItems(string fieldName)
        {
            this.RequireEditMode();

            this.DynamicContent.DeleteRelations(fieldName);
        }

        public Guid Id
        {
            get
            {
                return this.DynamicContent.Id;
            }
        }

        public Guid SystemParentId
        {
            get
            {
                return this.DynamicContent.SystemParentId;
            }
        }

        public string SystemParentType
        {
            get
            {
                return this.DynamicContent.SystemParentType;
            }
        }

        public Lstring UrlName
        {
            get
            {
                return this.DynamicContent.UrlName;
            }
            set
            {
                this.RequireEditMode();

                this.DynamicContent.SetString("UrlName", value);
            }
        }

        public bool Visible
        {
            get
            {
                return this.DynamicContent.Visible;
            }
        }

        public string SystemUrl
        {
            get
            {
                return this.DynamicContent.SystemUrl;
            }
        }

        public ContentLifecycleStatus Status
        {
            get
            {
                return this.DynamicContent.Status;
            }
        }

        public DateTime PublicationDate
        {
            get
            {
                return this.DynamicContent.PublicationDate;
            }
        }

        public DateTime DateCreated
        {
            get
            {
                return this.DynamicContent.DateCreated;
            }
        }

        public Lstring ApprovalWorkflowState
        {
            get
            {
                return this.DynamicContent.ApprovalWorkflowState;
            }
        }

        public string Author
        {
            get
            {
                return this.DynamicContent.Author;
            }
        }

        public DateTime LastModified
        {
            get
            {
                return this.DynamicContent.LastModified;
            }
        }

        public Guid LastModifiedBy
        {
            get
            {
                return this.DynamicContent.LastModifiedBy;
            }
        }

        public Guid OriginalContentId
        {
            get
            {
                return this.DynamicContent.OriginalContentId;
            }
        }

        protected void SyncUrlNameWithMainShortTextFieldIfRequired(string oldValue, string newValue)
        {
            this.RequireEditMode();

            string currentUrlNameValue = this.UrlName.Value;
            if (string.IsNullOrEmpty(currentUrlNameValue) || this.TransformToUrl(oldValue) == currentUrlNameValue) // if url is not set or is synced with the main short text field, sync the again
            {
                this.UrlName = this.TransformToUrl(newValue);
            }
        }

        private string TransformToUrl(string text)
        {
            return Regex.Replace(text.ToLowerInvariant(), @"[^\w\-\!\$\'\(\)\=\@\d_]+", "-");
        }

        protected abstract string MainShortTextFieldName { get; }

        public Lstring GetMainShortTextFieldValue()
        {
            return this.GetDynamicFieldValue<Lstring>(this.MainShortTextFieldName);
        }
    }
}
