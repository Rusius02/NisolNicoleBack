namespace Infrastructure.SqlServer.Repository.Users
{
    public interface IJwtAuthentificationManager
    {
        UserProxy Authentificate(string pseudo, string password);
    }
}