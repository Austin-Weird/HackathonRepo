using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Connector;
using WeirdBot.Forms;

namespace WeirdBot.Dialogs
{
    [Serializable]
    public class RootDialog //: IDialog<object>
    {
        //public Task StartAsync(IDialogContext context)
        //{
        //    context.Wait(MessageReceivedAsync);

        //    return Task.CompletedTask;
        //}

        //private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        //{
        //    var activity = await result as Activity;

        //    // calculate something for us to return
        //    int length = (activity.Text ?? string.Empty).Length;

        //    // return our reply to the user
        //    await context.PostAsync($"You sent {activity.Text} which was {length} characters");

        //    context.Wait(MessageReceivedAsync);
        //}

        public static readonly IDialog<string> dialog = Chain.PostToChain()
            .Select(msg => msg.Text)
            .Switch(
                new RegexCase<IDialog<string>>(new Regex("^hi", RegexOptions.IgnoreCase), (context, text) =>
                {
                    return Chain.ContinueWith(new GreetingDialog(), AfterGreetingContinuation);
                }),
                new DefaultCase<string, IDialog<string>>((context, text) =>
                {
                    return Chain.ContinueWith(FormDialog.FromForm(BuildComputerForm.BuildForm, FormOptions.PromptInStart), AfterGreetingContinuation);
                })

            )
            .Unwrap()
            .PostToUser()
            ;

        private async static Task<IDialog<string>> AfterGreetingContinuation(IBotContext context, IAwaitable<object> item)
        {
            var token = await item;
            var name = "User";
            context.UserData.TryGetValue<string>("Name", out name);
            return Chain.Return($"Thank you for using the Austin Weird bot, {name}!");
        }
    }
}