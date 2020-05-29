using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentNotes_MVC_SebastianPytel.Models
{
    public class StudentNote
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string UserId { get; set; }

        [Required]
        public string NoteLabel { get; set; }
        public string Note { get; set; }
    }
}
