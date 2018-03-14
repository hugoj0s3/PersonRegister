using System;

namespace ProjectRegistration.FrontEnd.Models
{
    public class PersonViewModel
    {
        public Guid? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}