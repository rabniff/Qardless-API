﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QardlessAPI.Data;
using QardlessAPI.Data.Models;
using QardlessAPI.Data.Dtos.Admin;
using QardlessAPI.Data.Dtos.Authentication;

namespace QardlessAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        private readonly IQardlessAPIRepo _repo;
        private readonly IMapper _mapper;

        public AdminsController(IQardlessAPIRepo repo, IMapper mapper)
        {
            _repo = repo ??
                    throw new ArgumentNullException(nameof(repo));
            _mapper = mapper ??
                      throw new ArgumentNullException(nameof(mapper));
        }

        // GET: api/Admins
        [HttpGet("/admins")]
        public async Task<ActionResult<IEnumerable<Admin>>> AllAdmins()
        {
            var admins = await _repo.ListAllAdmins();

            if(admins == null)
                return NotFound();

            return Ok(_mapper.Map<IEnumerable<AdminReadDto>>(admins));
        }

        // GET: api/Admins/5
        [HttpGet("/admins/{id}")]
        public async Task<ActionResult<Admin>> AdminById(Guid id)
        {
            var admin = await _repo.GetAdminById(id);
            
            if(admin== null) return NotFound();

            return Ok(_mapper.Map<AdminReadDto>(admin));
        }

        // PUT: api/Admins/5
        [HttpPut("/admins/{id}")]
        public async Task<ActionResult> UpdateAdmin(Guid id, AdminUpdateDto adminUpdateDto)
        {
            if(adminUpdateDto == null) return BadRequest();

            var admin = await _repo.GetAdminById(id);
            if(admin == null) return BadRequest();

            await Task.Run(() => _repo.UpdateAdminDetails(id, adminUpdateDto));

            return Accepted(admin);
        }

        // POST: api/Admins    (REGISTER)
        [HttpPost("/admins")]
        public async Task<ActionResult<AdminCreateDto?>> RegisterNewAdmin(AdminCreateDto newAdmin)
        {
            if(newAdmin == null) return BadRequest();

            AdminPartialDto adminPartialDto = await Task.Run(() => _repo.AddNewAdmin(newAdmin));

            return Created("/admins", adminPartialDto);
        }

        //LOGOUT
        // POST: api/admins/logout
        [HttpPost("/admins/logout")]
        public async Task<ActionResult<LogoutResponseDto>> LogoutAdmin(
            [FromBody] LogoutRequestDto adminLogoutRequest)
        {
            var admin = await _repo.GetAdminById(adminLogoutRequest.Id);
            if (admin == null) return BadRequest();

            LogoutResponseDto adminLogoutResponse = new LogoutResponseDto
            {
                Id = adminLogoutRequest.Id,
                IsLoggedIn = false
            };

            return Ok(adminLogoutResponse);
        }

        // DELETE: api/Admins/5
        [HttpDelete("/admins/{id}")]
        public async Task<IActionResult> DeleteAdmin(Guid id)
        {
            var admin = await _repo.GetAdminById(id);
            if (admin == null) return BadRequest();

            _repo.DeleteAdmin(admin);
            _repo.SaveChanges();

            return Accepted();
        }

        private bool CheckAdminExists(Guid id)
        {
            var admin = _repo.GetAdminById(id);
            if (admin == null) 
                return false;

            return true;
        }
    }
}
