namespace BlueDot.DiseasesAPI.Areas.HelpPage.ModelDescriptions
{
    /// <summary>
    /// Collection Model Description
    /// </summary>
    /// <seealso cref="BlueDot.DiseasesAPI.Areas.HelpPage.ModelDescriptions.ModelDescription" />
    public class CollectionModelDescription : ModelDescription
    {
        /// <summary>
        /// Gets or sets the element description.
        /// </summary>
        /// <value>
        /// The element description.
        /// </value>
        public ModelDescription ElementDescription { get; set; }
    }
}