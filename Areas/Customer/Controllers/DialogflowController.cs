using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace TechLife.Areas.Customer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DialogflowController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Webhook([FromBody] JObject request)
        {
            var queryResult = request["queryResult"];
            var intentName = queryResult["intent"]["displayName"].ToString();
            var parameters = queryResult["parameters"];

            // Process the request and generate a response
            var responseText = "";

            switch (intentName)
            {
                case "YourIntentName":
                    responseText = "This is a response from your webhook.";
                    break;
                // Add cases for other intents
                default:
                    responseText = "I'm not sure how to respond to that.";
                    break;
            }

            var responseJson = new
            {
                fulfillmentText = responseText
            };

            return Ok(responseJson);
        }
    }
}
