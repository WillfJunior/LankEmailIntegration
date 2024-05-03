using Microsoft.Extensions.Configuration;

namespace Infra.Data
{
    public  class LankContext
    {
        private readonly IConfiguration _configuration;

        public LankContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetConnectionString()
        {
            return _configuration.GetConnectionString("Default");
        }
    }
}
