DNTBreadCrumb.Core-UIkit
=======
`DNTBreadCrumb.Core` Creates custom bread crumb definitions, based on UIkit 3 features for ASP.NET Core applications.

**This is a fork from [DNTBreadCrumb.Core](https://github.com/VahidN/DNTBreadCrumb.Core) with adds breadcrumbs for UIkit 3 instead of for Twitter Bootstrap.**


Install via NuGet
-----------------
To install DNTBreadCrumb.Core-UIkit, run the following command in the Package Manager Console:

```
PM> Install-Package DNTBreadCrumb.Core-UIkit
```

You can also view the [package page](http://www.nuget.org/packages/DNTBreadCrumb.Core-UIKit/) on NuGet.



Usage:
-----------------
- After installing the DNTBreadCrumb.Core-UIkit package, add the following definition to the _ViewImports.cshtml file:
```csharp
@addTagHelper *, DNTBreadCrumb.Core-UIkit
```

- Then modify the _Layout.cshtml file to add its new tag-helper:
```xml
 <breadcrumb asp-homepage-title="Home"
             asp-homepage-url="@Url.Action("Index", "Home", values: new { area = "" })"
             asp-homepage-icon="heart"></breadcrumb>
```


- Now you can add the `BreadCrumb` attributes to your controller or action methods:
```csharp
[BreadCrumb(Title = "Home", UseDefaultRouteUrl = true, Order = 0)]
public class HomeController : Controller
{
   [BreadCrumb(Title = "Main index", Order = 1)]
   public ActionResult Index()
   {
      return View();
   }
```
