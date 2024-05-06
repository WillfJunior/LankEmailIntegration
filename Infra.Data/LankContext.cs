using Microsoft.Extensions.Configuration;

namespace Infra.Data
{
    public  class LankContext
    {
        private readonly IConfiguration _configuration;
        public string Server { get; set; }
        public string Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }

        public LankContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetConnectionString()
        {
            Server = _configuration.GetSection("EnvDB:Server").Value;
            Port = _configuration.GetSection("EnvDB:Port").Value;
            Username = _configuration.GetSection("EnvDB:UserName").Value;
            Password = _configuration.GetSection("EnvDB:Password").Value;
            Database = _configuration.GetSection("EnvDB:DataBase").Value;
            
            return $"Server={Server};Port={Port};Uid={Username};Pwd={Password};Database={Database}";
        }
    }
}
