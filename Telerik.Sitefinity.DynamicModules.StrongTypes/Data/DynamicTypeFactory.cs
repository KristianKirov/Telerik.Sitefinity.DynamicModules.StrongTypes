using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.DynamicModules.Model;

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
            DynamicContent dynamicItem = this.manager.CreateDataItem(this.itemType);

            T newItem = this.BuildTypedItem(dynamicItem);

            return newItem;
        }

        public T CreateItem(Guid id)
        {
            DynamicContent dynamicItem = this.manager.CreateDataItem(this.itemType);

            T newItem = this.BuildTypedItem(dynamicItem);

            return newItem;
        }

        public T BuildTypedItem(DynamicContent dynamicItem)
        {
            return converter.BuildTypedItem<T>(dynamicItem);
        }
    }
}