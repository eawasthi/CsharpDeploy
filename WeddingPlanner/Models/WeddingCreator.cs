using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace WeddingPlanner.Models
{
    public class WeddingCreator
    {
        
        [Key]
        public int WeddingId {get;set;}
        public string WeddingOne {get; set;}
        public string WeddingTwo {get; set;}
        public DateTime Date {get; set;}
        public string WeddingAddress {get; set;}
        public DateTime Created_at {get;set;}
        public DateTime Updated_at{get;set;}
        public int UserId { get; set; }
        public User User { get; set; }
        public List<Guest> Guests {get;set;}
        public WeddingCreator() {

            Guests = new List<Guest>();
        }
    }
}