#pragma checksum "C:\Users\User\Desktop\EDP\EDP_Project\Pages\Staff\Survey\Create.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "213738ec1f64d12deea3fd1671a8ab4eca390992"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(EDP_Project.Pages.Staff.Survey.Pages_Staff_Survey_Create), @"mvc.1.0.razor-page", @"/Pages/Staff/Survey/Create.cshtml")]
namespace EDP_Project.Pages.Staff.Survey
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
#line 1 "C:\Users\User\Desktop\EDP\EDP_Project\Pages\_ViewImports.cshtml"
using EDP_Project;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"213738ec1f64d12deea3fd1671a8ab4eca390992", @"/Pages/Staff/Survey/Create.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"998634860f44dea60736173b5c33d0d16ee27270", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Staff_Survey_Create : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("text-danger"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-label"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-control"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", "text", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("mainContainerForm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationSummaryTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationMessageTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.TextAreaTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_TextAreaTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "C:\Users\User\Desktop\EDP\EDP_Project\Pages\Staff\Survey\Create.cshtml"
  
    ViewData["Title"] = "Create Survey - Admin";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<style>
    .btnCustom {
        display: inline-block;
        height: 50px;
        width: 50px;
        border-radius: 50%;
        border: 1px solid black;
        padding-top: 0px;
        font-size: 30px;
        font-weight: bold;
    }

    .qnsTB {
        background-color: #f1f3f4;
        border: none;
        border-bottom: 1px solid black;
        border-radius: 10px 10px 0 0;
    }

    .dot {
        height: 25px;
        width: 25px;
        background-color: white;
        border: 2px solid grey;
        border-radius: 50%;
        display: inline-block;
    }
</style>

<div class=""container"">
    <h1 class=""text-center text-dark mt-5"">Create Survey </h1>
    <div class=""row"">
        <div class=""col-md-12"">
            <div class=""p-3"">
");
            WriteLiteral("                    <input type=\"submit\" value=\"+\" class=\"btnCustom\" id=\"addQnsButton\" />\r\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "213738ec1f64d12deea3fd1671a8ab4eca3909927105", async() => {
                WriteLiteral(@"
                        <div class=""col-12 my-3 text-center"">
                            <input type=""submit"" value=""Create"" class=""btn btn-dark ml-auto"" />

                        </div>
                        <div class=""row mt-4"">
                            <div class=""col-md-12"">
                                <div class=""card p-3"">
                                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("div", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "213738ec1f64d12deea3fd1671a8ab4eca3909927763", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationSummaryTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper);
#nullable restore
#line 51 "C:\Users\User\Desktop\EDP\EDP_Project\Pages\Staff\Survey\Create.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper.ValidationSummary = global::Microsoft.AspNetCore.Mvc.Rendering.ValidationSummary.All;

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-validation-summary", __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationSummaryTagHelper.ValidationSummary, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n\r\n                                    <div class=\"col-12 my-3\">\r\n                                        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "213738ec1f64d12deea3fd1671a8ab4eca3909929526", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
                BeginWriteTagHelperAttribute();
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __tagHelperExecutionContext.AddHtmlAttribute("hidden", Html.Raw(__tagHelperStringValueBuffer), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.Minimized);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
#nullable restore
#line 54 "C:\Users\User\Desktop\EDP\EDP_Project\Pages\Staff\Survey\Create.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => Model.newSurvey.SurveyUUID);

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                BeginWriteTagHelperAttribute();
#nullable restore
#line 54 "C:\Users\User\Desktop\EDP\EDP_Project\Pages\Staff\Survey\Create.cshtml"
                                                                                                          WriteLiteral(Model.surveyUUID);

#line default
#line hidden
#nullable disable
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.Value = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.Value, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                                    </div>\r\n\r\n                                    <div class=\"col-12 my-3\">\r\n                                        <label class=\"form-label\">Category</label>\r\n                                        ");
#nullable restore
#line 59 "C:\Users\User\Desktop\EDP\EDP_Project\Pages\Staff\Survey\Create.cshtml"
                                   Write(Html.DropDownListFor(m => m.newSurvey.Category, new SelectList(Enum.GetValues(typeof(Models.Survey.SurveyCategory))), new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                                        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("span", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "213738ec1f64d12deea3fd1671a8ab4eca39099212840", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationMessageTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper);
#nullable restore
#line 60 "C:\Users\User\Desktop\EDP\EDP_Project\Pages\Staff\Survey\Create.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => Model.newSurvey.Category);

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-validation-for", __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                                    </div>\r\n\r\n                                    <div class=\"col-12 my-3\">\r\n                                        <label class=\"form-label\">Title</label>\r\n                                        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "213738ec1f64d12deea3fd1671a8ab4eca39099214732", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_3.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
#nullable restore
#line 65 "C:\Users\User\Desktop\EDP\EDP_Project\Pages\Staff\Survey\Create.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.newSurvey.Title);

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                                        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("span", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "213738ec1f64d12deea3fd1671a8ab4eca39099216561", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationMessageTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper);
#nullable restore
#line 66 "C:\Users\User\Desktop\EDP\EDP_Project\Pages\Staff\Survey\Create.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => Model.newSurvey.Title);

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-validation-for", __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                                    </div>\r\n                                    <div class=\"col-12 my-3\">\r\n                                        <label class=\"form-label\">Description</label>\r\n                                        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("textarea", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "213738ec1f64d12deea3fd1671a8ab4eca39099218452", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_TextAreaTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.TextAreaTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_TextAreaTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
#nullable restore
#line 70 "C:\Users\User\Desktop\EDP\EDP_Project\Pages\Staff\Survey\Create.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_TextAreaTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.newSurvey.Description);

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_TextAreaTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                                        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("span", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "213738ec1f64d12deea3fd1671a8ab4eca39099220095", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationMessageTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper);
#nullable restore
#line 71 "C:\Users\User\Desktop\EDP\EDP_Project\Pages\Staff\Survey\Create.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => Model.newSurvey.Description);

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-validation-for", __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                                    </div>\r\n\r\n                                </div>\r\n\r\n                            </div>\r\n                        </div>\r\n                    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_5.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
            WriteLiteral(@"
            </div>
        </div>
    </div>

</div>

<script>

    function uuidv4() {
        return ([1e7] + -1e3 + -4e3 + -8e3 + -1e11).replace(/[018]/g, c =>
            (c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> c / 4).toString(16)
        );
    }

    var addQuestionButton = document.getElementById(""addQnsButton"");
    addQuestionButton.addEventListener(""click"", addNewQuestionForm);

    function addNewQuestionForm() {

        const qUUID = uuidv4();

        var rowDiv = document.createElement(""div"");
        rowDiv.classList.add(""row"");
        rowDiv.classList.add(""mt-4"");

        var colMD12Div = document.createElement(""div"");
        colMD12Div.className = ""col-md-12"";

        var cardP_3 = document.createElement(""div"");
        cardP_3.classList.add(""card"");
        cardP_3.classList.add(""p-3"");

        var questionUUIDHidden = document.createElement(""input"");
        questionUUIDHidden.classList.add(""hiddenQuestionUUIDInput"");
        quest");
            WriteLiteral(@"ionUUIDHidden.hidden = true;
        questionUUIDHidden.setAttribute(""value"", `${qUUID}`);

        var questionTB = document.createElement(""input"");
        questionTB.classList.add(""qnsInput"");
        questionTB.classList.add(""form-control"");
        questionTB.classList.add(""mt-3"");
        questionTB.classList.add(""qnsTB"");
        questionTB.placeholder = ""Question Text""

        var questionBelongsToUUIDHidden = document.createElement(""input"");
        questionBelongsToUUIDHidden.classList.add(""hiddenBelongsToSUUIDInput"");
        questionBelongsToUUIDHidden.hidden = true;
        questionBelongsToUUIDHidden.setAttribute(""value"", '");
#nullable restore
#line 129 "C:\Users\User\Desktop\EDP\EDP_Project\Pages\Staff\Survey\Create.cshtml"
                                                      Write(Model.surveyUUID);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"');

        cardP_3.appendChild(questionUUIDHidden);
        cardP_3.appendChild(questionTB);
        cardP_3.appendChild(questionBelongsToUUIDHidden);


        var addInputButton = document.createElement(""a"");
        addInputButton.className = ""btnCustom"";
        addInputButton.id = ""addOptionButton"";
        addInputButton.textContent = ""A"";
        addInputButton.addEventListener(""click"", function () {
            addNewQuestionInput(qUUID);
        });

        cardP_3.appendChild(addInputButton);

        var deleteQuestionButton = document.createElement(""a"");
        deleteQuestionButton.className = ""btnCustom"";
        deleteQuestionButton.id = ""deleteQnsButton"";
        deleteQuestionButton.textContent = ""DQ"";
        deleteQuestionButton.addEventListener(""click"", function () {
            deleteQuestionForm(qUUID);
        });

        cardP_3.appendChild(deleteQuestionButton);

        colMD12Div.appendChild(cardP_3);
        rowDiv.appendChild(colMD12Div);

       ");
            WriteLiteral(@" document.getElementById(""mainContainerForm"").appendChild(rowDiv);
    }

    function addNewQuestionInput(questionUUID) {
        var formInlineDiv = document.createElement(""div"");

        const optionUUID = uuidv4();

        formInlineDiv.className = ""form-inline"";

        var circleIconLabel = document.createElement(""input"");
        circleIconLabel.classList.add(""dot"");
        circleIconLabel.classList.add(""my-1"");
        circleIconLabel.classList.add(""mr-2"");
        circleIconLabel.classList.add(""mt-3"");

        var hiddenOptionUUIDInput = document.createElement(""input"");
        hiddenOptionUUIDInput.className = ""hiddenOptionUUIDInput"";
        hiddenOptionUUIDInput.hidden = true;
        hiddenOptionUUIDInput.setAttribute(""value"", `${optionUUID}`);

        var optionTextInput = document.createElement(""input"");
        optionTextInput.classList.add(""optionInput"");
        optionTextInput.classList.add(""form-control"");
        optionTextInput.classList.add(""mt-3"");
      ");
            WriteLiteral(@"  optionTextInput.type = ""text"";

        var hiddenBelongsToQUUIDInput = document.createElement(""input"");
        hiddenBelongsToQUUIDInput.className = ""hiddenBelongsToQUUIDInput"";
        hiddenBelongsToQUUIDInput.hidden = true;
        hiddenBelongsToQUUIDInput.setAttribute(""value"", questionUUID);

        formInlineDiv.appendChild(circleIconLabel);
        formInlineDiv.appendChild(hiddenOptionUUIDInput);
        formInlineDiv.appendChild(optionTextInput);
        formInlineDiv.appendChild(hiddenBelongsToQUUIDInput);

        var deleteInputButton = document.createElement(""a"");
        deleteInputButton.className = ""btnCustom"";
        deleteInputButton.id = ""deleteOptionButton"";
        deleteInputButton.textContent = ""D"";
        deleteInputButton.addEventListener(""click"", function () {
            deleteOptionInput(optionUUID);
        });

        formInlineDiv.appendChild(deleteInputButton);


        var allInputs = document.getElementsByTagName(""input"");

        for (var i");
            WriteLiteral(@" = 0; i < allInputs.length; i++) {
            if (allInputs[i].value == questionUUID) {
                if (allInputs[i].hidden == true) {
                    allInputs[i].parentElement.appendChild(formInlineDiv);
                    break;
                }
            }
        }
    }

    function deleteOptionInput(optionUUID) {
        var allInputs = document.getElementsByTagName(""input"");

        for (var i = 0; i < allInputs.length; i++) {
            if (allInputs[i].hidden == true) {
                if (allInputs[i].value == optionUUID) {
                    var parentElementOfInput = allInputs[i].parentElement;

                    parentElementOfInput.remove();
                }
            }
        }
    }

    function deleteQuestionForm(questionUUID) {
        var allInputs = document.getElementsByTagName(""input"");

        for (var i = 0; i < allInputs.length; i++) {
            if (allInputs[i].hidden == true) {
                if (allInputs[i].value == questio");
            WriteLiteral(@"nUUID) {
                    var parentCard = allInputs[i].parentElement;
                    var parentColMD = parentCard.parentElement;
                    var parentRow = parentColMD.parentElement;

                    parentRow.remove();
                }
            }
        }
    }

    function resortIndex(e) {
        //e.preventDefault();
        var questionInputs = document.getElementsByClassName(""qnsInput"");
        var hiddenQuestionUUIDInputs = document.getElementsByClassName(""hiddenQuestionUUIDInput"");
        var hiddenBelongsToSUUIDInputs = document.getElementsByClassName(""hiddenBelongsToSUUIDInput"");


        var hiddenOptionUUIDInputs = document.getElementsByClassName(""hiddenOptionUUIDInput"");
        var optionInputs = document.getElementsByClassName(""optionInput"");
        var hiddenBelongsToQUUIDInputs = document.getElementsByClassName(""hiddenBelongsToQUUIDInput"");

        for (var i = 0; i < questionInputs.length; i++) {

            questionInputs[i].name = `");
            WriteLiteral(@"AllQuestionList[${i}].Text`;
            hiddenQuestionUUIDInputs[i].name = `AllQuestionList[${i}].QuestionUUID`;
            hiddenBelongsToSUUIDInputs[i].name = `AllQuestionList[${i}].BelongsToSurveyID`;

            for (var k = 0; k < optionInputs.length; k++) {

                hiddenOptionUUIDInputs[k].name = `qnsOptions[${k}].OptionUUID`;
                optionInputs[k].name = `qnsOptions[${k}].Text`;
                hiddenBelongsToQUUIDInputs[k].name = `qnsOptions[${k}].BelongsToQuestionID`;
            }

        }
        //$.ajax({
        //    url: """",
        //    type: ""POST"",
        //    data: $(""#mainContainerForm"").serialize(),
        //    success: function () {
        //        alert('success');
        //    }
        //});
    }

        document.getElementById(""mainContainerForm"").addEventListener(""submit"", resortIndex);
</script>
");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<EDP_Project.Pages.Staff.Survey.CreateSurveyModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<EDP_Project.Pages.Staff.Survey.CreateSurveyModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<EDP_Project.Pages.Staff.Survey.CreateSurveyModel>)PageContext?.ViewData;
        public EDP_Project.Pages.Staff.Survey.CreateSurveyModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591
