using System;
using System.Collections.Generic;
using System.Threading.Tasks;

#nullable disable

namespace DataAccess.Models
{
    public partial class ContactDetail
    {
        public int ContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Status { get; set; }

        public static explicit operator Task<object>(ContactDetail v)
        {
            throw new NotImplementedException();
        }
    }
}
