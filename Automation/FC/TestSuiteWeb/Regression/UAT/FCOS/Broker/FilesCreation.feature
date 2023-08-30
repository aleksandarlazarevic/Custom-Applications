Feature: FilesCreation

A short summary of the feature

#@tag1
#Scenario: [scenario name]
#	Given [context]
#	When [action]
#	Then [outcome]
@regression
Scenario: FilesCreation
	Given The website <Website> is started
	When Login to <Website> as <Email>, <Password>

Examples:
	| Website   | Email                         | Password     |
	| FcosAzure | broker.testim@franchiczar.com | FranchiCzar! |
