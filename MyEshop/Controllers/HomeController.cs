using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyEshop.Data;
using MyEshop.Models;

namespace MyEshop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private MyEshopContext _context;

        public HomeController(ILogger<HomeController> logger , MyEshopContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var Products = _context.Products;
            return View(Products);
        }

        public IActionResult Detail(int Id)
        {
            var Product = _context.Products
                .Include(p => p.Item)
                .SingleOrDefault( p => p.Id == Id);


            if ( Product == null)
            {
                return NotFound();
            }

            var categories = _context.Products
                .Where(p => p.Id == Id)
                .SelectMany(c => c.categoryToProducts)
                .Select( ca=> ca.Category)
                .ToList();

            var vm = new DetailsViewModel()
            {
                Product = Product,
                Categories = categories
            };

            return View(vm);

            return null;
        }
        [Authorize]

        public IActionResult AddToCart(int itemId)
        {
            var product = _context.Products.Include(p => p.Item).SingleOrDefault(p => p.ItemId == itemId);

            if ( product != null)
            {
                int UserID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());
                var Order = _context.Orders.SingleOrDefault( o => o.UserId == UserID && !o.IsFinaly );

                if (Order != null)

                {
var orderdetails = _context.OrderDeatails.FirstOrDefault( d => d.OrderId == Order.OrderId && d.ProductId == product.Id);

                    if (orderdetails != null)
                    {
                        orderdetails.Count += 1;

                    }
                    else
                    {
                        _context.OrderDeatails.Add(new OrderDeatails()
                        {
                            OrderId = Order.OrderId,
                            ProductId = product.Id,
                            Price = product.Item.Price,
                            Count = 1

                        });

                    }

                }
                else
                {
                    Order = new Order()
                    {
                        IsFinaly = false,
                        CreateDate = DateTime.Now,  
                        UserId = UserID

                    };
                    _context.Orders.Add(Order);
                    _context.SaveChanges();
                    _context.OrderDeatails.Add(new OrderDeatails()
                    {
                        OrderId = Order.OrderId,
                        ProductId = product.Id , 
                        Price = product.Item.Price,
                        Count = 1

                    });
                }
                _context.SaveChanges();
            }



            return RedirectToAction("ShowCart");    
        }
        [Authorize]
        public IActionResult ShowCart()
        {

            int UserID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());
            var order = _context.Orders.Where(o => o.UserId == UserID && !o.IsFinaly)
                .Include(o => o.orderDeatails)
                .ThenInclude(c => c.Product).FirstOrDefault();

            return View(order);
        }

        [Authorize]

        public IActionResult RemoveCart(int detailId)
        {
            var orderdetail = _context.OrderDeatails.Find(detailId);
            _context.Remove(orderdetail);
            _context.SaveChanges();

            return RedirectToAction("ShowCart");
        }

        [Route("ContactUs")]
        public IActionResult ContactUs()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
