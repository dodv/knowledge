using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Knowledge.Models.Models.DTOs
{
    public class PagedResults<T>
    {
        [Required]
        public bool HasMore { get; set; }
        [Required]
        public List<T> Data { get; set; }

    }
}