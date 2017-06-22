using System.ComponentModel.DataAnnotations;
using System;
namespace WeddingPlanner.Models
{
    public class Guest : BaseEntity
    {
        [Key]
        public int GuestId {get;set;}
        public int UserId {get;set;}
        public User User { get; set; }
        public int WeddingId {get;set;}
        public WeddingCreator Wedding { get; set; }
    }
}