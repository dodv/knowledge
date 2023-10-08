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
    public class Link : IIdentityEntity
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        public string URL { get; set; }
        public int Status { get; set; }
    }
}
