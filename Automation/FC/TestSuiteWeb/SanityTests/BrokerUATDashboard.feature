Feature: BrokerUATDashboard

A short summary of the feature

@sanity
Scenario: BrokerUATDashboard2
	Given The website <Website> is started
	When Login to <Website> as <Email>, <Password>
	And <Website> start page appears
	Then Log out of <Website>
Examples:
	| Website | Email                         | Password     |
	| Fcos    | broker.testim@franchiczar.com | FranchiCzar! |
