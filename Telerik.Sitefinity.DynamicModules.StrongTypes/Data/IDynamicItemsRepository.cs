using System;
using System.Collections.Generic;
namespace Telerik.Sitefinity.DynamicModules.StrongTypes.Data
{
    public interface IDynamicItemsRepositoryBase
    {
        void Commit();
        DynamicTypeBase Create();
        DynamicTypeBase Create(Guid itemId);
        void Delete(Guid itemId);
        void Delete(DynamicTypeBase item);
        IList<DynamicTypeBase> GetAll();
        DynamicTypeBase GetEditableItem(DynamicTypeBase originalItem);
        DynamicTypeBase GetItemById(Guid id);
        DynamicTypeBase GetItemByUrlName(string urlName);
        DynamicTypeBase GetItemFromUrl(string url, bool published);
        IList<DynamicTypeBase> GetItemsForEdit();
        IList<DynamicTypeBase> GetPublishedItems();
        string ItemTypeName { get; }
        void MarkAsDraft(DynamicTypeBase item);
        void Publish(DynamicTypeBase item);
        void SchedulePublish(DynamicTypeBase item, DateTime publishDate);
        void Unpublish(DynamicTypeBase item);
    }
}
