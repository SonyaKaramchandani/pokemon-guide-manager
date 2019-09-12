using Swashbuckle.Swagger;
using System.Collections.Generic;
using System.Web.Http.Description;

namespace Biod.Zebra.Library.Infrastructures.AttributeFilter
{
    /// <summary>
    /// Class to inform swagger the required field in the header when calling the APIs with the token
    /// </summary>
    public class TokenAuthenticationHeader : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if (!apiDescription.ActionDescriptor.ControllerDescriptor.ControllerType.FullName.Contains("api.")
                || apiDescription.ActionDescriptor.ControllerDescriptor.ControllerType.FullName.Contains(".TokenController"))
            {
                return;
            }

            if (operation.parameters == null)
            {
                operation.parameters = new List<Parameter>();
            }

            operation.parameters.Add(new Parameter()
            {
                name = Constants.LoginHeader.TOKEN_AUTHORIZATION,
                @in = "header",
                type = "string",
                required = true
            });
        }
    }
}
