Sitefnity Strong Types
======================

** Telerik.Sitefinity.DynamicModules.StrongTypes ** is NuGet package which will add ** SitefinityDynamicTypes.tt ** to     the selected project. This file is T4 template which will generate strong types for all dynamic types in the sitefinity     project in the opened solution.

If you are not familiar with the T4 templates you could take a look [here](http://msdn.microsoft.com/en-us/library/bb126445.aspx) or [here](http://www.hanselman.com/blog/T4TextTemplateTransformationToolkitCodeGenerationBestKeptVisualStudioSecret.aspx).

In short: When you install the Nuget package, SitefinityDynamicTypes.tt is added to the project, under it SitefinityDynamicTypes.cs file is generated and it contains one class for each dynamic type and one repository for retrieving, creating and editing items.

How the types are generated?
----------------------------
The T4 template file searches the whole solution for file with name DataConfig.config. When it is found it reads the connection string with name Sitefinity. After that all dynamic modules with thei definitions are read forom the DB.

Code generation conventions
---------------------------
1. For each dynamic module is created namespace with the following format {current_project_namespace}.{module_name}
2. This namespace contains one class for each of the content types in the dynamic module.
3. Again for each dynamic module is created namespace with format: {current_project_namespace}.{module_name}.Data
4. This namespace contains one repository class for each content type in the dynamic module. The name of the repository class is generated from the follwoing format: {content_type_name}DataRepository


Supported field types
---------------------
1. [ShortText](#shorttext),
2. [LongText](#longtext),
3. [MultipleChoice](#multiplechoice),
4. [YesNo](#yesno),
5. [Currency](#currency),
6. [DateTime](#datetime),
7. [Number](#number),
8. [Classification](#classification),
9. [Media](#media),
10. [Guid](#guid),
11. [GuidArray](#guidArray),
12. [Choices](#choices),
13. [Address](#address),
14. [RelatedMedia](#relatedmedia-and-reladetdata),
15. [RelatedData](#relatedmedia-and-reladetdata)

#### ShortText
For each short text field is generated public property with type Lstring with the name of the field with getter and setter.
> If the field is the main short text field, call to the SyncUrlNameWithMainShortTextFieldIfRequired method is added in order tho automatically set the UrlName when you set the title

```cs
private const string SHORTTEXT_FIELD_NAME = "ShortText";
public Lstring ShortText
{
    get
    {
        return this.GetDynamicFieldValue<Lstring>(FullItem.SHORTTEXT_FIELD_NAME);
    }
    set
    {
        this.SyncUrlNameWithMainShortTextFieldIfRequired(this.ShortText, value);
                                 
        this.SetDynamicFieldValue(FullItem.SHORTTEXT_FIELD_NAME, value.Value);
    }
}
```

#### LongText
The same as [ShortText](#shorttext)

#### MultipleChoice
Property only with getter is generated. A separate set method is generated

Single ChoiceField:
```cs
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
```
Multiple ChoiceField:
```cs
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
```

#### YesNo
Boolean property with the name of the field is generated
```cs
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
```

#### Currency
Decimal property with the filed name with getter and setter is generated

#### DateTime
DateTime property with the filed name with getter and setter is generated

#### Number
Same as [Currency](#currency)

#### Classification
Property that returns IEnumerable<Guid> is generated only with getter.
```cs
private const string FIRSTCLASSIFICATION_FIELD_NAME = "FirstClassification";
public IEnumerable<Guid> FirstClassification
{
    get
    {
        return this.GetDynamicFieldValue<IEnumerable<Guid>>(FullItem.FIRSTCLASSIFICATION_FIELD_NAME);
    }
}
```
Custom setter method and clear methods are generated is generated.
If single taxon is allowed:
```cs
public void SetFirstClassification(Guid taxonId)
{
    this.ClearClassificationField(FullItem.FIRSTCLASSIFICATION_FIELD_NAME);

    this.AddClassificationValue(FullItem.FIRSTCLASSIFICATION_FIELD_NAME, taxonId);
}
```
If multiple taxons are allowed:
```cs
public void AddSecondClassification(params Guid[] taxonIds)
{
    this.AddClassificationValue(FullItem.SECONDCLASSIFICATION_FIELD_NAME, taxonIds);
}
```
Clear method:
```cs
public void ClearFirstClassification()
{
    this.ClearClassificationField(FullItem.FIRSTCLASSIFICATION_FIELD_NAME);
}
```

#### Media
This type of field was obsolated in Sitefinity 7 but it is supported for backward compatibility.
Property that returns array of ContentLinks is generated. Custom Set/Add and Clear methods are generated.

#### Guid
Property with getter and setter is generated that returs the value of the field.
```cs
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
```

#### GuidArray
Property of type Guid[] is generated with getter and setter methods.
```cs
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
```

#### Choices
Property with getter method is generated that returns ChoiceOption or ChoiceOptions[] depending on multiple items are allowed. Respective setter methods are generated that accept string or string[] which are the ids of the ChoiceOptions
```cs
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
```

#### Address
Property of type Address is generated with getter and setter methods.
```cs
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
```

#### RelatedMedia and ReladetData
Property with getter is generated which returns the type of the related item. If the related item is Dynamic content, the newly generated type is returned, otherwise sitefinity built in type is returned (Image, Document, NewsItems, etc.).
Custom Set/Add (depending on that if multiple items are allowed) and clear methods are generated.

Built in sitefinity type
```cs
private const string SINGLEIMAGEFIELD_FIELD_NAME = "SingleImageField";
public Telerik.Sitefinity.Libraries.Model.Image SingleImageField
{
    get
    {
        return this.GetRelatedKnowTypeItem<Telerik.Sitefinity.Libraries.Model.Image>(FullItem.SINGLEIMAGEFIELD_FIELD_NAME);
    }
}

public void SetSingleImageField(Telerik.Sitefinity.Libraries.Model.Image relatedItem)
{
    this.SetRelatedItem(FullItem.SINGLEIMAGEFIELD_FIELD_NAME, relatedItem);
}

public void ClearSingleImageField()
{
    this.ClearRelatedItems(FullItem.SINGLEIMAGEFIELD_FIELD_NAME);
}

private const string MULTIPLEIMAGEFIELD_FIELD_NAME = "MultipleImageField";
public IList<Telerik.Sitefinity.Libraries.Model.Image> MultipleImageField
{
    get
    {
        return this.GetRelatedKnowTypeItems<Telerik.Sitefinity.Libraries.Model.Image>(FullItem.MULTIPLEIMAGEFIELD_FIELD_NAME);
    }
}

public void AddMultipleImageField(Telerik.Sitefinity.Libraries.Model.Image relatedItem)
{
    this.AddRelatedItem(FullItem.MULTIPLEIMAGEFIELD_FIELD_NAME, relatedItem);
}

public void ClearMultipleImageField()
{
    this.ClearRelatedItems(FullItem.MULTIPLEIMAGEFIELD_FIELD_NAME);
}
```
Newly generated dynamic type
```cs
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
```
Constants
---------
Each class for dynamic type has two public constants:
1. MAIN_SHORT_TEXT_FIELD_NAME
2. DYNAMIC_TYPE_NAME

Example:
```cs
public const string MAIN_SHORT_TEXT_FIELD_NAME = "Title";
public const string DYNAMIC_TYPE_NAME = "Telerik.Sitefinity.DynamicTypes.Model.MyModule.MyItem";
```
DataProviders
-------------
#### Public members
```cs
public abstract string ItemTypeName { get; }
```
The sitefinity dynamic type name which is overrided in the provider's class.

```cs
public void Commit()
```
Commits all changes to the storage (In the most cases database)

```cs
public T Create();
public T Create(Guid itemId);
```
Factory method that creates new item of the concrete strong type.

```cs
public void Delete(Guid itemId);
public void Delete(T item);
```
Marks item for deletion.

```cs
public IList<T> GetAll()
```
Returns all items (Live, Master, and Temp)

```cs
public T GetEditableItem(T originalItem)
```
If the item is Live items returns the Master item as changes only on Master items are allowed. If you pass Master item, the same item is returned.

```cs
public T GetItemById(Guid id)
```
Gets item by its Id

```cs
public T GetItemByUrlName(string urlName)
```
Gets item by its UrlName

```cs
public T GetItemFromUrl(string url, bool published)
```
Resolves item from the passed url parameters. The published flag shows if we want Live or Master item.

```cs
public IList<T> GetItemsForEdit()
```
Returns list of Master items - ready for edit.

```cs
public IList<T> GetPublishedItems()
```
Returns list of live items which should be shown on the frontend.

```cs
public void MarkAsDraft(T item)
```
Sets the selected item ApprovalWorkflowState to Draft - this means that only the Master version of the item is updated and the Live version stays untouched.

```cs
public void Publish(T item)
```
Sets the selected item ApprovalWorkflowState to Published - when the chages are commited both the live item and the master item will be updated.

```cs
public void SchedulePublish(T item, DateTime publishDate)
```
Sets the selected item ApprovalWorkflowState to Scheduled - The item will be published on the selected publish date.

```cs
public void Unpublish(T item)
```
Sets the selected item ApprovalWorkflowState to Unpublished - Marks the Live version of the item as Invisible (the Visible field of the item is set to false)

#### Protected members
```cs
protected Type ItemType { get; }
```
The resolved dynamic type of the item

```cs
protected DynamicModuleManager Manager { get; }
```
Instance of the Dynamic module manager used to work with the sitefinity api.

```cs
protected virtual string ProviderName { get; }
```
The provider name used of the DynamicModuleManager

```cs
protected IQueryable<DynamicContent> GetAllUntyped();
```
Returns the sitefinity data objects (They should be used only internally and must not be exposed by the public API)

```cs
protected IQueryable<DynamicContent> GetUntypedItemsForEdit()
```
Returns not materialized IQueryable collection of sitefinity data Master items. This method should be used by the specific public API methods.

```cs
protected IQueryable<DynamicContent> GetUntypedPublishedItems();
```
The same as GetUntypedItemsForEdit but returns the Live version of the items.

```cs
protected virtual IDynamicTypeFactory<T> ResolveItemsFactory();
```
This method is called internally. It should return the factory class used for converting DynamicContent objects to Strong typed objects and creating new objects.

```cs
protected T Typify(DynamicContent dynamicContent);
protected IList<T> Typify(IQueryable<DynamicContent> dynamicItems);
```
These methods converts single or multiple items from DynamicContent to strong typed item. All public methods that returns items must call one of these methods in order to expose only typified objects.

Code snippets
-------------

#### Create new draft item
```cs
MyItem newItem = dataRepository.Create();
//Set properties here ...
dataRepository.Commit();
```

#### Create new item and publish it
```cs
NewItem newItem = dataRepository.Create();
//Set properties here ...
dataRepository.Commit();
dataRepository.Publish(newItem);
dataRepository.Commit();
```

#### Publish existing item
```cs
MyItem item = dataRepository.GetItemById(itemId);
dataRepository.Publish(item);
dataRepository.Commit();
```

#### Update only master version of the item
```cs
MyItem item = dataRepository.GetItemById(itemId);
//Update some properties here
dataRepository.MarkAsDraft(item);
dataRepository.Commit();
```

#### Unpublish published item
```cs
MyItem masterItem = dataRepository.GetItemById(itemId);
dataRepository.Unpublish(masterItem);
dataRepository.Commit();
```

#### Convert Master item to Live item
```cs
MyItem liveItem = dataRepository.GetItemById(itemId);
Myitem masterItem = dataRepository.GetEditableItem(liveItem);
```