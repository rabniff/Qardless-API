﻿namespace QardlessAPI.Data.Dtos.Employee
{
    public class EmployeeReadFullDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool EmailVerified { get; set; }
        public string? ContactNumber { get; set; }
        public bool? ContactNumberVerified { get; set; }
        public int PrivilegeLevel { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public Guid BusinessId { get; set; }
    }
}
