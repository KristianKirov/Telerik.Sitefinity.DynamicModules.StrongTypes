using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.DynamicModules.Model;

namespace Telerik.Sitefinity.DynamicModules.StrongTypes.Data
{
    public interface IDynamicTypeConverter
    {
        T BuildTypedItem<T>(DynamicContent dynamicItem) where T : DynamicTypeBase, new();
    }
}
