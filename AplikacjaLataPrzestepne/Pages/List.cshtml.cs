using LeapYearApp.Data;
using LeapYearApp.Forms;
using ListLeapYears;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace LeapYearApp.Pages
{
    public class ListModel : PageModel
    {
        public IEnumerable<LeapYear> LeapYearList;
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration Configuration;
        private readonly helper _context;
        private readonly IHttpContextAccessor _contextAccessor;

        public ListModel(ILogger<IndexModel> logger, helper context, IConfiguration configuration, IHttpContextAccessor contextAccessor)
        {
            _logger = logger;
            _context = context;
            Configuration = configuration;
            _contextAccessor = contextAccessor;
        }
        public LeapYear find { get; set; } = new LeapYear();
        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public PaginatedList<LeapYear> LeapYear { get; set; }
        public async Task OnGetAsync(string sortOrder,
            string currentFilter, string searchString, int? pageIndex)
        {
            if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var user_id = _contextAccessor.HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
                find.user_id = user_id.Value;
                
            }
            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_asc" : "Date";
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }
             
            IQueryable<LeapYear> uzytkownicy = from s in _context.LeapData.OrderByDescending(x=>x.Data) select s;
            
            var pageSize = Configuration.GetValue("PageSize", 20);
            LeapYear = await PaginatedList<LeapYear>.CreateAsync(
                uzytkownicy.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
        public IActionResult OnPost(int id_User)
        {    
            find = _context.LeapData.Find(id_User);
            
            find.Id = id_User;
            _context.LeapData.Remove(find);
            _context.SaveChanges();
            
            return RedirectToAction("Async");
        }
    }
}