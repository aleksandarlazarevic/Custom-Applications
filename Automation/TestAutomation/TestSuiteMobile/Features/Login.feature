Feature: Login
Log in with valid credentials leads user to Clips screen
Log in with invalid credentials shows Login screen alert
@smoke
Scenario: Regular Log in
	Given The app is launched
	Then Login screen appears
	And Enter username and password as
		| Username  | Password  |
		| testuser | Testpass123! |
	When Click the Login button

Scenario: Log in with invalid credentials
	Given The app is launched
	Then Login screen appears
	And Enter username and password as
		| Username    | Password    |
		| B@dUs3rn@m3 | b@dP@$$w0rd |
	When Click the Login button

