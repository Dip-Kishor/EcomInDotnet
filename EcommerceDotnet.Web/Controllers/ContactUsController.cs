using EcommerceDotnet.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceDotnet.Web.Controllers
{
    
    public class ContactUsController : Controller
    {
        private readonly IContactService _emailService;

        public ContactUsController(IContactService emailService)
        {
            _emailService = emailService;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Policy ="All")]
        [HttpPost]
        public async Task<IActionResult> SendEmail(string fname, string lname, string email, string message)
        {
            string subject = "New Contact Form Message";
            string body = $"Name: {fname} {lname}\nEmail: {email}\n\nMessage:\n{message}";

            // Send the email to your email address (handled server-side)
            await _emailService.SendEmail("dipkishor9910@gmail.com", subject, body);

            // Optionally, show a thank you message or redirect after submission
            return RedirectToAction("ThankYou");  // Redirect to a thank-you page
        }
        public IActionResult Thankyou() 
        {
            return View();
        }
    }
}
