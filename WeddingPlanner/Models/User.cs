using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace WeddingPlanner.Models
{
    public class User : BaseEntity
    {
        [Key]
        public int UserId {get;set;}
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public string Email {get; set;}
        public string Password {get; set;}
        public DateTime Created_at {get;set;}
        public DateTime Updated_at{get;set;}

        public List<Guest> Guests {get; set;}
        public List<WeddingCreator> Weddings {get; set;}

        public User()
        {
            Guests = new List<Guest>();
            Weddings = new List<WeddingCreator>();

        }
    }
}