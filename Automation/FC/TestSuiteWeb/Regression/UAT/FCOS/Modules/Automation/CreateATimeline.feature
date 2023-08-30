Feature: CreateATimeline

A short summary of the feature

@regression
Scenario: Create new timeline
	Given The website <Website> is started
	When Login to <Website> as <Email>, <Password>
	Then Go to Automation Timelines
	#	And Create a timeline assigned to <TypeOfCompany>
	#Then Delete created timeline
Examples:
	| Website   | Email                         | Password     | TypeOfCompany |
	| FcosAzure | broker.testim@franchiczar.com | FranchiCzar! | Broker        |
	| FcosAzure | broker.testim@franchiczar.com | FranchiCzar! | Franchisor    |
	| FcosAzure | broker.testim@franchiczar.com | FranchiCzar! | Franchisee    |
	| FcosAzure | broker.testim@franchiczar.com | FranchiCzar! | Facility      |
	| FcosAzure | broker.testim@franchiczar.com | FranchiCzar! | Customer      |
	| FcosAzure | broker.testim@franchiczar.com | FranchiCzar! | Member        |