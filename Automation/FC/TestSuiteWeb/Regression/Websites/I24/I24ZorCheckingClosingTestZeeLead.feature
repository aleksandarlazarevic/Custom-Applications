Feature: I24ZorCheckingClosingTestZeeLead

A short summary of the feature

@regression
Scenario: I24ZorCheckingClosingTestZeeLead
	Given The website <Website> is started
	When Login to <Website> as <Email>, <Password>


Examples:
	| Website | Email                    | Password     |
	| Fcos    | testim.i24zor@iron24.com | FranchiCzar! |