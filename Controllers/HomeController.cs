using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DemoMiddleware.Models;
using System.Linq.Expressions;

namespace DemoMiddleware.Controllers
{
    public class Student
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public int Age { get; set; }
    }
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly int a = 10;


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            Student student = new Student();
            Func<int, int> func = (x) => x * x;

            Predicate<Student> IsNULLStudentNamePredicate = (student) => {
                Console.WriteLine($"StudentID = {student.StudentID}");
                Console.WriteLine($"StudentName = {student.StudentName}");
                Console.WriteLine($"Age = {student.Age}");
                if (string.IsNullOrEmpty(student.StudentName))
                    return true;
                return false;
            };

            Console.WriteLine("Chua gan");
            bool IsNULLStudentName = IsNULLStudentNamePredicate(student);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("IsNULLStudentName = " + IsNULLStudentName);

            Console.WriteLine("Da Gan");
            student.StudentName = "Nguyen Van C";
            student.StudentID = 1;
            student.Age = 24;
            IsNULLStudentName = IsNULLStudentNamePredicate(student);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("IsNULLStudentName = " + IsNULLStudentName);



            return View();
        }

        [NonAction]
        private void PrintSum(int input1, int input2)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{input1} + {input2} = " + (input1 + input2));
        }

        [NonAction]
        private void PrintSubtraction(int input1, int input2)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{input1} - {input2} = " + (input1 - input2));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
