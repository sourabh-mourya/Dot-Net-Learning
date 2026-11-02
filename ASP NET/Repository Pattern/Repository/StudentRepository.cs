using ModelsInASPCore.Models;

namespace ModelsInASPCore.Repository
{
    public class StudentRepository : IStudent
    {
        public List<StudentModel> GetAllStudents()
        {
            return DataSource();
        }

        public StudentModel? GetStudentById(int id)
        {
            return DataSource()
                .FirstOrDefault(x => x.rollNo == id);
        }

        private List<StudentModel> DataSource()
        {
            var students = new List<StudentModel>
            {
                new StudentModel
                {
                    rollNo = 1,
                    Name = "Rahul",
                    Gender = "M",
                    Standard = 12
                },

                new StudentModel
                {
                    rollNo = 2,
                    Name = "Aman",
                    Gender = "M",
                    Standard = 11
                },

                new StudentModel
                {
                    rollNo = 3,
                    Name = "Priya",
                    Gender = "F",
                    Standard = 12
                },

                new StudentModel
                {
                    rollNo = 4,
                    Name = "Neha",
                    Gender = "F",
                    Standard = 10
                },

                new StudentModel
                {
                    rollNo = 5,
                    Name = "Rohit",
                    Gender = "M",
                    Standard = 11
                }
            };

            return students;
        }
    }
}