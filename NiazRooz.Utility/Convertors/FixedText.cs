﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NiazRooz.Utility
{
    public class FixedText
    {
        public static string FixEmail(string email)
        {
            return email.Trim().ToLower();
        }
    }
}