using Coffee_Project.Data;
using Coffee_Project.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coffee_Project.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public OrdersController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var user = dbContext.Users.FirstOrDefault(c => c.UserName == User.Identity.Name);
            ViewBag.Credit = user.Credit;
            var Drinks = dbContext.Products.Select(c => new Order()
            {
                Id = c.Id,
                DrinkType = c.Drink,
                Name = c.Drink.ToString(),
                Price = c.price,
                Count = 0
            }).ToList();
            return View(Drinks);
        }

        [HttpPost]
        public IActionResult ProccessOrder(List<Order> orders)
        {
            var totalPrice = orders.Sum(c => c.Count * c.Price);

            if(totalPrice==0)
            {
                ViewBag.IsDone = "0";
                return View();
            }
            var user = dbContext.Users.FirstOrDefault(c => c.UserName == User.Identity.Name);
            if (user.Credit < totalPrice)
            {
                ViewBag.IsDone = "0";
                return View();
            }
            user.Credit -= totalPrice;
            dbContext.Users.Update(user);
            dbContext.SaveChanges();
            ViewBag.IsDone = "1";

            return View();
        }
    }
}
