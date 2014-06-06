// Guids.cs
// MUST match guids.h
using System;

namespace Telerik.SitefinityStringTypesGenerator
{
    static class GuidList
    {
        public const string guidSitefinityStringTypesGeneratorPkgString = "249209d5-0fec-4938-894d-ab8f957122d7";
        public const string guidSitefinityStringTypesGeneratorCmdSetString = "d0556828-c29c-4932-a9fb-c7f31c37d720";
        public const string guidToolWindowPersistanceString = "51fcacf9-8d2b-4fdb-b569-e018354de0a7";

        public static readonly Guid guidSitefinityStringTypesGeneratorCmdSet = new Guid(guidSitefinityStringTypesGeneratorCmdSetString);
    };
}