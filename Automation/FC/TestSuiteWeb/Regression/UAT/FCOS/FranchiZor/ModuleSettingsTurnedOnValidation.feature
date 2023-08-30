Feature: ModuleSettingsTurnedOnValidation

A short summary of the feature

@regression
Scenario: ModuleSettingsTurnedOnValidation
	Given The website <Website> is started
	When Login to <Website> as <Email>, <Password>

Examples:
	| Website   | Email                      | Password     |
	| FcosAzure | testim.zor@franchiczar.com | FranchiCzar! |
