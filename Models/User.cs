using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace dotnetApi.Models
{
    public class User : IdentityUser
    {
        public List<Portfolio> Portfolios { get; set; } = [];
    }
}