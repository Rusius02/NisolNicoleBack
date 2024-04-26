namespace Infrastructure.SqlServer.Repository.Users
{
    public interface IJwtAuthentificationManager
    {
        string Authentificate(string pseudo, string password);
    }
}