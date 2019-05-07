
using System;
using System.ComponentModel.DataAnnotations;

namespace PersonApi.Models
{
    public class Person
    {
        public long id { get; set; }

        [Required, RegularExpression(@"^[A-Z]\D*$"), StringLength(20, MinimumLength = 2)]
        public string name { get; set; }

        [DataType(DataType.Date)]
        public DateTime birthdate { get; set; }

        [EmailAddress]
        public string email { get; set; }

        [RegularExpression(@"^[0-9]{9}$")]
        public long phone { get; set; }

    }
}
