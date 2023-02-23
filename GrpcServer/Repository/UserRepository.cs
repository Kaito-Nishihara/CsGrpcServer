using GrpcServer.Entites;
using Microsoft.EntityFrameworkCore;

namespace GrpcServer.Repository
{
    public interface IUserRepository
    {
        Task<User?> Get(LoginRequest loginRequest);
    }

    public class UserRepository: IUserRepository
    {
        private readonly GrpcDbContext _context;

        public UserRepository(GrpcDbContext context)
        {
            _context = context;
        }

        public async Task<User?> Get(LoginRequest loginRequest)
        {
            /*var user = await _context.User.FirstOrDefaultAsync(x => x.Email.Equals(loginRequest.Email) && x.Password.Equals(loginRequest.Password));
            return user;*/

            var user = await _context.User.FirstOrDefaultAsync(x => x.Email.Equals(loginRequest.Email));
            if (user is  null) throw new Exception("Emailが間違っています");
            if (user.Password != loginRequest.Password) throw new Exception("Passwordが間違っています。");
            return user;


        }
    }
}
