namespace DependencyInjectionServices.Services
{
    public class StudentService : IStudentService
    {
        //ab student Service ki jimmendari hi ki jinte bhi IStudentService ki method unko yaha pr body dena jauri hi ...
        public int GetStudentAge()
        {
            return 33;
        }

        public string GetStudentName()
        {
            return "Sourabh Mourya";
        }
    }
}