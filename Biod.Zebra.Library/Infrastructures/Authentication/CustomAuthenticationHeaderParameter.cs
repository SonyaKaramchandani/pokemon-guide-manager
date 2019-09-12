using Swashbuckle.Swagger;
using System.Collections.Generic;
using System.Web.Http.Description;

namespace Biod.Zebra.Library.Infrastructures.AttributeFilter
{
    /// <summary>
    /// Class to inform swagger the required fields in the header when calling the APIs for the token.
    /// </summary>
    public class LoginAuthenticationHeader : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if (!apiDescription.ActionDescriptor.ControllerDescriptor.ControllerType.FullName.Contains(".TokenController"))
            {
                return;
            }

            if (operation.parameters == null)
            {
                operation.parameters = new List<Parameter>();
            }

            operation.parameters.Add(new Parameter()
            {
                name = Constants.LoginHeader.USERNAME,
                @in = "header",
                type = "string",
                required = true
            });

            operation.parameters.Add(new Parameter()
            {
                name = Constants.LoginHeader.PASSWORD,
                @in = "header",
                type = "string",
                format = "password",
                required = true
            });

            operation.parameters.Add(new Parameter()
            {
                name = Constants.LoginHeader.FIREBASE_DEVICE_ID,
                @in = "header",
                type = "string",
                required = true
            });
        }
    }
}
