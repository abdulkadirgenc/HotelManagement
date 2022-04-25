using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel;

namespace HotelManagement.Api.Requests
{
    public class LoginRequestDto
    {
        [BindRequired]
        [DefaultValue("hotelmanagement@outlook.com")]
        public string Email { get; set; }
        [BindRequired]
        [DefaultValue("P@ssw0rd!")]
        public string Password { get; set; }
    }
}