Feature: AddDoorsToI24

A short summary of the feature

@regression
Scenario: AddDoorsToI24
	Given The website <Website> is started
	When Login to <Website> as <Email>, <Password>

Examples:
	| Website   | Email                    | Password     |
	| FcosAzure | testim.i24zor@iron24.com | FranchiCzar! |
