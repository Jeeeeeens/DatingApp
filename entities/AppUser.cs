using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.entities
{
    public class AppUser
    {
        [Key]
        public int Id { get; set; } 
        public string? UserName { get; set; }
        
        public byte[]? PasswordHash {get;set;}
        public byte[]? PaswordSalt {get;set;}

    }
}