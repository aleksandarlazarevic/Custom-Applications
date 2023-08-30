Feature: Login
Log in with valid email address
@sanity

Scenario: Log in as a franchizee
	Given The app is launched
	Then Login screen appears
		And Verify that the email <Email> is not bound to another device
		Then Enter email <Email>
	When Click the Login button
	Then Microsoft Sign in screen appears
	When Enter email <Email> on Microsoft's form
	And Microsof Next in button is clicked
	And Microsoft password <Password> is entered
	And Microsoft SignIn button is clicked
	And Microsoft Stay Signed in is accepted
Examples:
	| Email                    | Password     |
	| testim.i24zee@iron24.com | FranchiCzar! |

