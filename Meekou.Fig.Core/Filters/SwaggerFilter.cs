using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace Meekou.Fig.Core.Filters
{
    public class SwaggerFilter : IDocumentFilter, IOperationFilter
    {
        private readonly List<XPathDocument> _xmlDocs = new List<XPathDocument>();

        public SwaggerFilter()
        {
            var xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml");
            foreach (var xmlFile in xmlFiles)
            {
                _xmlDocs.Add(new XPathDocument(xmlFile));
            }
        }
        #region DocumentFilter
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            AddMSConnectorMetadata(swaggerDoc, context);
            //foreach (var path in swaggerDoc.Paths)
            //{
            //    foreach (var operation in path.Value.Operations)
            //    {
            //        foreach (var param in operation.Value.Parameters)
            //        {
            //            param.Extensions.Add("x-ms-summary", new Microsoft.OpenApi.Any.OpenApiString(param.Description));
            //        }
            //        //var actionDescriptor = context.ApiDescriptions.FirstOrDefault(action => path.Key.Contains(action.RelativePath))?.ActionDescriptor;
            //        //if (actionDescriptor != null)
            //        //{
            //        //    var methodInfo = (actionDescriptor as ControllerActionDescriptor)?.MethodInfo;
            //        //    if (methodInfo != null)
            //        //    {
            //        //        var paramNodes = GetParamDescriptionsFromXml(_xmlDocs, methodInfo);
            //        //        foreach (var param in operation.Value.Parameters)
            //        //        {
            //        //            if (paramNodes.ContainsKey(param.Name))
            //        //            {
            //        //                param.Extensions.Add("x-ms-summary", new Microsoft.OpenApi.Any.OpenApiString(paramNodes[param.Name]));
            //        //            }
            //        //        }
            //        //    }
            //        //}
            //    }
            //}
        }
        /// <summary>
        /// Add x-ms-connector-metadata at the document level
        /// </summary>
        /// <param name="swaggerDoc"></param>
        /// <param name="context"></param>
        private void AddMSConnectorMetadata(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Extensions.Add("x-ms-connector-metadata", new OpenApiArray
            {
                new OpenApiObject(){
                    ["propertyName"] = new OpenApiString("Support Email"),
                    ["propertyValue"] = new OpenApiString("support@meekou365.onmicrosoft.com")
                },
               new OpenApiObject(){
                    ["propertyName"] = new OpenApiString("Website"),
                    ["propertyValue"] = new OpenApiString("https://blog.meekou.cn")
                },
                new OpenApiObject(){
                    ["propertyName"] = new OpenApiString("Privacy policy"),
                    ["propertyValue"] = new OpenApiString("https://blog.meekou.cn/Privacy policy")
                },
                new OpenApiObject(){
                    ["propertyName"] = new OpenApiString("Categories"),
                    ["propertyValue"] = new OpenApiString("AI;Content and Files")
                }
            });
        }
        private Dictionary<string, string> GetParamDescriptionsFromXml(List<XPathDocument> xmlDocs, MethodInfo methodInfo)
        {
            var paramDescriptions = new Dictionary<string, string>();
            var methodName = $"{methodInfo.DeclaringType.FullName}.{methodInfo.Name}";

            // Construct the method signature with parameters
            var parameters = methodInfo.GetParameters();
            var paramTypes = string.Join(",", parameters.Select(p => p.ParameterType.FullName));
            var fullMethodName = $"M:{methodName}({paramTypes})";

            foreach (var xmlDoc in xmlDocs)
            {
                var methodNode = xmlDoc.CreateNavigator().SelectSingleNode($"/doc/members/member[@name='{fullMethodName}']");
                if (methodNode != null)
                {
                    var paramNodes = methodNode.Select("param");
                    while (paramNodes.MoveNext())
                    {
                        var paramName = paramNodes.Current.GetAttribute("name", string.Empty);
                        var paramDescription = paramNodes.Current.Value.Trim();
                        paramDescriptions[paramName] = paramDescription;
                    }
                }
            }

            return paramDescriptions;
        }

        #endregion
        #region OperationFilter
        /// <inheritdoc/>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            RemoveTextJonProduces(operation, context);
            AddSummaryForOperation(operation, context);
        }
        private void AddSummaryForOperation(OpenApiOperation operation, OperationFilterContext context)
        {
            foreach (var parameter in operation.Parameters)
            {
                parameter.Extensions.Add("x-ms-summary", new OpenApiString(parameter.Description));
            }
        }
        private void RemoveTextJonProduces(OpenApiOperation operation, OperationFilterContext context)
        {
            var producesToRemove = new List<string>() { "text/json", "application/*+json" };
            foreach (var produce in producesToRemove)
            {
                // remove from request body
                if (operation.RequestBody?.Content != null)
                {
                    operation.RequestBody.Content.Remove(produce);
                }

                foreach (var response in operation.Responses)
                {
                    response.Value.Content.Remove(produce);
                }
            }
        }
        #endregion
    }
}
