using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace WeirdBot.Dialogs
{
    [Serializable]
    public class GreetingDialog : IDialog
    {
        public async Task StartAsync(IDialogContext context)
        {
            var message = context.MakeMessage();
            await Respond(context, message);
        }

        private async Task Respond(IDialogContext context, IMessageActivity result)
        {
            var userName = string.Empty;
            context.UserData.TryGetValue<string>("Name", out userName);

            if (string.IsNullOrEmpty(userName))
            {
                await context.PostAsync("Hello!  What is your name?");
                context.UserData.SetValue<bool>("GetName", true);
                context.Wait(MessageReceivedAsync);
            }
            else
            {
                await context.PostAsync($"Hi {userName}. How can I help you today?");
                context.Done(result);
            }
        }

        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var userName = string.Empty;
            var message = await result;
            var getName = false;

            context.UserData.TryGetValue<string>("Name", out userName);
            context.UserData.TryGetValue<bool>("GetName", out getName);

            if (getName)
            {
                userName = message.Text;
                context.UserData.SetValue<string>("Name", userName);
                context.UserData.SetValue<bool>("GetName", false);
            }

            await Respond(context, message);
        }
    }
}
