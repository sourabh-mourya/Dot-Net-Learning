namespace DependencyInjectionServices.Services
{
    public class ServiceImplementation : IService1
    {
        private readonly Guid _id;

        public ServiceImplementation()
        {
            _id = Guid.NewGuid();
        }

        public Guid ServiceId()
        {
            return _id;
        }
    }
}