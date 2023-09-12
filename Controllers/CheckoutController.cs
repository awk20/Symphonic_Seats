/* using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Mvc;
using SymphonicSeats2.Models;
using Stripe.Checkout;

namespace Server.Controllers;

[ApiController]
[Route("[controller]")]
[ApiExplorerSettings(IgnoreApi = true)]
public class CheckoutController : ControllerBase
{
    private readonly IConfiguration _configuration;

    private static string s_wasmClientURL = string.Empty;

    public CheckoutController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost]
    public async Task<ActionResult> CheckoutOrder([FromBody] CollectionItem item, [FromServices] IServiceProvider sp)
    {
        var referer = Request.Headers.Referer;
        s_wasmClientURL = referer[0];

        // Build the URL to which the customer will be redirected after paying.
        var server = sp.GetRequiredService<IServer>();

        var serverAddressesFeature = server.Features.Get<IServerAddressesFeature>();

        string? thisApiUrl = null;

        if (serverAddressesFeature is not null)
        {
            thisApiUrl = serverAddressesFeature.Addresses.FirstOrDefault();
        }

        if (thisApiUrl is not null)
        {
            var sessionId = await CheckOut(item, thisApiUrl);
            var pubKey = _configuration["Stripe:PubKey"];

            var checkoutOrderResponse = new ChcekoutResponseModel()
            {
                SessionId = sessionId,
                PubKey = pubKey
            };

            return Ok(checkoutOrderResponse);
        }
        else
        {
            return StatusCode(500);
        }
    }

    [NonAction]
    public async Task<string> CheckOut(CollectionItem item, string thisApiUrl)
    {
        // Create a payment flow from the items in the cart.
        // Gets sent to Stripe API.
        var options = new SessionCreateOptions
        {
            // Stripe calls the URLs below when certain checkout events happen such as success and failure.
            SuccessUrl = $"{thisApiUrl}/checkout/success?sessionId=" + "{CHECKOUT_SESSION_ID}", // Customer paid.
            CancelUrl = s_wasmClientURL + "failed",  // Checkout cancelled.
            PaymentMethodTypes = new List<string> // Only card available in test mode?
            {
                "card"
            },
            LineItems = new List<SessionLineItemOptions>
            {
                new()
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = item.Price, // Price is in USD cents.
                        Currency = "USD",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Name,
                            Description = item.Description,
                            Images = new List<string> { item.ImageURL }
                        },
                    },
                    Quantity = 1,
                },
            },
            Mode = "payment" // One-time payment. Stripe supports recurring 'subscription' payments.
        };

        var service = new SessionService();
        var session = await service.CreateAsync(options);

        return session.Id;
    }

    [HttpGet("success")]
    // Automatic query parameter handling from ASP.NET.
    // Example URL: https://localhost:7051/checkout/success?sessionId=si_123123123123
    public ActionResult CheckoutSuccess(string sessionId)
    {
        var sessionService = new SessionService();
        var session = sessionService.Get(sessionId);

        // Here you can save order and customer details to your database.
        var total = session.AmountTotal.Value;
        var customerEmail = session.CustomerDetails.Email;

        return Redirect(s_wasmClientURL + "success");
    }
} */

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
//using Stripe.BillingPortal;
using SymphonicSeats2.Models;
using Stripe.Checkout;
using Stripe;
using System.Reflection.Metadata;

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
        public IActionResult OrderConfirmation()
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

        public IActionResult Checkout()
        {
            List<CollectionItem> productList = new List<CollectionItem>()
                    {
                        new CollectionItem
                        {
                            Id = 2,
                            Name = "Weezer",
                            Description = "Weezer is back on tour performing their latest album SZNZ.",
                            ConcertTime = new DateTime(2023, 10, 15),
                            ImageURL = "https://s3.amazonaws.com/heights-photos/wp-content/uploads/2019/03/03130519/Weezer.jpg",
                            Location = "Austin, Texas",
                            Price = 200,
                            NumTickets = 400
                        },
                        new CollectionItem
                        {
                            Id = 3,
                            Name = "The Cure",
                            Description = "Kings of the alternative genre, The Cure are back on tour for the first time in years.",
                            ConcertTime = new DateTime(2023, 09, 21),
                            ImageURL = "https://blog.roughtrade.com/content/images/size/w1000/2022/02/Screen-Shot-2022-02-14-at-9.21.39-AM.png",
                            Location = "Miami, Florida",
                            Price = 175,
                            NumTickets = 300
                        }
                    };

            var domain = "http://localhost:5270/";

            var options = new SessionCreateOptions
            {
                SuccessUrl = domain + $"Checkout/OrderConfirmation",
                CancelUrl = domain + $"Checkout/Login",
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",

            };


            foreach (var item in productList)
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
            }

            var service = new SessionService();
            Session session = service.Create(options);

            TempData["Session"] = session.Id;

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);

        }
    }


}