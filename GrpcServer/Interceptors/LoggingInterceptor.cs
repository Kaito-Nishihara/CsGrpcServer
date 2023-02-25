using Grpc.Core;
using Grpc.Core.Interceptors;

namespace GrpcServer.Interceptors
{
    public class LoggingInterceptor : Interceptor
    {
        private readonly ILogger<LoggingInterceptor> logger;

        public LoggingInterceptor(ILogger<LoggingInterceptor> logger)
        {
            this.logger = logger;
        }

        public override Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
        {
           logger.LogInformation($"★ {context.Host}, {context.Method}");
            return base.UnaryServerHandler(request, context, continuation);
        }
    }
}
