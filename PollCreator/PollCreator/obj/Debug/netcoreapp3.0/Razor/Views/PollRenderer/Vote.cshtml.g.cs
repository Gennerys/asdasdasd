#pragma checksum "D:\GIT\TraineePlayground\Papirnyk_Oleksandr\PollCreator\PollCreator\Views\PollRenderer\Vote.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0674ecdb89e39121ee169875945cec2b38652c9c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_PollRenderer_Vote), @"mvc.1.0.view", @"/Views/PollRenderer/Vote.cshtml")]
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
#line 1 "D:\GIT\TraineePlayground\Papirnyk_Oleksandr\PollCreator\PollCreator\Views\_ViewImports.cshtml"
using PollCreator;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0674ecdb89e39121ee169875945cec2b38652c9c", @"/Views/PollRenderer/Vote.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0bb343d8c37829e60a36604f72c5c0406b1edeef", @"/Views/_ViewImports.cshtml")]
    public class Views_PollRenderer_Vote : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<PollCreator.ViewModels.PollRenderViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<h1>Vote Page</h1>\r\n<div>\r\n\t");
#nullable restore
#line 4 "D:\GIT\TraineePlayground\Papirnyk_Oleksandr\PollCreator\PollCreator\Views\PollRenderer\Vote.cshtml"
Write(Html.DisplayName(Model.Title));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n</div>\r\n");
#nullable restore
#line 6 "D:\GIT\TraineePlayground\Papirnyk_Oleksandr\PollCreator\PollCreator\Views\PollRenderer\Vote.cshtml"
  
	using (Html.BeginForm("Vote", "PollRenderer", FormMethod.Post))
	{
		if (Model.IsSingleOption)
		{
			foreach (var option in Model.Options)
			{

#line default
#line hidden
#nullable disable
            WriteLiteral("\t\t\t\t<span>");
#nullable restore
#line 13 "D:\GIT\TraineePlayground\Papirnyk_Oleksandr\PollCreator\PollCreator\Views\PollRenderer\Vote.cshtml"
                 Write(Html.DisplayFor(i => option.SerialNumber));

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 13 "D:\GIT\TraineePlayground\Papirnyk_Oleksandr\PollCreator\PollCreator\Views\PollRenderer\Vote.cshtml"
                                                            Write(option.Value);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span> ");
#nullable restore
#line 13 "D:\GIT\TraineePlayground\Papirnyk_Oleksandr\PollCreator\PollCreator\Views\PollRenderer\Vote.cshtml"
                                                                                 Write(Html.RadioButton("radioButtonOpt", option.Value));

#line default
#line hidden
#nullable disable
            WriteLiteral("\t\t\t\t<br />\r\n");
#nullable restore
#line 15 "D:\GIT\TraineePlayground\Papirnyk_Oleksandr\PollCreator\PollCreator\Views\PollRenderer\Vote.cshtml"
			}
		}
		else
		{
			foreach (var option in Model.Options)
			{

#line default
#line hidden
#nullable disable
            WriteLiteral("\t\t\t\t<span>");
#nullable restore
#line 21 "D:\GIT\TraineePlayground\Papirnyk_Oleksandr\PollCreator\PollCreator\Views\PollRenderer\Vote.cshtml"
                 Write(Html.DisplayFor(i => option.SerialNumber));

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 21 "D:\GIT\TraineePlayground\Papirnyk_Oleksandr\PollCreator\PollCreator\Views\PollRenderer\Vote.cshtml"
                                                            Write(option.Value);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>");
#nullable restore
#line 21 "D:\GIT\TraineePlayground\Papirnyk_Oleksandr\PollCreator\PollCreator\Views\PollRenderer\Vote.cshtml"
                                                                                Write(Html.CheckBox($"{option.Value}"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\t\t\t\t<br />\r\n");
#nullable restore
#line 23 "D:\GIT\TraineePlayground\Papirnyk_Oleksandr\PollCreator\PollCreator\Views\PollRenderer\Vote.cshtml"
			}
			
		}

#line default
#line hidden
#nullable disable
            WriteLiteral("\t\t<input type=\"submit\" value=\"submit\" />\r\n");
#nullable restore
#line 27 "D:\GIT\TraineePlayground\Papirnyk_Oleksandr\PollCreator\PollCreator\Views\PollRenderer\Vote.cshtml"

	}


#line default
#line hidden
#nullable disable
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<PollCreator.ViewModels.PollRenderViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
