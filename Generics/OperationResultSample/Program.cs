using Generics.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationResultSample
{
    class Program
    {
        static void Main(string[] args)
        {
            OperationResult<Student> studentResult = DoSomeStuff(12);
            if (studentResult.Success)
            {
                Console.WriteLine("Student {0} name {1}", 
                    studentResult.Result.Id, studentResult.Result.Name);
            }
            else
            {
                Console.WriteLine(studentResult.Exception);
            }

            Console.ReadLine();
        }

        public static OperationResult<Student> DoSomeStuff(int studentId)
        {
            if (studentId > 0)
            {
                Student student = QueryDatabase<Student>(studentId);
                return OperationResult<Student>.CreateSuccessResult(student);
            }
            else
            {
                return OperationResult<Student>
                    .CreateFailure(new ApplicationException(nameof(studentId) + " must be bigger than zero"));
            }
        }

        public static Student QueryDatabase<T>(int studentId)
        {
            return new Student() { Id = 1056, Name = "Berkay Başöz" };
        }
    }
}
