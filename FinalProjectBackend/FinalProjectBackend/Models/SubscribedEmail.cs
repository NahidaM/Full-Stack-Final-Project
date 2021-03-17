using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectBackend.Models
{
    public class SubscribedEmail
    {
        public int Id { get; set; }
        [Required, EmailAddress, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public bool HasDeleted { get; set; }
        public DateTime? DeletedTime { get; set; }
    }
}
