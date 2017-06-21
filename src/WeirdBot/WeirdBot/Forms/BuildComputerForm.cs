using System;
using Microsoft.Bot.Builder.FormFlow;
using WeirdBot.Models;

namespace WeirdBot.Forms
{
    [Serializable]
    public class BuildComputerForm
    {
        public string PriceRange;
        public Category? Category;


        public static IForm<BuildComputerForm> BuildForm()
        {
            return new FormBuilder<BuildComputerForm>()
                .Message("Welcome to the computer builder bot!")
                .Build();
        }
    }
}
