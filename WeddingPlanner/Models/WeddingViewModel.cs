using System.ComponentModel.DataAnnotations;
using System;
namespace WeddingPlanner.Models
{
    public class WeddingViewModel : BaseEntity
    {
        // public class CustomDateAttribute : RangeAttribute
        // {
        // public CustomDateAttribute()
        // //This custom attribute validates date and allows for the use of Now.
        // : base(typeof(DateTime),
        //         DateTime.Now.AddYears(-200).ToString(),
        //         DateTime.Now.ToString())
        // { }
        // }
        [Required]
        [RegularExpression(@"^[a-zA-Z]+$")]
        public string WeddingOne { get; set; }
 

        [Required]
        [RegularExpression(@"^[a-zA-Z]+$")]
        public string WeddingTwo { get; set; }

        [Required]
        // [CustomDateAttribute]
        public DateTime Date { get; set; }
 
        [Required]
        public string WeddingAddress { get; set; }
    }
}