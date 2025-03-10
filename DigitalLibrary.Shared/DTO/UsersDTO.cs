using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalLibrary.Shared.DTO
{
    public class UsersDTO
    {
        public int id { get; set; }
        public int? roleid { get; set; }
        public string? username { get; set; }
        public string? password { get; set; }
        public string? email { get; set; }
        public string? fullname { get; set; }
        public bool? gender { get; set; }
        public DateTime? birthday { get; set; }
        public string? phonenumber { get; set; }
        public string? identification {  get; set; }
        public string? address { get; set; }
        public bool status { get; set; }
    }
}
