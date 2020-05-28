namespace Druin.Chef.Core.Authentication
{
    public class ChefConnection : IChefConnection
    {
        public string ChefServer { get; set; }
        public string PrivateKey { get; set; }
        public string UserId { get; set; }
    }
}