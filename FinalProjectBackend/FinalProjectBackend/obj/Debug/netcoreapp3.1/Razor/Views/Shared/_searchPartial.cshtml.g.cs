#pragma checksum "C:\Users\ACER\Desktop\Full-Stack-Final-Project\FinalProjectBackend\FinalProjectBackend\Views\Shared\_searchPartial.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5d8f750ebf0e834edf1adb8b4f1103d836411122"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__searchPartial), @"mvc.1.0.view", @"/Views/Shared/_searchPartial.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\ACER\Desktop\Full-Stack-Final-Project\FinalProjectBackend\FinalProjectBackend\Views\_ViewImports.cshtml"
using FinalProjectBackend;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\ACER\Desktop\Full-Stack-Final-Project\FinalProjectBackend\FinalProjectBackend\Views\_ViewImports.cshtml"
using FinalProjectBackend.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\ACER\Desktop\Full-Stack-Final-Project\FinalProjectBackend\FinalProjectBackend\Views\_ViewImports.cshtml"
using FinalProjectBackend.ViewModel;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5d8f750ebf0e834edf1adb8b4f1103d836411122", @"/Views/Shared/_searchPartial.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7d8ec620332005fef2bbda78a6ee68b29628a327", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__searchPartial : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Event>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n");
#nullable restore
#line 3 "C:\Users\ACER\Desktop\Full-Stack-Final-Project\FinalProjectBackend\FinalProjectBackend\Views\Shared\_searchPartial.cshtml"
 foreach (Event pro in Model)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("<li><a");
            BeginWriteAttribute("href", " href=\"", 65, "\"", 99, 2);
            WriteAttributeValue("", 72, "ProductDetail/Index/", 72, 20, true);
#nullable restore
#line 5 "C:\Users\ACER\Desktop\Full-Stack-Final-Project\FinalProjectBackend\FinalProjectBackend\Views\Shared\_searchPartial.cshtml"
WriteAttributeValue("", 92, pro.Id, 92, 7, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 5 "C:\Users\ACER\Desktop\Full-Stack-Final-Project\FinalProjectBackend\FinalProjectBackend\Views\Shared\_searchPartial.cshtml"
                                     Write(pro.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a></li>");
#nullable restore
#line 5 "C:\Users\ACER\Desktop\Full-Stack-Final-Project\FinalProjectBackend\FinalProjectBackend\Views\Shared\_searchPartial.cshtml"
                                                             }

#line default
#line hidden
#nullable disable
            WriteLiteral("\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Event>> Html { get; private set; }
    }
}
#pragma warning restore 1591
