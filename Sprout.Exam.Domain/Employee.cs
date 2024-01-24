using System;
using System.ComponentModel.DataAnnotations;

namespace Sprout.Exam.Domain
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
    }
}
