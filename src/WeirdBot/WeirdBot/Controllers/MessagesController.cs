using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using WeirdBot.Dialogs;
using WeirdBot.Forms;

namespace WeirdBot.Controllers
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                //await Conversation.SendAsync(activity, () => RootDialog.dialog);
                await Conversation.SendAsync(activity, MakeLuisDialog);
            }
            //else if (activity.Type == ActivityTypes.ConversationUpdate && activity.MembersAdded.Count > 0 && !activity.MembersAdded.Any(m => m.Name.ToLower() == "bot"))
            //{
            //    // Attempt to start the conversation here?
            //    //await Conversation.SendAsync(activity, MakeLuisDialog);
                //var userAccount = new ChannelAccount(name: "Larry", id: "@UV357341");
                //var connector = new ConnectorClient(new Uri(actvity.ServiceUrl));
                //var conversationId = await connector.Conversations.CreateDirectConversationAsync(botAccount, userAccount);

                //IMessageActivity message = Activity.CreateMessageActivity();
                //message.From = botAccount;
                //message.Recipient = userAccount;
                //message.Conversation = new ConversationAccount(id: conversationId.Id);
                //message.Text = "Hello, Larry!";
                //message.Locale = "en-Us";
                //await connector.Conversations.SendToConversationAsync((Activity)message); 
            //}
            else
            {
                HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private IDialog<BuildComputerForm> MakeLuisDialog()
        {
            return Chain.From(() => new LuisDialog(BuildComputerForm.BuildForm));
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
            else if (message.Type == ActivityTypes.EndOfConversation)
            {
                // Handle end of conversation
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