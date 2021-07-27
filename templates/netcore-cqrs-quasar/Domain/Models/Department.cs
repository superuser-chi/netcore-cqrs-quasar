using System;
namespace Domain.Models
{
    public class Department
    {
        public Guid Id { get; set; }
        public long Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}