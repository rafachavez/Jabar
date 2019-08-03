using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jabar.Data;
using Jabar.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Jabar.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class AdminUsersModel : PageModel
    {
        public readonly ApplicationDbContext _db;
        
        public AdminUsersModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<ApplicationUser> ApplicationUser { get; private set; }

        public IActionResult OnGet()
        {
            ApplicationUser = _db.ApplicationUser.ToList();
            return Page();
        }
    }
}
