using PayPal;
using PayPal.Api;
using System;
using System.Collections.Generic;
using TarotBooking.Repositories.Interfaces;

namespace Service.Services
{
    public class PaymentService
    {
        private readonly IBookingRepo _bookingRepo;

        public PaymentService(IBookingRepo bookingRepo)
        {
            _bookingRepo = bookingRepo;
        }

        private APIContext GetAPIContext()
        {
            var clientId = "AVxahSSl4ARDEASlB4bb-DDlwidxh7PHKCytZI-N-1TEoOrHt6iLYFBuYs8V4aZOPW_YSmDbbr1t2WYf"; 
            var clientSecret = "EFedTfMAt61pQ7JWAArZEHU3iMg2BY3D4sRrMIX9Jezl16g8mkk2XwZg0HUzWoVbumrPlv0gy85n7i6o";  
            var accessToken = new OAuthTokenCredential(clientId, clientSecret).GetAccessToken();
            return new APIContext(accessToken) { Config = new Dictionary<string, string> { { "mode", "sandbox" } } };
        }

        public Payment CreatePayment(float total, string returnUrl, string cancelUrl)
        {
            var apiContext = GetAPIContext();
            decimal newTotal = Math.Round((decimal)total / 24850, 2);
            Console.WriteLine($"Converted total: {newTotal}");

            var transactionList = new List<Transaction>
            {
                new Transaction
                {
                    description = "Payment for booking",
                    invoice_number = new Random().Next(100000).ToString(),  
                    amount = new Amount
                    {
                        currency = "USD",  
                        total = newTotal.ToString("F2")  
                    }
                }
            };

            var payment = new Payment
            {
                intent = "sale",  
                payer = new Payer { payment_method = "paypal" },
                transactions = transactionList,  
                redirect_urls = new RedirectUrls
                {
                    cancel_url = cancelUrl,
                    return_url = returnUrl
                },
            };

            try
            {
                var createdPayment = payment.Create(apiContext);

                var approvalUrl = createdPayment.links
                    .FirstOrDefault(link => link.rel == "approval_url")?.href;

                return createdPayment;  
            }

            catch (PayPalException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw new Exception("Error while creating PayPal payment", ex);
            }
        }
    }
}
