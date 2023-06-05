using LeapYearApp.Data;
using LeapYearApp.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;

namespace LeapYearApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        [BindProperty]
        public LeapYear leap_year{get; set;} = new LeapYear();

        private readonly IHttpContextAccessor _contextAccessor;
        private readonly helper _context;

        public IndexModel(ILogger<IndexModel> logger, helper context, IHttpContextAccessor contextAccessor)
        {
            _logger = logger;
            _context = context;
            _contextAccessor = contextAccessor;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            this.leap_year.Data = DateTime.Now;
            if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var user_id = _contextAccessor.HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
                this.leap_year.user_id = user_id.Value;
                this.leap_year.login = _contextAccessor.HttpContext.User.Identity.Name;
            }

            string leap_yearText;

            if ((this.leap_year.Rok % 4 == 0 && this.leap_year.Rok % 100 != 0) || this.leap_year.Rok % 400 == 0)
            {
                leap_yearText = "To był rok przestępny.";
                this.leap_year.czy_przestepny = "rok przestępny";
            }
            else
            {
                leap_yearText = "To nie był rok przestępny.";
                this.leap_year.czy_przestepny = "rok nieprzestępny";
            }

            string result;
            if (!string.IsNullOrEmpty(this.leap_year.Imie) && !string.IsNullOrEmpty(this.leap_year.Rok.ToString()))
            {
                if (this.leap_year.Imie.EndsWith("a", StringComparison.OrdinalIgnoreCase))
                {
                    result = this.leap_year.Imie + " urodziła się w " + this.leap_year.Rok + " roku. " + leap_yearText;
                }
                else
                {
                    result = this.leap_year.Imie + " urodził się w " + this.leap_year.Rok + " roku. " + leap_yearText;
                }
                ViewData["message"] = result;
                _context.LeapData.Add(this.leap_year);
                _context.SaveChanges();
            }
            else if (!string.IsNullOrEmpty(this.leap_year.Rok.ToString()))
            {
                result = this.leap_year.Rok + " rok. " + leap_yearText;
                ViewData["message"] = result;
                _context.LeapData.Add(this.leap_year);
                _context.SaveChanges();
            }

            return Page();
        }
    }
}