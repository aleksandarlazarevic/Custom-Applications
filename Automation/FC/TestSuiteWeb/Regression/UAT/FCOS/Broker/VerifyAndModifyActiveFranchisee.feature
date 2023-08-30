Feature: VerifyAndModifyActiveFranchisee

A short summary of the feature

@regression
Scenario: VerifyAndModifyActiveFranchisee
	Given The website <Website> is started
	When Login to <Website> as <Email>, <Password>

Examples:
	| Website   | Email                         | Password     |
	| FcosAzure | broker.testim@franchiczar.com | FranchiCzar! |
