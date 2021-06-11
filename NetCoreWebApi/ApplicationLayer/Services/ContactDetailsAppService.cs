using ApplicationLayer.Common;
using ApplicationLayer.Interfaces;
using DataAccess.Interfaces;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Services
{
    public class ContactDetailsAppService : IContactDetailsApp
    {
        public readonly IRepository<ContactDetail> _contactRepository;
        public ContactDetailsAppService(IRepository<ContactDetail> contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<IEnumerable<ContactDetail>> GetContactList()
        {
            try
            {
                IEnumerable<ContactDetail> contactList = await _contactRepository.Get();

                if (contactList == null)
                {
                    throw new Exception(ErrorMessages.ContactListEmpty);
                }
                return contactList;
            }
            catch (Exception ex)
            {
                throw new Exception("GetContactList - " + ex.Message);
            }

        }
        
        public async Task<ContactDetail> GetContactDetailsById(int id)
        {
            if (id <= 0)
            {
                throw new Exception("GetContactDetailsById - " + ErrorMessages.ContactIdNull);
            }

            try
            {
                ContactDetail contactDetails = await _contactRepository.GetById(id);

                if (contactDetails == null)
                {
                    throw new Exception(ErrorMessages.ContactDetailsNull);
                }
                return contactDetails;
            }
            catch (Exception ex)
            {
                throw new Exception("GetContactDetailsById - " + ex.Message);
            }
        }

        public async Task<int> AddContactDetails(ContactDetail contactDetails)
        {
            if (contactDetails == null)
            {
                throw new Exception("AddContactDetails - " + ErrorMessages.ContactDetailsNull);
            }

            try
            {
                await this._contactRepository.Add(contactDetails);
                int result = await _contactRepository.SaveChangesAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("AddContactDetails - " + ex.Message);
            }

        }

        public async Task<int> UpdateContactDetails(ContactDetail contactDetails)
        {
            if (contactDetails.ContactId <= 0)
            {
                throw new Exception("UpdateContactDetails - " + ErrorMessages.ContactIdNull);
            }

            try
            {
                this._contactRepository.Update(contactDetails);
                int result = await _contactRepository.SaveChangesAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("UpdateContactDetails - " + ex.Message);
            }

        }
        public async Task<int> DeleteContactDetails(int id)
        {
            if (id <= 0)
            {
                throw new Exception("DeleteContactDetails - " + ErrorMessages.ContactIdNull);
            }

            try
            {
                ContactDetail contactDetails = await _contactRepository.GetById(id);

                if (contactDetails == null)
                {
                    throw new Exception(ErrorMessages.ContactDetailsNull);
                }

                contactDetails.Status = Convert.ToString(StatusEnum.InActive);

                _contactRepository.Update(contactDetails);
                var result = await _contactRepository.SaveChangesAsync();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("DeleteContactDetails - " + ex.Message);
            }
        }

    }
}

