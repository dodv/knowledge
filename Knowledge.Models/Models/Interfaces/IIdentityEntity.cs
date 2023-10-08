using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Knowledge.Models.Models.Interfaces
{
    /// <summary>
    /// Intended for entities that should manage their own Resource auth creation as opposed to IEntity
    /// </summary>
    public interface IIdentityEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        Guid Id { get; set; }
    }
}
