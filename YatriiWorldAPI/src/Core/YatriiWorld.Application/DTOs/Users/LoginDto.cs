using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YatriiWorld.Application.DTOs.Users
{
    public class LoginDto()
    {
        public string UserNameOrEmail { get; set; }
        public string Password { get; set; }
    }
}
