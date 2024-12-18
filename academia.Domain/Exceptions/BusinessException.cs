﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace academia.Domain.Exceptions
{
    public class BusinessException : Exception
    {
        //classe de exception personalizada
        public BusinessException()
        {
        }

        public BusinessException(string message) : base(message) { }

        public BusinessException(string message, Exception exception) : base(message, exception) { }
    }
}
