namespace ServiceInterface
{
    public interface IUserRepository
    {
        bool NameInUse(string name);
    }
}