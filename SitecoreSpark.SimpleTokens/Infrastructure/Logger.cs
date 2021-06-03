namespace SitecoreSpark.CATS.Infrastructure
{
    /// <summary>
    /// Proxy logging methods for Sitecore diagnostics log.
    /// </summary>
    public static class Logger
    {
        public static void Info(string message, object owner)
        {
            Sitecore.Diagnostics.Log.Info($"[CATS] {message}", owner);
        }

        public static void Error(string message, object owner)
        {
            Sitecore.Diagnostics.Log.Error($"[CATS] {message}", owner);
        }

        public static void Exception(string message, object owner)
        {
            Sitecore.Diagnostics.Log.Fatal($"[CATS] {message}", owner);
        }

        public static void Warn(string message, object owner)
        {
            Sitecore.Diagnostics.Log.Warn($"[CATS] {message}", owner);
        }
    }
}