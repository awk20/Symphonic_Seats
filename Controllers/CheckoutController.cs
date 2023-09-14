using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
//using Stripe.BillingPortal;
using SymphonicSeats2.Models;
using Stripe.Checkout;
using Stripe;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Authorization;

namespace SymphonicSeats2.Controllers
{
    public class CheckoutController : Controller
    {

        private readonly CollectionContext _context;
        private readonly IWebHostEnvironment Environment;

        public CheckoutController(CollectionContext context, IWebHostEnvironment environment)
        {
            _context = context;
            this.Environment = environment;
        }
        public async Task<IActionResult> Index()
        {
            return Redirect("/");
        }

        // confirms order and redirects accordingly 
        public IActionResult OrderConfirmation(int id)
        {
            var service = new SessionService();
            Session session = service.Get(TempData["Session"].ToString());

            if (session.PaymentStatus == "paid")
            {
                var transaction = session.PaymentIntentId.ToString();
                return View("Success");
            }
            else
            {
                return View("Login");
            }
        }

        public IActionResult Success()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [Authorize]
        public IActionResult Checkout(int id)
        {
            var domain = "http://localhost:5270/";

            // Should get the database element based on id which will be passed in 
            //depending on which card is clicked 
            var itemToPurchase = _context.CollectionItems
                                        .Where(i => i.Id == id)
                                        .FirstOrDefault();


            var options = new SessionCreateOptions
            {
                SuccessUrl = domain + $"Checkout/OrderConfirmation",
                CancelUrl = domain + $"Checkout/Login",
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = itemToPurchase.Price,
                            Currency = "USD",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = itemToPurchase.Name.ToString()
                            }
                        },
                        Quantity = 1
                    }
                },
                Mode = "payment",

            };


            /*             foreach (var item in productList)
                        {
                            var sessionListItem = new SessionLineItemOptions
                            {
                                PriceData = new SessionLineItemPriceDataOptions
                                {
                                    UnitAmount = (item.Price),
                                    Currency = "USD",
                                    ProductData = new SessionLineItemPriceDataProductDataOptions
                                    {
                                        Name = item.Name.ToString(),
                                    }
                                },
                                Quantity = 1
                            };
                            options.LineItems.Add(sessionListItem);
                        } */

            var service = new SessionService();
            Session session = service.Create(options);

            TempData["Session"] = session.Id;

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);

        }
    }


}