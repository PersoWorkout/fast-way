using Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Errors
{
    public static class AuthErrors
    {
        public readonly static Error InvalidCreadentials = new(
            "Authentication.InvalidCredantials", 
            "The creadentials are not valid");
    }
}
