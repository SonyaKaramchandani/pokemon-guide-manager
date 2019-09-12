using System.Collections.ObjectModel;

namespace BlueDot.DiseasesAPI.Areas.HelpPage.ModelDescriptions
{
    /// <summary>
    /// Complex Type Model Description
    /// </summary>
    /// <seealso cref="BlueDot.DiseasesAPI.Areas.HelpPage.ModelDescriptions.ModelDescription" />
    public class ComplexTypeModelDescription : ModelDescription
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ComplexTypeModelDescription"/> class.
        /// </summary>
        public ComplexTypeModelDescription()
        {
            Properties = new Collection<ParameterDescription>();
        }

        /// <summary>
        /// Gets the properties.
        /// </summary>
        /// <value>
        /// The properties.
        /// </value>
        public Collection<ParameterDescription> Properties { get; private set; }
    }
}