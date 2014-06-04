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
3. [MultipleChoice](#shorttext),
4. [YesNo](#shorttext),
5. [Currency](#shorttext),
6. [DateTime](#shorttext),
7. [Number](#shorttext),
8. [Classification](#shorttext),
9. [Media](#shorttext),
10. [Guid](#shorttext),
11. [GuidArray](#shorttext),
12. [Choices](#shorttext),
13. [Address](#shorttext),
14. [RelatedMedia](#shorttext),
15. [RelatedData](#shorttext)

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