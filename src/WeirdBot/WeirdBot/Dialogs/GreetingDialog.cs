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
            await Respond(context);
        }

        private async Task Respond(IDialogContext context)
        {
            var userName = string.Empty;
            context.UserData.TryGetValue<string>("Name", out userName);

            if (string.IsNullOrEmpty(userName))
            {
                await context.PostAsync("Hi! Welcome to the Austin Weird Bot!");
                await context.PostAsync("I am a professional consultant available to answer your questions on 'what do I need to build a Do It Yourself(DIY) project'.");
                await context.PostAsync("First, what is your name?");
                context.UserData.SetValue<bool>("GetName", true);
                context.Wait(MessageReceivedAsync);
            }
            else
            {
                await context.PostAsync(String.Format($"Hi {userName}. How can I help you today?"));
                context.Wait(MessageReceivedCompletedAsync);
            }
        }

        public virtual async Task MessageReceivedCompletedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;
            context.Done(message);
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

            await Respond(context);
            context.Done(message);
        }
    }
}
