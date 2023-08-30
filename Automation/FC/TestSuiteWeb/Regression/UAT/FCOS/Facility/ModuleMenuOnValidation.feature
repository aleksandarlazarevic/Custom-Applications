Feature: ModuleMenuOnValidation

A short summary of the feature

@regression
Scenario: ModuleMenuOnValidation
	Given The website <Website> is started
	When Login to <Website> as <Email>, <Password>

Examples:
	| Website   | Email                       | Password     |
	| FcosAzure | testim.user@franchiczar.com | FranchiCzar! |
