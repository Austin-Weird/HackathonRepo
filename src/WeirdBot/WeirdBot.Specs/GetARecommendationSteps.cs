using System;
using System.Net.Http;
using TechTalk.SpecFlow;
using WeirdBot.Models;

namespace WeirdBot.Utilities.Specs
{
    [Binding]
    public class GetARecommendationSteps
    {
        private decimal priceCap;
        Usage[] usage;

        public GetARecommendationSteps()
        {
            ScenarioContext.Current.Add("botClient", new HttpClient());
            ScenarioContext.Current.Add("apiClient", new HttpClient());
        }

        [Given(@"I have entered a price cap of \$(.*)")]
        public void GivenIHaveEnteredAPriceCapOf(Decimal userPrice)
        {
            priceCap = userPrice;
        }
        
        [Given(@"I have received a recommended build")]
        public void GivenIHaveReceivedARecommendedBuild()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"the bot allows me to accept the build")]
        public void GivenTheBotAllowsMeToAcceptTheBuild()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"the bot allows me to modify the build")]
        public void GivenTheBotAllowsMeToModifyTheBuild()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I select a general use computer")]
        public void WhenISelectAGeneralUseComputer()
        {
             usage = new Usage[] { Usage.General };
        }
        
        [When(@"I accept the build")]
        public void WhenIAcceptTheBuild()
        {
            //TODO: Pass "yes" to bot's "Accept Build" prompt
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I enter my email")]
        public void WhenIEnterMyEmail()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I select a gaming computer")]
        public void WhenISelectAGamingComputer()
        {
            usage = new Usage[] { Usage.Gaming };
        }
        
        [When(@"I say '(.*)'")]
        public void WhenISay(string p0)
        {
            //TODO: send 'yes' to bot
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I select a component type")]
        public void WhenISelectAComponentType()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I select a component")]
        public void WhenISelectAComponent()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"the bot requests and displays a recommended build")]
        public void ThenTheBotRequestsAndDisplaysARecommendedBuild()
        {
            var apiClient = ScenarioContext.Current["apiClient"] as HttpClient;
           // apiClient.PostAsync()
        }
        
        [Then(@"the bot allows me to accept the build")]
        public void ThenTheBotAllowsMeToAcceptTheBuild()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"the bot requests my email to send the recommended build")]
        public void ThenTheBotRequestsMyEmailToSendTheRecommendedBuild()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"the bot sends my recommendation to the entered email address")]
        public void ThenTheBotSendsMyRecommendationToTheEnteredEmailAddress()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"the bot requests a recommendation and displays the null recommendation")]
        public void ThenTheBotRequestsARecommendationAndDisplaysTheNullRecommendation()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"the bot allows me to raise my price cap")]
        public void ThenTheBotAllowsMeToRaiseMyPriceCap()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"the bot allows me to select which component type to modify")]
        public void ThenTheBotAllowsMeToSelectWhichComponentTypeToModify()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"the bot displays a list of components that meet my usage criteria")]
        public void ThenTheBotDisplaysAListOfComponentsThatMeetMyUsageCriteria()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"the bot updates the recommendation")]
        public void ThenTheBotUpdatesTheRecommendation()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"redisplays the recommendation")]
        public void ThenRedisplaysTheRecommendation()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"allows me to accept or modify the build")]
        public void ThenAllowsMeToAcceptOrModifyTheBuild()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
