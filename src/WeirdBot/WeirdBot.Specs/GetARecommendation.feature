Feature: Get A Recommendation
As a WeirdBot User
I want to talk with the bot
To get a recommended build list for my ideal computer

Scenario: Get a recommendation for a computer
	Given I have entered a price cap of $800.00
	When I select a general use computer
	Then the bot requests and displays a recommended build
		And the bot allows me to accept the build

Scenario: Accept Build
	Given I have received a recommended build
		And the bot allows me to accept the build
	When I accept the build
	Then the bot requests my email to send the recommended build
	When I enter my email
	Then the bot sends my recommendation to the entered email address

Scenario: Enter restrictions that prevent a valid recommendation
	Given I have entered a price cap of $50
		When I select a gaming computer
	Then the bot requests a recommendation and displays the null recommendation
		And the bot allows me to raise my price cap


Scenario: Modify recommended build
	Given I have received a recommended build
		And the bot allows me to modify the build
	When I say 'yes'
	Then the bot allows me to select which component type to modify
	When I select a component type
	Then the bot displays a list of components that meet my usage criteria
	When I select a component
	Then the bot updates the recommendation 
		And redisplays the recommendation
		And allows me to accept or modify the build
		 