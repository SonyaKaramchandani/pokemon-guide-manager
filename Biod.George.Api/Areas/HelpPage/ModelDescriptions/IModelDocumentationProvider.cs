using System;
using System.Reflection;

namespace BlueDot.DiseasesAPI.Areas.HelpPage.ModelDescriptions
{
    /// <summary>
    /// IModel Documentation Provider
    /// </summary>
    public interface IModelDocumentationProvider
    {
        /// <summary>
        /// Gets the documentation.
        /// </summary>
        /// <param name="member">The member.</param>
        /// <returns></returns>
        string GetDocumentation(MemberInfo member);

        /// <summary>
        /// Gets the documentation.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        string GetDocumentation(Type type);
    }
}