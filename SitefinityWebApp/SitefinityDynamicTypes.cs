using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.GeoLocations.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.DynamicModules.StrongTypes;
using Telerik.Sitefinity.DynamicModules.StrongTypes.Data;

namespace SitefinityWebApp
{
    namespace FullModule
    {
        public class FullItem : DynamicTypeBase
        {
            public const string MAIN_SHORT_TEXT_FIELD_NAME = "ShortText";
            public const string DYNAMIC_TYPE_NAME = "Telerik.Sitefinity.DynamicTypes.Model.FullModule.FullItem";
            
            public FullItem()
            {
            }

            private const string SHORTTEXT_FIELD_NAME = "ShortText";
            public Lstring ShortText
            {
                get
                {
                                        return this.GetDynamicFieldValue<Lstring>(FullItem.SHORTTEXT_FIELD_NAME);
                                }
                set
                {
                    this.SetDynamicFieldValue(FullItem.SHORTTEXT_FIELD_NAME, value.Value);
                }
                }

            private const string LONGTEXT_FIELD_NAME = "LongText";
            public Lstring LongText
            {
                get
                {
                                        return this.GetDynamicFieldValue<Lstring>(FullItem.LONGTEXT_FIELD_NAME);
                                }
                set
                {
                    this.SetDynamicFieldValue(FullItem.LONGTEXT_FIELD_NAME, value.Value);
                }
                }

            private const string CHOICESSINGLE_FIELD_NAME = "ChoicesSingle";
            public ChoiceOption ChoicesSingle
            {
                get
                {
                                        return this.GetDynamicFieldValue<ChoiceOption>(FullItem.CHOICESSINGLE_FIELD_NAME);
                                }
                }

    public void SetChoicesSingle(string selectedOption)
    {
        this.SetDynamicFieldValue(FullItem.CHOICESSINGLE_FIELD_NAME, selectedOption);
    }
            private const string CHOICESMULTIPLE_FIELD_NAME = "ChoicesMultiple";
            public ChoiceOption[] ChoicesMultiple
            {
                get
                {
                                        return this.GetDynamicFieldValue<ChoiceOption[]>(FullItem.CHOICESMULTIPLE_FIELD_NAME);
                                }
                }

    public void SetChoicesMultiple(string[] selectedOptions)
    {
        this.SetDynamicFieldValue(FullItem.CHOICESMULTIPLE_FIELD_NAME, selectedOptions);
    }
            private const string YESNO_FIELD_NAME = "YesNo";
            public bool YesNo
            {
                get
                {
                                        return this.GetDynamicFieldValue<bool>(FullItem.YESNO_FIELD_NAME);
                                }
                set
                {
                    this.SetDynamicFieldValue(FullItem.YESNO_FIELD_NAME, value);
                }
                }

            private const string DATETIME_FIELD_NAME = "DateTime";
            public DateTime DateTime
            {
                get
                {
                                        return this.GetDynamicFieldValue<DateTime>(FullItem.DATETIME_FIELD_NAME);
                                }
                set
                {
                    this.SetDynamicFieldValue(FullItem.DATETIME_FIELD_NAME, value);
                }
                }

            private const string NUMBER_FIELD_NAME = "Number";
            public decimal Number
            {
                get
                {
                                        return this.GetDynamicFieldValue<decimal>(FullItem.NUMBER_FIELD_NAME);
                                }
                set
                {
                    this.SetDynamicFieldValue(FullItem.NUMBER_FIELD_NAME, value);
                }
                }

            private const string ADDRESS_FIELD_NAME = "Address";
            public Address Address
            {
                get
                {
                                        return this.GetDynamicFieldValue<Address>(FullItem.ADDRESS_FIELD_NAME);
                                }
                set
                {
                    this.SetDynamicFieldValue(FullItem.ADDRESS_FIELD_NAME, value);
                }
                }

            private const string SINGLEIMAGEFIELD_FIELD_NAME = "SingleImageField";
            public ContentLink[] SingleImageField
            {
                get
                {
                                        return this.GetDynamicFieldValue<ContentLink[]>(FullItem.SINGLEIMAGEFIELD_FIELD_NAME);
                                }
                }

    public void SetSingleImageField(Guid imageId, string librariesProviderName = "")
    {
    this.ClearSingleImageField();
    this.AddImageValue(FullItem.SINGLEIMAGEFIELD_FIELD_NAME, imageId, librariesProviderName);
    }

    public void SetSingleImageField(Image image)
    {
    this.ClearSingleImageField();
    this.AddImageValue(FullItem.SINGLEIMAGEFIELD_FIELD_NAME, image);
    }

    public void ClearSingleImageField()
    {
        this.ClearImages(FullItem.SINGLEIMAGEFIELD_FIELD_NAME);
    }
            private const string MULTIPLEIMAGEFIELD_FIELD_NAME = "MultipleImageField";
            public ContentLink[] MultipleImageField
            {
                get
                {
                                        return this.GetDynamicFieldValue<ContentLink[]>(FullItem.MULTIPLEIMAGEFIELD_FIELD_NAME);
                                }
                }

    public void AddMultipleImageField(Guid imageId, string librariesProviderName = "")
    {
    this.AddImageValue(FullItem.MULTIPLEIMAGEFIELD_FIELD_NAME, imageId, librariesProviderName);
    }

    public void AddMultipleImageField(Image image)
    {
    this.AddImageValue(FullItem.MULTIPLEIMAGEFIELD_FIELD_NAME, image);
    }

    public void ClearMultipleImageField()
    {
        this.ClearImages(FullItem.MULTIPLEIMAGEFIELD_FIELD_NAME);
    }
            private const string SINGLEVIDEOFIELD_FIELD_NAME = "SingleVideoField";
            public ContentLink[] SingleVideoField
            {
                get
                {
                                        return this.GetDynamicFieldValue<ContentLink[]>(FullItem.SINGLEVIDEOFIELD_FIELD_NAME);
                                }
                }

    public void SetSingleVideoField(Guid videoId, string librariesProviderName = "")
    {
    this.ClearSingleVideoField();
    this.AddVideoValue(FullItem.SINGLEVIDEOFIELD_FIELD_NAME, videoId, librariesProviderName);
    }

    public void SetSingleVideoField(Video video)
    {
    this.ClearSingleVideoField();
    this.AddVideoValue(FullItem.SINGLEVIDEOFIELD_FIELD_NAME, video);
    }

    public void ClearSingleVideoField()
    {
        this.ClearVideos(FullItem.SINGLEVIDEOFIELD_FIELD_NAME);
    }
            private const string MULTIPLEVIDEOFIELD_FIELD_NAME = "MultipleVideoField";
            public ContentLink[] MultipleVideoField
            {
                get
                {
                                        return this.GetDynamicFieldValue<ContentLink[]>(FullItem.MULTIPLEVIDEOFIELD_FIELD_NAME);
                                }
                }

    public void AddMultipleVideoField(Guid videoId, string librariesProviderName = "")
    {
    this.AddVideoValue(FullItem.MULTIPLEVIDEOFIELD_FIELD_NAME, videoId, librariesProviderName);
    }

    public void AddMultipleVideoField(Video video)
    {
    this.AddVideoValue(FullItem.MULTIPLEVIDEOFIELD_FIELD_NAME, video);
    }

    public void ClearMultipleVideoField()
    {
        this.ClearVideos(FullItem.MULTIPLEVIDEOFIELD_FIELD_NAME);
    }
            private const string SINGLEFILEFIELD_FIELD_NAME = "SingleFileField";
            public ContentLink[] SingleFileField
            {
                get
                {
                                        return this.GetDynamicFieldValue<ContentLink[]>(FullItem.SINGLEFILEFIELD_FIELD_NAME);
                                }
                }

    public void SetSingleFileField(Guid fileId, string librariesProviderName = "")
    {
    this.ClearSingleFileField();
    this.AddFileValue(FullItem.SINGLEFILEFIELD_FIELD_NAME, fileId, librariesProviderName);
    }

    public void SetSingleFileField(Document file)
    {
    this.ClearSingleFileField();
    this.AddFileValue(FullItem.SINGLEFILEFIELD_FIELD_NAME, file);
    }

    public void ClearSingleFileField()
    {
        this.ClearFiles(FullItem.SINGLEFILEFIELD_FIELD_NAME);
    }
            private const string MULTIPLEFILEFIELD_FIELD_NAME = "MultipleFileField";
            public ContentLink[] MultipleFileField
            {
                get
                {
                                        return this.GetDynamicFieldValue<ContentLink[]>(FullItem.MULTIPLEFILEFIELD_FIELD_NAME);
                                }
                }

    public void AddMultipleFileField(Guid fileId, string librariesProviderName = "")
    {
    this.AddFileValue(FullItem.MULTIPLEFILEFIELD_FIELD_NAME, fileId, librariesProviderName);
    }

    public void AddMultipleFileField(Document file)
    {
    this.AddFileValue(FullItem.MULTIPLEFILEFIELD_FIELD_NAME, file);
    }

    public void ClearMultipleFileField()
    {
        this.ClearFiles(FullItem.MULTIPLEFILEFIELD_FIELD_NAME);
    }
            private const string GUIDFIELD_FIELD_NAME = "GuidField";
            public Guid GuidField
            {
                get
                {
                                        return this.GetDynamicFieldValue<Guid>(FullItem.GUIDFIELD_FIELD_NAME);
                                }
                set
                {
                    this.SetDynamicFieldValue(FullItem.GUIDFIELD_FIELD_NAME, value);
                }
                }

            private const string ARRAYOFGUIDS_FIELD_NAME = "ArrayOfGuids";
            public Guid[] ArrayOfGuids
            {
                get
                {
                                        return this.GetDynamicFieldValue<Guid[]>(FullItem.ARRAYOFGUIDS_FIELD_NAME);
                                }
                set
                {
                    this.SetDynamicFieldValue(FullItem.ARRAYOFGUIDS_FIELD_NAME, value);
                }
                }

            private const string RELATEDDYNAMICMODULEMULTIPLE_FIELD_NAME = "RelatedDynamicModuleMultiple";
            public IList<Relatedmodule.RelatedItem> RelatedDynamicModuleMultiple
            {
                get
                {
                                        return this.GetRelatedDynamicItems<Relatedmodule.RelatedItem>(FullItem.RELATEDDYNAMICMODULEMULTIPLE_FIELD_NAME);
                                }
                }

    public void AddRelatedDynamicModuleMultiple(Relatedmodule.RelatedItem relatedItem)
    {
        this.AddRelatedItem(FullItem.RELATEDDYNAMICMODULEMULTIPLE_FIELD_NAME, relatedItem);
    }
    public void ClearRelatedDynamicModuleMultiple()
    {
        this.ClearRelatedItems(FullItem.RELATEDDYNAMICMODULEMULTIPLE_FIELD_NAME);
    }
            private const string RELATEDDYNAMICMODULESINGLE_FIELD_NAME = "RelatedDynamicModuleSingle";
            public Relatedmodule.RelatedItem RelatedDynamicModuleSingle
            {
                get
                {
                                        return this.GetRelatedDynamicItem<Relatedmodule.RelatedItem>(FullItem.RELATEDDYNAMICMODULESINGLE_FIELD_NAME);
                                }
                }

    public void SetRelatedDynamicModuleSingle(Relatedmodule.RelatedItem relatedItem)
    {
        this.SetRelatedItem(FullItem.RELATEDDYNAMICMODULESINGLE_FIELD_NAME, relatedItem);
    }
    public void ClearRelatedDynamicModuleSingle()
    {
        this.ClearRelatedItems(FullItem.RELATEDDYNAMICMODULESINGLE_FIELD_NAME);
    }
            private const string NEWSRELATEDDATASINGLE_FIELD_NAME = "NewsRelatedDataSingle";
            public Telerik.Sitefinity.News.Model.NewsItem NewsRelatedDataSingle
            {
                get
                {
                                        return this.GetDynamicFieldValue<Telerik.Sitefinity.News.Model.NewsItem>(FullItem.NEWSRELATEDDATASINGLE_FIELD_NAME);
                                }
                }

    public void SetNewsRelatedDataSingle(Telerik.Sitefinity.News.Model.NewsItem relatedItem)
    {
        this.SetRelatedItem(FullItem.NEWSRELATEDDATASINGLE_FIELD_NAME, relatedItem);
    }
    public void ClearNewsRelatedDataSingle()
    {
        this.ClearRelatedItems(FullItem.NEWSRELATEDDATASINGLE_FIELD_NAME);
    }
            private const string NEWSRELATEDDATAMULTIPLE_FIELD_NAME = "NewsRelatedDataMultiple";
            public IList<Telerik.Sitefinity.News.Model.NewsItem> NewsRelatedDataMultiple
            {
                get
                {
                                        return this.GetDynamicFieldValue<IList<Telerik.Sitefinity.News.Model.NewsItem>>(FullItem.NEWSRELATEDDATAMULTIPLE_FIELD_NAME);
                                }
                }

    public void AddNewsRelatedDataMultiple(Telerik.Sitefinity.News.Model.NewsItem relatedItem)
    {
        this.AddRelatedItem(FullItem.NEWSRELATEDDATAMULTIPLE_FIELD_NAME, relatedItem);
    }
    public void ClearNewsRelatedDataMultiple()
    {
        this.ClearRelatedItems(FullItem.NEWSRELATEDDATAMULTIPLE_FIELD_NAME);
    }
            private const string FIRSTCLASSIFICATION_FIELD_NAME = "FirstClassification";
            public IEnumerable<Guid> FirstClassification
            {
                get
                {
                                        return this.GetDynamicFieldValue<IEnumerable<Guid>>(FullItem.FIRSTCLASSIFICATION_FIELD_NAME);
                                }
                }

    public void SetFirstClassification(params Guid[] taxonId)
    {
        //Do we need it?
        this.ClearClassificationField(FullItem.FIRSTCLASSIFICATION_FIELD_NAME);

        this.AddClassificationValue(FullItem.FIRSTCLASSIFICATION_FIELD_NAME, taxonId);
    }
    public void ClearFirstClassification()
    {
        this.ClearClassificationField(FullItem.FIRSTCLASSIFICATION_FIELD_NAME);
    }
            private const string SECONDCLASSIFICATION_FIELD_NAME = "SecondClassification";
            public IEnumerable<Guid> SecondClassification
            {
                get
                {
                                        return this.GetDynamicFieldValue<IEnumerable<Guid>>(FullItem.SECONDCLASSIFICATION_FIELD_NAME);
                                }
                }

    public void AddSecondClassification(params Guid[] taxonIds)
    {
        this.AddClassificationValue(FullItem.SECONDCLASSIFICATION_FIELD_NAME, taxonIds);
    }
    public void ClearSecondClassification()
    {
        this.ClearClassificationField(FullItem.SECONDCLASSIFICATION_FIELD_NAME);
    }
        }

    namespace Data
    {
        public partial class FullItemDataRepository : DynamicItemsRepositoryBase<FullItem>
        {
            public override string ItemTypeName
            {
                get
                {
                    return FullItem.DYNAMIC_TYPE_NAME;
                }
            }
        }
    }

    }
    namespace Relatedmodule
    {
        public class RelatedItem : DynamicTypeBase
        {
            public const string MAIN_SHORT_TEXT_FIELD_NAME = "Title";
            public const string DYNAMIC_TYPE_NAME = "Telerik.Sitefinity.DynamicTypes.Model.Relatedmodule.RelatedItem";
            
            public RelatedItem()
            {
            }

            private const string TITLE_FIELD_NAME = "Title";
            public Lstring Title
            {
                get
                {
                                        return this.GetDynamicFieldValue<Lstring>(RelatedItem.TITLE_FIELD_NAME);
                                }
                set
                {
                    this.SetDynamicFieldValue(RelatedItem.TITLE_FIELD_NAME, value.Value);
                }
                }

            private const string DATETIME_FIELD_NAME = "DateTime";
            public DateTime DateTime
            {
                get
                {
                                        return this.GetDynamicFieldValue<DateTime>(RelatedItem.DATETIME_FIELD_NAME);
                                }
                set
                {
                    this.SetDynamicFieldValue(RelatedItem.DATETIME_FIELD_NAME, value);
                }
                }

            private const string ICON_FIELD_NAME = "Icon";
            public ContentLink[] Icon
            {
                get
                {
                                        return this.GetDynamicFieldValue<ContentLink[]>(RelatedItem.ICON_FIELD_NAME);
                                }
                }

    public void SetIcon(Guid imageId, string librariesProviderName = "")
    {
    this.ClearIcon();
    this.AddImageValue(RelatedItem.ICON_FIELD_NAME, imageId, librariesProviderName);
    }

    public void SetIcon(Image image)
    {
    this.ClearIcon();
    this.AddImageValue(RelatedItem.ICON_FIELD_NAME, image);
    }

    public void ClearIcon()
    {
        this.ClearImages(RelatedItem.ICON_FIELD_NAME);
    }
        }

    namespace Data
    {
        public partial class RelatedItemDataRepository : DynamicItemsRepositoryBase<RelatedItem>
        {
            public override string ItemTypeName
            {
                get
                {
                    return RelatedItem.DYNAMIC_TYPE_NAME;
                }
            }
        }
    }

    }

}
