$packageProject = Get-Project "Telerik.Sitefinity.DynamicModules.StrongTypes"
$packageProjectOutputPath = $packageProject.Properties.Item("FullPath").Value
$packageProjectRelativeOutputPath = $packageProject.ConfigurationManager.ActiveConfiguration.Properties.Item("OutputPath").Value
$fullOutputPath = [System.IO.Path]::Combine($packageProjectOutputPath, $packageProjectRelativeOutputPath)

$fullOutputPath

Update-Package -reinstall Telerik.Sitefinity.DynamicModules.StrongTypes -Source $fullOutputPath