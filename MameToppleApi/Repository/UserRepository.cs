using MameToppleApi.Models;

namespace MameToppleApi.Repository
{
    public class UserRepository : BaseRepository<User, string>
    {
        public UserRepository(ToppleDBContext _context) : base(_context)
        {
        }
    }
}