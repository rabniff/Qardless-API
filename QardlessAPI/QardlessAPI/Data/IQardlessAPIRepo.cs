using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Bson;
using QardlessAPI.Data.Dtos.Authentication;
using QardlessAPI.Data.Dtos.Business;
using QardlessAPI.Data.Dtos.Certificate;
using QardlessAPI.Data.Dtos.Employee;
using QardlessAPI.Data.Dtos.EndUser;
using QardlessAPI.Data.Models;

namespace QardlessAPI.Data
{
    public interface IQardlessAPIRepo
    {
        bool SaveChanges();

        #region Business
        Task<IEnumerable<Business>> ListAllBusinesses();
        Task<Business?> GetBusinessById(Guid id);
        Task<Business?> GetBusinessByEmail(LoginDto businessLogin);
        Task<Business?> UpdateBusinessDetails(Guid id, BusinessUpdateDto businessUpdate);
        BusinessReadPartialDto AddNewBusiness(BusinessCreateDto businessForCreation);
        void DeleteBusiness(Business? business);

        #endregion

        #region Certificate
        Task<IEnumerable<Certificate?>> ListAllCertificates();
        Task<IEnumerable<Certificate?>> GetCertificatesByEndUserId(Guid id);
        Task<Certificate?> GetCertificateById(Guid id);
        Task<Certificate?> UpdateCertificate(Guid id, CertificateUpdateDto certForUpdateDto);
        void AddNewCertificate(Certificate? certificate);
        void DeleteCertificate(Certificate? certificate);
        #endregion

        #region Changelog
        Task<IEnumerable<Changelog?>> GetChangelogs();
        Task<Changelog?> GetChangelog(Guid id);
        void PutChangelog(Guid id, Changelog? changelog);
        void PatchChangelog(Guid id, Changelog? changelog);
        void PostChangelog(Changelog? changelog);
        void DeleteChangelog(Changelog? changelog);
        #endregion

        #region Employee
        Task<IEnumerable<Employee>> ListAllEmployees();
        Task<IEnumerable<Employee?>> GetEmployeesByBusinessId(Guid id);
        Task<Employee?> GetEmployeeById(Guid id);
        Task<Employee?> UpdateEmployee(Guid id, EmployeeUpdateDto employeeUpdateDto);
        EmployeeReadPartialDto AddNewEmployee(EmployeeCreateDto newEmp);
        void DeleteEmployee(Employee? employee);
        #endregion

        #region EndUser
        Task<IEnumerable<EndUser>> ListAllEndUsers();
        Task<EndUser?> GetEndUserById(Guid id);
        Task<EndUser?> UpdateEndUserDetails(Guid id, EndUserUpdateDto endUserUpdateDto);
        EndUserReadPartialDto AddNewEndUser(EndUserCreateDto endUserForCreation);
        void DeleteEndUser(EndUser endUser);
        #endregion


        #region Security
        public string HashPassword(string Password);

        #endregion

    }
}
