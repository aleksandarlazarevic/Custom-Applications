Feature: LeadConversionRegression

A short summary of the feature

@regression
Scenario: LeadConversionRegression
	Given The website <Website> is started
	When Login to <Website> as <Email>, <Password>

Examples:
	| Website   | Email                       | Password     |
	| FcosAzure | testim.user@franchiczar.com | FranchiCzar! |
