using System;
using System.Collections.Generic;
using System.Text;

namespace NiazRooz.Utility
{
    public class NameGenerator
    {
        public static string GenerateUniqCode()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}
