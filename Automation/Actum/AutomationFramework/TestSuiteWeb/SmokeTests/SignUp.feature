Feature: SignUp

Sign up tests

@smoke
Scenario: Sign up new account
	Given The website <Website> is started
	When Sign up new account <User>, <Password>
Examples:
	| Website   | User      | Password      |
	| demoblaze | LonyBunny | LonyBunny123! |

@smoke
Scenario: Sign up new account with weak password
	Given The website <Website> is started
	When Sign up new account <Website>, <User>, <Password>
Examples:
	| Website   | User | Password |
	| demoblaze | Tony | Bunny    |
