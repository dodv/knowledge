using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knowledge.Models.Models.DTOs
{
    public class User
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public DateTimeOffset Created { get; set; }
    }


    public class UserEnumerable
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public DateTimeOffset Created { get; set; }
        [Required]
        public bool DeviceMfaEnabled { get; set; }
        [Required]
        public DateTimeOffset LastActivity { get; set; }
    }
}
