using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.DynamicModules.Model;

namespace Telerik.Sitefinity.DynamicModules.StrongTypes.Core.Data
{
    public class DynamicTypeConverter : IDynamicTypeConverter
    {
        public T BuildTypedItem<T>(DynamicContent dynamicItem) where T : DynamicTypeBase, new()
        {
            T newItem = new T();
            newItem.Initialize(dynamicItem);

            return newItem;
        }
    }
}
