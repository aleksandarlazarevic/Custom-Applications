Feature: WorkflowCompanyDropdownSearching

A short summary of the feature

@regression
Scenario: Select first item from the Workflow company dropdown menu
	Given The website <Website> is started
	When Login to <Website> as <Email>, <Password>
	Then Go to Automation Workflows
		And Click Create
	Then Select first option from Workflow company dropdown
Examples:
	| example description  | Website   | Email                         | Password     |
	| Select first company | FcosAzure | broker.testim@franchiczar.com | FranchiCzar! |

@regression
Scenario: Select specific Workflow company
	Given The website <Website> is started
	When Login to <Website> as <Email>, <Password>
	Then Go to Automation Workflows
		And Click Create
	Then Select <Company> as a workflow company
Examples:
	| example description     | Website   | Email                         | Password     | Company          |
	| FranchiCzar search      | FcosAzure | broker.testim@franchiczar.com | FranchiCzar! | FranchiCzar      |
	| Affinity Esports search | FcosAzure | broker.testim@franchiczar.com | FranchiCzar! | Affinity Esports |

