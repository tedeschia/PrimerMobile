using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Batch;
using System.Web.Http.OData.Batch;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;
using System.Web.Http.OData.Routing;
using SeguimientoEleccion.Web.Models;

namespace SeguimientoEleccion.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            //config.Routes.MapHttpBatchRoute(
            //    routeName: "WebApiBatch",
            //    routeTemplate: "api/$batch",
            //    batchHandler:new DefaultHttpBatchHandler(GlobalConfiguration.DefaultServer)
            //);
            //ConfigureOData(config);
        }

        static void ConfigureOData(HttpConfiguration config)
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Elector>("Electores");
            config.Routes.MapODataServiceRoute(
                routeName: "ODataRoute",
                routePrefix: "",
                model:builder.GetEdmModel(),
                batchHandler:new DefaultODataBatchHandler(GlobalConfiguration.DefaultServer));
        }
    }
    public class PathFixODataBatchHandler : DefaultODataBatchHandler
    {

        public PathFixODataBatchHandler(HttpServer httpServer)
            : base(httpServer)
        {
        }

        public override async Task<IList<ODataBatchResponseItem>> ExecuteRequestMessagesAsync(IEnumerable<ODataBatchRequestItem> requests, CancellationToken cancellationToken)
        {
            if (requests == null)
            {
                throw new System.ArgumentNullException("requests"); // Error.ArgumentNull("requests");
            }

            IList<ODataBatchResponseItem> responses = new List<ODataBatchResponseItem>();

            try
            {
                foreach (ODataBatchRequestItem request in requests)
                {
                    fixRequestUri(request);
                    responses.Add(await request.SendRequestAsync(Invoker, cancellationToken));
                }
            }
            catch
            {
                foreach (ODataBatchResponseItem response in responses)
                {
                    if (response != null)
                    {
                        response.Dispose();
                    }
                }
                throw;
            }

            return responses;
        }

        private void fixRequestUri(ODataBatchRequestItem request)
        {
            foreach (HttpRequestMessage req in ((ChangeSetRequestItem)request).Requests)
            {
                var oldUri = req.RequestUri;
                var newUriBuilder = new UriBuilder(oldUri);
                newUriBuilder.Path = "/odata" + newUriBuilder.Path;
                req.RequestUri = newUriBuilder.Uri;
            }
        }
    }
}
