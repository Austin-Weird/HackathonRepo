//using System.Net;
//using System.Net.Http;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using System.Web.Http;
//using Microsoft.Bot.Builder.Dialogs;
//using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Bot.Api.ServiceModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Net;

namespace Bot.Api.Controllers
{
    /// <summary>
    /// This controller will receive the messages and handle them to the service. 
    /// </summary>
    [Route("api/[controller]")]
    public class MessagesController : Controller
    {
        /// <summary>
        /// memoryCache
        /// </summary>
        IMemoryCache memoryCache;

        /// <summary>
        /// Bot Credentials
        /// </summary>
        BotCredentials botCredentials;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="memoryCache">memory cache service</param>
        /// <param name="botCredentials">bot credentials</param>
        public MessagesController(IMemoryCache memoryCache, IOptions<BotCredentials> botCredentials)
        {
            this.memoryCache = memoryCache;
            this.botCredentials = botCredentials.Value;
        }

        /// <summary>
        /// This method will be called every time the bot receives an activity. This is the messaging endpoint
        /// </summary>
        /// <param name="activity">The activity sent to the bot. I'm using dynamic here to simplify the code for the post</param>
        /// <returns>201 Created</returns>
        //[HttpPost]
        public virtual async Task<IActionResult> Post([FromBody] dynamic activity)
        {
            // Get the conversation id so the bot answers.
            var conversationId = activity.from.id.ToString();

            // Get a valid token 
            string token = await this.GetBotApiToken();

            // send the message back
            using (var client = new HttpClient())
            {
                // I'm using dynamic here to make the code simpler
                dynamic message = new ExpandoObject();
                message.type = "message/text";
                message.text = activity.text;

                // Set the toekn in the authorization header.
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                // Post the message
                //await client.PostAsJsonAsync<ExpandoObject>(
                //    $"https://api.skype.net/v3/conversations/{conversationId}/activities",
                //    message as ExpandoObject);

                await HandleActivityMessage(activity);

                //if (activity.Type == ActivityTypes.Message)
                //{
                //    await Conversation.SendAsync(activity, () => new Dialogs.RootDialog());
                //}
                //else
                //{
                //    HandleSystemMessage(activity);
                //}
            }

            return Created(Url.Content("~/"), string.Empty);
        }

        /// <summary>
        /// Gets and caches a valid token so the bot can send messages.
        /// </summary>
        /// <returns>The token</returns>
        private async Task<string> GetBotApiToken()
        {
            // Check to see if we already have a valid token
            string token = memoryCache.Get("token")?.ToString();
            if (string.IsNullOrEmpty(token))
            {
                // we need to get a token.
                using (var client = new HttpClient())
                {
                    // Create the encoded content needed to get a token
                    var parameters = new Dictionary<string, string>
                    {
                        {"client_id", this.botCredentials.ClientId },
                        {"client_secret", this.botCredentials.ClientSecret },
                        {"scope", "https://graph.microsoft.com/.default" },
                        {"grant_type", "client_credentials" }
                    };
                    var content = new FormUrlEncodedContent(parameters);

                    // Post
                    var response = await client.PostAsync("https://login.microsoftonline.com/common/oauth2/v2.0/token", content);

                    // Get the token response
                    var tokenResponse = await response.Content.ReadAsAsync<TokenResponse>();

                    token = tokenResponse.access_token;

                    // Cache the token for 15 minutes.
                    memoryCache.Set(
                        "token",
                        token,
                        new DateTimeOffset(DateTime.Now.AddMinutes(15)));
                }
            }

            return token;
        }

        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        //[HttpPost]
        //public virtual async Task<IActionResult> Post([FromBody] dynamic activity)
        //public async Task<IActionResult> Post([FromBody]Activity activity)
        private async Task<IActionResult> HandleActivityMessage(Activity activity)
        {
            //// Get a valid token 
            //string token = await this.GetBotApiToken();
            //
            //// Set the token in the authorization header.
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


            if (activity.Type == ActivityTypes.Message)
            {
                await Conversation.SendAsync(activity, () => new Dialogs.RootDialog());
            }
            else
            {
                HandleSystemMessage(activity);
            }
            //var response = Request.CreateResponse(HttpStatusCode.OK);
            return Ok();
            //return Created(Url.Content("~/"), string.Empty);
        }

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }
    }
}