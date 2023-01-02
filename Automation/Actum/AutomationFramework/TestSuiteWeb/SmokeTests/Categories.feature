Feature: Categories

Testing purchases of Phone, Laptop, Monitor
@smoke
Scenario: Buy a phone
	Given The website <Website> is started
	When Login to <Website> as <User>, <Password>
		And Navigate to Home Page
	Then Choose <Category>
		And Add to cart a <Model>
Examples:
	| Website   | User      | Password      | Category | Model   |
	| demoblaze | BugsBunny | BugsBunny123! | Phones   | Nexus 6 |

@smoke
Scenario: Buy a laptop
	Given The website <Website> is started
	When Login to <Website> as <User>, <Password>
		And Navigate to Home Page
	Then Choose <Category>
		And Add to cart a <Model>
Examples:
	| Website   | User      | Password      | Category | Model        |
	| demoblaze | BugsBunny | BugsBunny123! | Laptops  | Sony vaio i7 |

@smoke
Scenario: Buy a monitor
	Given The website <Website> is started
	When Login to <Website> as <User>, <Password>
		And Navigate to Home Page
	Then Choose <Category>
		And Add to cart a <Model>
Examples:
	| Website   | User      | Password      | Category | Model        |
	| demoblaze | BugsBunny | BugsBunny123! | Monitors | ASUS Full HD |
