namespace Druin.Chef.Core.Authentication
{
    public interface IChefConnection
    {
        string ChefServer { get; set; }
        string PrivateKey { get; set; }
        string UserId { get; set; }
    }
}