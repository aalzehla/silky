namespace SilkyApp.Application.Contracts.System.Dtos
{
    public class GetSystemInfoOutput
    {
        /// <summary>
        /// hostname
        /// </summary>
        public string HostName { get; set; }
        
        /// <summary>
        /// Operating environment
        /// </summary>
        public string Environment { get; set; }
        
        /// <summary>
        /// service address
        /// </summary>
        public string[] Addresses { get; set; }
    }
}