using Knowledge.Models.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knowledge.Models.Models.Entities
{
    public class User : IIdentityEntity
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(128)]
        public string Email { get; set; }
        [MaxLength(64)]
        public string? Phone { get; set; }
        [MaxLength(256)]
        public string? Address { get; set; }
        public DateTimeOffset? Deleted { get; set; }
        [Required]
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Updated { get; set; }
        [Required]
        public ICollection<AssignedUserRole> Roles { get; set; } = new List<AssignedUserRole>();
    }
}
