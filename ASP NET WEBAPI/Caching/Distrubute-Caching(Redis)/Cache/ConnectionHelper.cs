
using StackExchange.Redis;

namespace Distrubute_Caching_Redis_.Cache
{
    public class ConnectionHelper
    {
        private static readonly Lazy<ConnectionMultiplexer> lazyConnection =
            new Lazy<ConnectionMultiplexer>(() =>
            {
                return ConnectionMultiplexer.Connect("127.0.0.1:6379");
            });

        public static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }
    }
}
