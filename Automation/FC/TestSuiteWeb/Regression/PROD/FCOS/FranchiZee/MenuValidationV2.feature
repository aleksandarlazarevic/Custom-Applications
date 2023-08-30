Feature: MenuValidationV2

A short summary of the feature

@regression
Scenario: [scenario name]
	Given The website <Website> is started
	When Login to <Website> as <Email>, <Password>



	And <Website> start page appears
	Then Log out of <Website>
Examples:
	| Website | Email                      | Password     |
	| Fcos    | testim.zee@franchiczar.com | FranchiCzar! |
