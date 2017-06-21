using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using WeirdBot.Forms;

namespace WeirdBot.Dialogs
{
    [LuisModel("d7949cc6-b70b-4457-8f87-4d43838fc9e2", "080c2b042a8e48f0a5fb5e2ef2c71778")]
    [Serializable]
    public class LuisDialog : LuisDialog<BuildComputerForm>
    {
        private readonly BuildFormDelegate<BuildComputerForm> ComputerBuilder;

        public LuisDialog(BuildFormDelegate<BuildComputerForm> buildComputerForm)
        {
            this.ComputerBuilder = buildComputerForm;
        }

        private async Task Callback(IDialogContext context, IAwaitable<object> result)
        {
            var token = await result;
            var name = "User";
            context.UserData.TryGetValue<string>("Name", out name);
            //await context.PostAsync($"Great we got that all set up for you, {name}!");
            await context.PostAsync($"Thank you for using the Austin Weird Bot, {name}!");

            context.Wait(MessageReceived);
        }

        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("I'm sorry.  I don't know what you mean.");
            context.Wait(MessageReceived);
        }

        [LuisIntent("Greeting")]
        public async Task Greeting(IDialogContext context, LuisResult result)
        {
            context.Call(new GreetingDialog(), Callback);
        }

        [LuisIntent("BuildComputer")]
        public async Task BuildComputer(IDialogContext context, LuisResult result)
        {
            var buildComputerForm = new FormDialog<BuildComputerForm>(new BuildComputerForm(), this.ComputerBuilder, FormOptions.PromptInStart);
            context.Call<BuildComputerForm>(buildComputerForm, Callback);
        }

        [LuisIntent("QueryInformation")]
        public async Task QueryInformation(IDialogContext context, LuisResult result)
        {
            foreach(var entity in result.Entities.Where(Entity => Entity.Type == "Query"))
            {
                var value = entity.Entity.ToLower();
                if (value == "video card")
                {
                    await context.PostAsync($"Yes we have a {value}!");
                    context.Wait(MessageReceived);
                    return;
                }
                else
                {
                    await context.PostAsync("I'm sorry we don't have that.");
                    context.Wait(MessageReceived);
                    return;
                }
            }
            await context.PostAsync("I'm sorry we don't have that.");
            context.Wait(MessageReceived);
            return;
        }
    }
}
