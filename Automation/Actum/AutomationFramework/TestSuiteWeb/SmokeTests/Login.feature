Feature: Login

Basic Login tests

@smoke
Scenario: Log in with valid credentials
	Given The website <Website> is started
	When Login to <Website> as <User>, <Password>
Examples:
	| Website   | User     | Password      |
	| demoblaze | BugsBunny | BugsBunny123! |

@smoke
Scenario: Log in with non-existant user
	Given The website <Website> is started
	When Login to <Website> with non-existing <User>, <Password>
Examples:
	| Website   | User     | Password      |
	| demoblaze | LolaBunny | LolaBunny123! |

@smoke
Scenario: Log in with invalid credentials
	Given The website <Website> is started
	When Login to <Website> with wrong credentials <User>, <Password>
Examples:
	| Website   | User     | Password      |
	| demoblaze | BugsBunny | LolaBunny123! |
