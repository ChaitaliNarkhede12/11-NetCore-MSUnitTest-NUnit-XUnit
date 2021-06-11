using ApplicationLayer.Common;
using ApplicationLayer.Interfaces;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        public readonly IContactDetailsApp _conatctService;
        public ContactController(IContactDetailsApp conatctService)
        {
            this._conatctService = conatctService;
        }

        [HttpGet("getContactList")]
        public async Task<IActionResult> Get()
        {
            var result = await _conatctService.GetContactList();
            result = result.ToList().Where (x => x.Status == StatusEnum.Active.ToString());
            return Ok(result);
        }


        [HttpGet("getContactById/{id}")]
        public async Task<IActionResult> GetContactById(int id)
        {
            var result = await _conatctService.GetContactDetailsById(id);
            return Ok(result);
        }


        [HttpPost("saveContactDetails")]
        public async Task<IActionResult> SaveContactDetails(ContactDetail contactDetails)
        {
            contactDetails.Status = Convert.ToString(StatusEnum.Active);
            var result = await _conatctService.AddContactDetails(contactDetails);
            return Ok(result);
        }


        [HttpPut("updateContactDetails")]
        public async Task<IActionResult> UpdateContactDetails(ContactDetail contactDetails)
        {
            var result = await _conatctService.UpdateContactDetails(contactDetails);
            return Ok(result);
        }

        [HttpDelete("deleteContactDetails/{id}")]
        public async Task<IActionResult> DeleteContactDetails(int id)
        {
            var result = await _conatctService.DeleteContactDetails(id);
            return Ok(result);
        }
    }
}
