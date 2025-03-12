//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.AspNetCore.Mvc.ViewEngines;
//using Microsoft.AspNetCore.Mvc.ViewFeatures;
//using Microsoft.AspNetCore.Routing;
//using System.IO;
//using System.Threading.Tasks;

//namespace E_Commerce.Application.Common.Services
//{
//    public class EmailTemplateRenderer
//    {
//        private readonly ICompositeViewEngine _viewEngine;
//        private readonly ITempDataProvider _tempDataProvider;
//        private readonly IServiceProvider _serviceProvider;

//        public EmailTemplateRenderer(
//            ICompositeViewEngine viewEngine,
//            ITempDataProvider tempDataProvider,
//            IServiceProvider serviceProvider)
//        {
//            _viewEngine = viewEngine;
//            _tempDataProvider = tempDataProvider;
//            _serviceProvider = serviceProvider;
//        }

//        public async Task<string> RenderTemplateAsync<TModel>(string templatePath, TModel model)
//        {
//            var httpContext = new DefaultHttpContext
//            {
//                RequestServices = _serviceProvider
//            };

//            var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());

//            var viewResult = _viewEngine.FindView(actionContext, templatePath, false);

//            if (!viewResult.Success)
//            {
//                throw new FileNotFoundException($"Email template '{templatePath}' not found");
//            }

//            using var output = new StringWriter();
//            var viewContext = new ViewContext(
//                actionContext,
//                viewResult.View,
//                new ViewDataDictionary<TModel>(new EmptyModelMetadataProvider(), new ModelStateDictionary())
//                {
//                    Model = model
//                },
//                new TempDataDictionary(actionContext.HttpContext, _tempDataProvider),
//                output,
//                new HtmlHelperOptions()
//            );

//            await viewResult.View.RenderAsync(viewContext);
//            return output.ToString();
//        }
//    }
//}