Feature: AllianceCreateLead

A short summary of the feature

@regression
Scenario: AllianceCreateLead
	Given The website <Website> is started
	When Login to <Website> as <Email>, <Password>

Examples:
	| Website   | Email                         | Password     |
	| FcosAzure | broker.testim@franchiczar.com | FranchiCzar! |
