﻿using System;
using System.Collections.Generic;
using Microsoft.Bot.Builder.FormFlow;
using WeirdBot.Models;

namespace WeirdBot.Forms
{
    [Serializable]
    public class BuildComputerForm
    {
        public float PriceLimit;
        public List<Usage> Category;


        public static IForm<BuildComputerForm> BuildForm()
        {
            return new FormBuilder<BuildComputerForm>()
                .Message("Welcome to the Computer Builder!  \r\n\r\n Type **Help** at any time to see your options.")
                .Build();
        }
    }
}
