using System.Diagnostics;
using HTNL.Edu.Models;
using Microsoft.AspNetCore.Mvc;

namespace HTNL.Edu.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        // Optional: Handle contact form submission
        [HttpPost]
        public IActionResult Contact(ContactFormModel model)
        {
            if (ModelState.IsValid)
            {
                // Here you can:
                // 1. Send email
                // 2. Save to database
                // 3. Send notification

                TempData["SuccessMessage"] = "Cảm ơn bạn! Tin nhắn của bạn đã được gửi thành công.";
                return RedirectToAction(nameof(Contact));
            }

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }

    // Optional: Contact Form Model
    public class ContactFormModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
