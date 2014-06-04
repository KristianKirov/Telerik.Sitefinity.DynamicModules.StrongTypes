using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Security;

namespace Telerik.Sitefinity.DynamicModules.StrongTypes.Data
{
    public class DynamicTypeFactory<T> : IDynamicTypeFactory<T> where T : DynamicTypeBase, new()
    {
        private readonly DynamicModuleManager manager;
        private readonly Type itemType;
        private readonly IDynamicTypeConverter converter;

        public DynamicTypeFactory(DynamicModuleManager manager, Type itemType)
        {
            this.manager = manager;
            this.itemType = itemType;
            this.converter = new DynamicTypeConverter();
        }

        public T CreateItem()
        {
            DynamicContent dynamicItem = this.CreateItemInternal(null);

            T newItem = this.BuildTypedItem(dynamicItem);

            return newItem;
        }

        public T CreateItem(Guid id)
        {
            DynamicContent dynamicItem = this.CreateItemInternal(id);
            
            T newItem = this.BuildTypedItem(dynamicItem);

            return newItem;
        }

        private DynamicContent CreateItemInternal(Guid? id)
        {
            DynamicContent newItem = null;
            if (id != null)
            {
                newItem = this.manager.CreateDataItem(this.itemType, id.Value, this.manager.Provider.ApplicationName);
            }
            else
            {
                newItem = this.manager.CreateDataItem(this.itemType);
            }
            
            newItem.Owner = SecurityManager.GetCurrentUserId();
            newItem.PublicationDate = DateTime.UtcNow;
            newItem.SetWorkflowStatus(this.manager.Provider.ApplicationName, "Draft");

            return newItem;
        }

        public T BuildTypedItem(DynamicContent dynamicItem)
        {
            return converter.BuildTypedItem<T>(dynamicItem);
        }
    }
}