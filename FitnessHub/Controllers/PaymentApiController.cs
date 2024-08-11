using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using PayPal.Api;
using PayPalPayment = PayPal.Api.Payment;
using FitnessHubPayment = FitnessHub.Models.Payment;
using FitnessHub.Models;


namespace FitnessHub.Controllers.Api
{
    [Authorize]
    public class PaymentApiController : ApiController
    {
        private const string ClientId = "Your-Client-ID";
        private const string ClientSecret = "Your-Client-Secret";

        [HttpPost]
        public async Task<IHttpActionResult> CreatePayment(BookingDto bookingDto)
        {
            var apiContext = GetApiContext();

            var payment = CreatePayment(apiContext, bookingDto);
            var redirectUrl = payment.links.FirstOrDefault(link => link.rel.Equals("approval_url", System.StringComparison.OrdinalIgnoreCase))?.href;

            return Ok(new { redirectUrl });
        }

        [HttpPost]
        public async Task<IHttpActionResult> ExecutePayment(string paymentId, string PayerID)
        {
            var apiContext = GetApiContext();

            var paymentExecution = new PaymentExecution
            {
                payer_id = PayerID
            };

            var payment = new PayPalPayment { id = paymentId }; // Use PayPalPayment alias
            var executedPayment = payment.Execute(apiContext, paymentExecution);

            if (executedPayment.state.ToLower() != "approved")
            {
                return BadRequest("Payment not approved");
            }

            // Save payment details in the database, e.g., create a Payment record
            // SavePaymentDetails(paymentId, PayerID);

            return Ok();
        }

        private APIContext GetApiContext()
        {
            var config = new Dictionary<string, string>
            {
                { "mode", "sandbox" },
                { "clientId", ClientId },
                { "clientSecret", ClientSecret }
            };

            var accessToken = new OAuthTokenCredential(ClientId, ClientSecret, config).GetAccessToken();
            return new APIContext(accessToken);
        }

        private PayPalPayment CreatePayment(APIContext apiContext, BookingDto bookingDto)
        {
            var redirectUrl = Url.Link("DefaultApi", new { action = "ExecutePayment" });
            var payment = new PayPalPayment
            {
                intent = "sale",
                payer = new Payer { payment_method = "paypal" },
                transactions = new List<Transaction>
                {
                    new Transaction
                    {
                        description = "Booking Payment",
                        invoice_number = bookingDto.BookingID.ToString(),
                        amount = new Amount
                        {
                            currency = "USD",
                            total = bookingDto.AmountPaid.ToString("0.00") // Use AmountPaid
                        }
                    }
                },
                redirect_urls = new RedirectUrls
                {
                    cancel_url = redirectUrl,
                    return_url = redirectUrl
                }
            };

            return payment.Create(apiContext);
        }
    }
}
