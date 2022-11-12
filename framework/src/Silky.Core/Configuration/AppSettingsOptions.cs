namespace Silky.Core.Configuration
{
    public class AppSettingsOptions
    {
        public static string AppSettings = "AppSettings";

        public AppSettingsOptions()
        {
            DisplayFullErrorStack = false;
            AutoValidationParameters = true;
        }

        /// <summary>
        /// Whether to display stack information
        /// </summary>
        public bool DisplayFullErrorStack { get; set; }

        /// <summary>
        /// whether to record EFCore Sql execute command log
        /// </summary>
        public bool? LogEntityFrameworkCoreSqlExecuteCommand { get; set; }


        /// <summary>
        /// Whether to automatically verify input parameters
        /// </summary>
        public bool AutoValidationParameters { get; set; }
        
    }
}