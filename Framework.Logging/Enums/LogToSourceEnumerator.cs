namespace Framework.Logging
{
    /// <summary>
    /// This enumerator is in charge of identifiyng the log engine data repository
    /// </summary>
    public enum LogToSourceEnumerator
    {
        /// <summary>
        /// Off
        /// </summary>
        None        = 0,

        /// <summary>
        /// Plain text 
        /// </summary>
        File        = 1,

        /// <summary>
        /// Event Viewer
        /// </summary>
        EventViewer = 2,

        /// <summary>
        /// Plain text and EventViewer
        /// </summary>
        Both = File | EventViewer
    }
}