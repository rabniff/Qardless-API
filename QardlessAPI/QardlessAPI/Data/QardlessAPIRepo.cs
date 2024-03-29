﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QardlessAPI.Data.Dtos.Authentication;
using QardlessAPI.Data.Dtos.Business;
using QardlessAPI.Data.Dtos.Certificate;
using QardlessAPI.Data.Dtos.Employee;
using QardlessAPI.Data.Dtos.EndUser;
using QardlessAPI.Data.Models;
using System.Security.Cryptography;
using System.Text;

namespace QardlessAPI.Data
{
    public class QardlessAPIRepo : IQardlessAPIRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public QardlessAPIRepo(ApplicationDbContext context, IMapper mapper)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context)); 
            _mapper = mapper ??
               throw new ArgumentNullException(nameof(mapper));
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

        #region Business
        public async Task<IEnumerable<Business>> ListAllBusinesses()
        {
            return await _context.Businesses.ToListAsync();
        }

        public async Task<Business?> GetBusinessById(Guid id)
        {
            return await _context.Businesses.FirstOrDefaultAsync(b => b.Id == id);
        }

        // For login
        public async Task<Business?> GetBusinessByEmail(LoginDto businessLogin)
        {
            return await _context.Businesses.FirstOrDefaultAsync(
                e => e.Email == businessLogin.Email);
        }

        public async Task<Business?> UpdateBusinessDetails(Guid id, BusinessUpdateDto businessUpdate)
        {
            Business? business = await _context.Businesses.FirstOrDefaultAsync(b => b.Id == id);

            business.Title = businessUpdate.Title;
            business.Email = businessUpdate.Email;
            business.Phone = businessUpdate.Phone;

            _context.SaveChanges();
            _context.Businesses.Add(business);

            return await _context.Businesses.FirstOrDefaultAsync(b => b.Id == id);
        }

        public BusinessReadPartialDto AddNewBusiness(BusinessCreateDto businessForCreation)
        {
            if (businessForCreation == null)
                throw new ArgumentNullException(nameof(businessForCreation));

            Business business = new Business()
            {
                Id = new Guid(),
                Title = businessForCreation.Title,
                Email = businessForCreation.Email,
                Phone = businessForCreation.Phone,
                CreatedDate = DateTime.Now
            };

            _context.Businesses.Add(business);
            _context.SaveChanges();

            BusinessReadPartialDto businessReadPartialDto = new BusinessReadPartialDto
            {
                Id = business.Id,
                Title = business.Title,
                Email = business.Email,
                Phone = business.Phone
            };

            return businessReadPartialDto;
        }

        public void DeleteBusiness(Business? business)
        {
            if (business == null)
                throw new ArgumentNullException(nameof(business));

            _context.Businesses.Remove(business);
        }
        #endregion

        #region Certificate
        public async Task<IEnumerable<Certificate?>> ListAllCertificates()
        {
            return await _context.Certificates.ToListAsync();
        }

        public async Task<IEnumerable<Certificate?>> GetCertificatesByEndUserId(Guid id)
        {
            return await _context.Certificates
                .Where(c => c.EndUserId == id).ToListAsync();
        }

        public async Task<Certificate?> GetCertificateById(Guid id)
        {
            return await _context.Certificates.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Certificate?> UpdateCertificate(Guid id, CertificateUpdateDto certForUpdateDto)
        {
            Certificate? cert = await _context.Certificates.FirstOrDefaultAsync(c => c.Id == id);

            cert.CourseTitle = certForUpdateDto.CourseTitle;
            cert.CertNumber = certForUpdateDto.CertNumber;
            cert.CourseDate = certForUpdateDto.CourseDate;
            cert.ExpiryDate = certForUpdateDto.ExpiryDate;
            cert.PdfUrl = certForUpdateDto.PdfUrl;
            cert.CreatedDate = DateTime.Now;
            cert.EndUserId = certForUpdateDto.EndUserId;
            cert.BusinessId = certForUpdateDto.BusinessId;
            
            _context.SaveChanges();
            _context.Certificates.Add(cert);

            return await _context.Certificates.FirstOrDefaultAsync(c => c.Id == id);
        }

        public void AddNewCertificate(Certificate? certificate)
        {
            if (certificate == null)
                throw new ArgumentNullException(nameof(certificate));

            certificate.Id = Guid.NewGuid();
            certificate.CreatedDate = DateTime.Now;

            _context.Certificates.Add(certificate);
        }

        public void DeleteCertificate(Certificate? certificate)
        {
            if (certificate == null)
                throw new ArgumentNullException(nameof(certificate));

            _context.Certificates.Remove(certificate);
        }
        #endregion

        #region Changelog
        public async Task<IEnumerable<Changelog?>> GetChangelogs()
        {
            return await _context.Changelogs.ToListAsync();
        }

        public async Task<Changelog?> GetChangelog(Guid id)
        {
            return await _context.Changelogs.FirstOrDefaultAsync(c => c.Id == id);
        }

        public void PutChangelog(Guid id, Changelog? changelog)
        {
            // Implemented in the controller
        }

        public void PatchChangelog(Guid id, Changelog? changelog)
        {
            // Implemented in the controller
        }

        public void PostChangelog(Changelog? changelog)
        {
            if (changelog == null)
            {
                throw new ArgumentNullException(nameof(changelog));
            }

            changelog.Id = Guid.NewGuid();
            changelog.CreatedDate = DateTime.Now;

            _context.Changelogs.Add(changelog);
        }

        public void DeleteChangelog(Changelog? changelog)
        {
            if (changelog == null)
            {
                throw new ArgumentNullException(nameof(changelog));
            }

            _context.Changelogs.Remove(changelog);
        }
        #endregion

        #region Employee
        public async Task<IEnumerable<Employee>> ListAllEmployees()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<IEnumerable<Employee?>> GetEmployeesByBusinessId(Guid id)
        {
            return await _context.Employees
                .Where(e => e.BusinessId == id).ToListAsync();
        }

        public async Task<Employee?> GetEmployeeById(Guid id)
        {
            return await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
        }



        public async Task<Employee?> UpdateEmployee(Guid id, EmployeeUpdateDto employeeUpdateDto)
        {
            Employee? emp = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);

            emp.Name = employeeUpdateDto.Name;
            emp.BusinessId = employeeUpdateDto.BusinessId;

            _context.SaveChanges();
            _context.Employees.Add(emp);

            return await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
        }

        public EmployeeReadPartialDto AddNewEmployee(EmployeeCreateDto newEmp)
        {
            if (newEmp == null)
                throw new ArgumentNullException(nameof(newEmp));

            Employee emp = new Employee
            {
                Id = new Guid(),
                Name = newEmp.Name,
                BusinessId = newEmp.BusinessId
            };
            
            _context.Employees.Add(emp);
            _context.SaveChanges();

            EmployeeReadPartialDto empPartialRead = new EmployeeReadPartialDto
            {
                Id = emp.Id,
                Name = emp.Name,
                BusinessId = emp.BusinessId
            };
            
            return empPartialRead;
        }

        public void DeleteEmployee(Employee? emp)
        {
            if (emp == null)
                throw new ArgumentNullException(nameof(emp));

            _context.Employees.Remove(emp);
        }
        #endregion

        #region EndUser
        public async Task<IEnumerable<EndUser>> ListAllEndUsers()
        {
            return await _context.EndUsers.ToListAsync();
        }

        public async Task<EndUser?> GetEndUserById(Guid id)
        {
            return await _context.EndUsers.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<EndUser?> UpdateEndUserDetails(Guid id, EndUserUpdateDto endUserUpdateDto)
        {
            EndUser? endUser = await _context.EndUsers.FirstOrDefaultAsync(e => e.Id == id);

            endUser.Name = endUserUpdateDto.Name;

            _context.SaveChanges();
            _context.EndUsers.Add(endUser);

            return await _context.EndUsers.FirstOrDefaultAsync(e => e.Id == id);
        }

        public EndUserReadPartialDto AddNewEndUser(EndUserCreateDto endUserForCreation)
        {
            if (endUserForCreation == null)
                throw new ArgumentNullException(nameof(endUserForCreation));

            EndUser endUser = new EndUser
            {
                Id = new Guid(),
                Name = endUserForCreation.Name
            };

            _context.EndUsers.Add(endUser);
            _context.SaveChanges();

            EndUserReadPartialDto endUserReadPartialDto = new EndUserReadPartialDto
            {
                Id = endUser.Id,
                Name = endUser.Name,
                IsLoggedIn = false
            };
           
            return endUserReadPartialDto;
        }


        public void DeleteEndUser(EndUser? endUser)
        {
            if (endUser == null)
                throw new ArgumentNullException(nameof(endUser));

            _context.EndUsers.Remove(endUser);
        }

        #endregion

        #region Security 
        public string HashPassword(string password)
        {
            var sha = SHA256.Create();
            var asByteArray = Encoding.Default.GetBytes(password);
            var hashedPassword = sha.ComputeHash(asByteArray);
            return Convert.ToBase64String(hashedPassword);
        }
        #endregion

    }
}
