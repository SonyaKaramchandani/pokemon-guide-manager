namespace Biod.Zebra.Library.Infrastructures.Log
{
    /// <summary>
    /// Interface for Console logging (allows dependency injection for the Console for console applications)
    /// </summary>
    public interface IConsoleLogger
    {
        /// <summary>
        /// Updates the console with the provided message.
        /// </summary>
        /// <param name="message">The message to write to the console</param>
        void UpdateConsole(string message);
    }
}
