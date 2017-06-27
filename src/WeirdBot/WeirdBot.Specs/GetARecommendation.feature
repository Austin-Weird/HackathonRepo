Feature: Get A Recommendation
As a WeirdBot User
I want to talk with the bot
To get a recommended build list for my ideal computer

Scenario: Get a recommendation for a general computer
	Given I have entered a price range between $200 and $600
	When I select a general use computer
	Then the bot requests and displays a recommended build
		And the bot allows me to accept the build
	When I accept the build
	Then the bot requests my email to send the recommended build
	When I enter my email
	Then the bot sends my recommendation to the entered email address


Scenario: Get a recommendation for a gaming computer

Scenario: Enter restrictions that prevent a valid recommendation

Scenario: Modify recommended build