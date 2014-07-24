using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.DynamicModules.Model;

namespace Telerik.Sitefinity.DynamicModules.StrongTypes.Core.Data
{
    public interface IDynamicTypeFactory<T> where T : DynamicTypeBase, new()
    {
        T CreateItem();
        T CreateItem(Guid id);
        T BuildTypedItem(DynamicContent dynamicItem);
    }
}
