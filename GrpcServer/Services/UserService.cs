using Grpc.Core;
using GrpcServer.Common;
using GrpcServer.Repository;

namespace GrpcServer.Services
{
    public class UserService: user.userBase
    {
        private readonly IUserRepository _userRepository;
        public UserService(ILogger<UserService> logger, IUserRepository userRepository)
        {
            _userRepository = userRepository;            
        }

        public override async Task<LoginResponse> Login(LoginRequest request, ServerCallContext context)
        {
            var user = await _userRepository.Get(request);   
            var jsonStr = JsonCommon.ToJson(user);
            return new LoginResponse() { Data = JsonCommon.ToProtoByte(jsonStr) };
        }
    }
}