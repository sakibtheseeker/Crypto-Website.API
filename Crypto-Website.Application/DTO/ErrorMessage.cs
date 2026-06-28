using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypto_Website.Application.DTO
{
    public class ErrorMessage
    {
        public int statusCode { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
    }
}
