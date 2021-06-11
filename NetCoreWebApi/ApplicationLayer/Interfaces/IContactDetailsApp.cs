using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Interfaces
{
    public interface IContactDetailsApp
    {
        Task<IEnumerable<ContactDetail>> GetContactList();
        //Task<IEnumerable<ContactDetail>> GetContactDetailsById(Expression<Func<ContactDetail, bool>>
        //    predicate);
        Task<ContactDetail> GetContactDetailsById(int id);
        Task<int> AddContactDetails(ContactDetail employee);
        Task<int> UpdateContactDetails(ContactDetail employee);
        Task<int> DeleteContactDetails(int id);

    }
}
