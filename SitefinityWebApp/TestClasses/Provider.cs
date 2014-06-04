using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace SitefinityWebApp.TestClasses
{
    public class Provider
    {
        private readonly DynamicModuleManager manager;
        private readonly Type itemType;

        public Provider()
        {
            this.manager = DynamicModuleManager.GetManager(string.Empty);
            this.itemType = TypeResolutionService.ResolveType("Telerik.Sitefinity.DynamicTypes.Model.FullModule.FullItem");
        }

        protected IQueryable<DynamicContent> GetAllUntyped()
        {
            return this.manager.GetDataItems(this.itemType);
        }

        public IList<DataModel> GetAll()
        {
            return this.Typify(this.GetAllUntyped());
        }

        private IList<DataModel> Typify(IQueryable<DynamicContent> dynamicItems)
        {
            return dynamicItems.ToList().Select(dc => this.Typify(dc)).ToList();
        }

        private DataModel Typify(DynamicContent dynamicItem)
        {
            return new DataModel(dynamicItem);
        }
    }
}