using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceDotnet.Services
{
    public interface IContactService
    {
        public Task SendEmail(string toEmail, string subject, string messageBody);
    }
}
