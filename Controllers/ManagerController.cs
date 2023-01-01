using Coffee_Project.Data;
using Coffee_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coffee_Project.Controllers
{

    public class ManagerController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public ManagerController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [Route("/pleasedrinkdecaf")]
        public IActionResult Index()
        {
            var users = dbContext.Users.Select(c => MapUserObj(c));
            return View(users);
        }

        private static UsersDto MapUserObj(ApplicationUser c)
        {
            return new UsersDto()
            {
                Id = c.Id,
                UserName = c.UserName,
                Credit = c.Credit,
            };
        }

        [HttpGet]
        public IActionResult Edit(string Id)
        {
            var user = dbContext.Users.FirstOrDefault(u => u.Id == Id);
            if(user != null)
            {
                return View(MapUserObj(user));
            }
            return View (new UsersDto());
           
        }

        [HttpPost]
        public IActionResult Edit(UsersDto userDto)
        {
            var user = dbContext.Users.FirstOrDefault(u => u.Id == userDto.Id);
            if(user != null)
            {
                user.Credit = userDto.Credit;
                dbContext.Users.Update(user);
                dbContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }


    }
}
