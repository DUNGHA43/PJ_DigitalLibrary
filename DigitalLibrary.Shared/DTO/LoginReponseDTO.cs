using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalLibrary.Shared.DTO
{
    public class LoginReponseDTO
    {
        public string message { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
