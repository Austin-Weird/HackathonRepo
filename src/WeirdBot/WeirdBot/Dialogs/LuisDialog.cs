﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using WeirdBot.Forms;

namespace WeirdBot.Dialogs
{
    [LuisModel("d7949cc6-b70b-4457-8f87-4d43838fc9e2", "c72b2b2073164d4c84593bc3fd9f6e98")]
    [Serializable]
    public class LuisDialog : LuisDialog<BuildComputerForm>
    {
        private readonly BuildFormDelegate<BuildComputerForm> ComputerBuilder;

        public LuisDialog(BuildFormDelegate<BuildComputerForm> buildComputerForm)
        {
            this.ComputerBuilder = buildComputerForm;
        }

        private async Task GreetingCallback(IDialogContext context, IAwaitable<object> result)
        {
            var token = await result;
            var name = "User";
            var buildOptions = new List<string> { "  - I would like to build a computer" };
            context.UserData.TryGetValue<string>("Name", out name);
            await context.PostAsync("You can ask about the following build options:");
            buildOptions.ForEach(async option => await context.PostAsync(option));

            context.Wait(MessageReceived);
        }
        private async Task BuildComputerCallback(IDialogContext context, IAwaitable<object> result)
        {
            var token = await result;
            var name = "User";
            context.UserData.TryGetValue<string>("Name", out name);
            await context.PostAsync("Great we got that all set up for you!");
            await context.PostAsync(string.Format("Thank you for using the Austin Weird Bot, {0}!", name));

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
            context.Call(new GreetingDialog(), GreetingCallback);
        }

        [LuisIntent("BuildComputer")]
        public async Task BuildComputer(IDialogContext context, LuisResult result)
        {
            var buildComputerForm = new FormDialog<BuildComputerForm>(new BuildComputerForm(), this.ComputerBuilder, FormOptions.PromptInStart);
            context.Call<BuildComputerForm>(buildComputerForm, BuildComputerCallback);
        }

        [LuisIntent("QueryInformation")]
        public async Task QueryInformation(IDialogContext context, LuisResult result)
        {
            foreach (var entity in result.Entities.Where(Entity => Entity.Type == "Query"))
            {
                var value = entity.Entity.ToLower();
                if (value == "video card")
                {
                    await context.PostAsync(string.Format("Yes we have a {0}!",value));
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
