using System.ComponentModel.DataAnnotations;
using Silky.Rpc.Runtime.Server;

namespace ITestApplication.Test.Dtos
{
    public class TestInput
    {
       
        [HashKey]
        [Required(ErrorMessage = "Name is not allowed to be empty")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Address is not allowed to be empty")]
        [CacheKey(1)]
        public string Address { get; set; }
        
        [CacheKey(0)]
        public long? Id { get; set; }
    }
}