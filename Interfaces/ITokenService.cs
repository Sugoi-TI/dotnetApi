using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetApi.Models;

namespace dotnetApi.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}