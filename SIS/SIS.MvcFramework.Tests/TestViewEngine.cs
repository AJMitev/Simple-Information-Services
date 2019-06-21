namespace SIS.MvcFramework.Tests
{
    using System.Collections.Generic;
    using System.IO;
    using Validation;
    using ViewEngine;
    using Xunit;

    public class TestViewEngine
    {
        [Theory]
        [InlineData("UserForForeachAndIf")]
        [InlineData("UseModelData")]
        [InlineData("TestWithoutCsharpCode")]
        public void TestGetHtml(string testFileName)
        {
            IViewEngine viewEngine = new SisViewEngine();
            var viewFileName = $"ViewTests/{testFileName}.html";
            var expectedResultFileName = $"ViewTests/{testFileName}.Result.html";

            var viewContent = File.ReadAllText(viewFileName);
            var expectedContent = File.ReadAllText(expectedResultFileName);

            var model = new TestViewModel
            {
                StringValue = "str",
                ListValues = new List<string>
                {
                    "123",
                    "val1",
                    string.Empty
                }
            };

            var modelState = new ModelStateDictionary();
            var principal = new Identity.Principal();

            var actualResult = viewEngine.GetHtml<object>(viewContent, model, modelState, principal);

            Assert.Equal(expectedContent.Trim(), actualResult.Trim());
        }
    }
}