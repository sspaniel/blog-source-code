using SQLite.Web.API.Entities;

namespace SQLite.Web.API.Services
{
    public interface IRepository
    {
        IEnumerable<User> GetUsers();
    }
}
