using Stripe;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Stripe.Checkout;

namespace TechLife.Utility
{
    public class StripeService
    {
        private readonly IConfiguration _configuration;

        public StripeService(IConfiguration configuration)
        {
            _configuration = configuration;
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
        }

        public async Task<string> CreateCheckoutSessionAsync(string email, string name, string address, List<SessionLineItemOptions> items)
        {
            var options = new Stripe.Checkout.SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = items,
                Mode = "payment",
                SuccessUrl = "https://localhost:7293/Customer/ShoppingCart/OrderSuccess?session_id={CHECKOUT_SESSION_ID}",
                CancelUrl = "https://localhost:7293/Customer/ShoppingCart/OrderFailure",
                CustomerEmail = email,
                Metadata = new Dictionary<string, string>
        {
            { "CustomerName", name },
            { "Address", address }
        }
            };

            var service = new Stripe.Checkout.SessionService();
            var session = await service.CreateAsync(options);
            return session.Url; // Return the session URL directly
        }
        public async Task<Session> CreateCheckoutSession(double amount, string currency, int simServiceId)
        {
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
        {
            new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmount = (long)(amount * 100), // Stripe expects amount in cents
                    Currency = currency,
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = "Sim Service Request"
                    },
                },
                Quantity = 1,
            },
        },
                Mode = "payment",
                SuccessUrl = $"https://localhost:7293/Admin/SimServicesToDo/PaymentSuccess?session_id={{CHECKOUT_SESSION_ID}}&sim_service_id={simServiceId}",
                CancelUrl = "https://localhost:7293/Admin/SimServicesToDo/PaymentFailed",
                Metadata = new Dictionary<string, string>
        {
            { "SimServiceId", simServiceId.ToString() }
        }
            };

            var service = new SessionService();
            Session session = await service.CreateAsync(options);
            return session;
        }



    }
}
