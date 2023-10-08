using Knowledge.Models.Common.Enum;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Knowledge.Models.Models.Entities
{
    public class AssignedUserRole
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public UserRole Role { get; set; }

        public DateTimeOffset? Deleted { get; set; }
    }
}
