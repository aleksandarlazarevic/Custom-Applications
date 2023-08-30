Feature: MenuModuleOnVerification

A short summary of the feature

@regression
Scenario: MenuModuleOnVerification
	Given The website <Website> is started
	When Login to <Website> as <Email>, <Password>

Examples:
	| Website   | Email                      | Password     |
	| FcosAzure | testim.zee@franchiczar.com | FranchiCzar! |
