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
            else if (activity.Type == ActivityTypes.ConversationUpdate && activity.MembersAdded.Count > 0 && !activity.MembersAdded.Any(m => m.Name.ToLower() == "bot"))
            {
                // Attempt to start the conversation here?
                var userAccount = activity.From;
                var botAccount = activity.Recipient;
                var connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                var conversationId = await connector.Conversations.CreateDirectConversationAsync(botAccount, userAccount);

                var message = activity.CreateReply();
                message.Text = "Hi! &nbsp;&nbsp;Welcome to the Austin Weird Bot!" +
                    "  \r\nI am a professional consultant available to answer your questions on 'What do I need to build a Do It Yourself(DIY) project'.";
                await connector.Conversations.SendToConversationWithHttpMessagesAsync(message,conversationId.Id);


                // #1st way
                //var userAccount = new ChannelAccount(name: "default-user", id: "default-user");
                //var botAccount = new ChannelAccount() { Id = "934493jn5f6f348f", Name = "console-Bot" };
                //var connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                //var conversationId = await connector.Conversations.CreateDirectConversationAsync(botAccount, userAccount);

                //IMessageActivity message = Activity.CreateMessageActivity();
                //message.From = botAccount;
                //message.Recipient = userAccount;
                //message.Conversation = new ConversationAccount(id: conversationId.Id);
                //message.Text = "Hello, Larry!";
                //message.Locale = "en-Us";
                //await connector.Conversations.SendToConversationAsync((Activity)message);


                // #2nd way
                //var userAccount = new ChannelAccount() { Id = "default-user", Name = "user" };
                //var botAccount = new ChannelAccount() { Id = "934493jn5f6f348f", Name = "console-Bot" };
                //string url = "{serviceUrl}";

                //MicrosoftAppCredentials.TrustServiceUrl(url, DateTime.Now.AddDays(7));
                //var account = new MicrosoftAppCredentials("{MicrosoftAppIdKey}", "{MicrosoftAppPasswordKey}");
                //var connector = new ConnectorClient(new Uri(url), account);

                //IMessageActivity message = Activity.CreateMessageActivity();
                //message.From = botAccount;
                //message.Recipient = userAccount;
                //message.Conversation = new ConversationAccount() { Id = "{conversationId}" };
                //message.Text = "Message sent from console application!!!";
                //message.Locale = "en-us";
                //var response = await connector.Conversations.SendToConversationAsync((Activity)message);
                //Console.WriteLine($"response:{response.Id}");
            }
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