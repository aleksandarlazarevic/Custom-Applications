Feature: Register
Register with random email address
@sanity @done

Scenario: Register
	Given The app is launched
	Then Login screen appears
	When Enter random email address
		#And Click the Login button
	Then Registration screen appears
	When Profile picture is added
		And First name <FirstName> is entered
		And Last name <LastName> is entered
		And Date of birth <DateOfBirth> is entered
		And Country Code for <CountryCode> is chosen
		And Phone number <PhoneNumber> is entered
		And Terms and Conditions are agreed
		And Next button is clicked
	Then Find the gyms near you screen appears
		#And Use Current Location link is clicked
		#And Allow access to the device's location while using the app
		And Custom location <Location> is entered
		And First available gym is selected
		And Continue button is clicked
	Then Verify Your Email Address screen appears
	When Send Verification Code button is clicked
		And Verification code is received via email
		And Email Verification Code is entered
		And Verify Code button is clicked
	Then Email Verification is successfull
	When Multi-factor authentication screen appears
		#And Country Code for <CountryCode> is chosen
		#And Phone number <PhoneNumber> is entered
		And Send Code button is clicked
	Then Sms verification code is retrieved
	When Sms verification code is entered
		And Verify SMS Code button is clicked
	Then Create Password screen appears
	When New password is entered
		And New password is confirmed
		#And Continue button is clicked
	Then Congratulations screen appears
	When Continue link is clicked
	Then Ready to workout screen appears
	When Let's go button is clicked
	Then Select your membership screen appears
	When First available membership is selected
	Then Add members screen is shown
	When Skip this step link is clicked

	#When Add members button is clicked
	#Then Add new member screen appears

	Then Payment method screen is shown
	When Credit card data is entered
	Then Is this correct screen appears
	When Pay now button is clicked
	Then Welcome to screen appears
	When Go to home screen link is clicked
	Then Home Screen appears
		And Doors are shown
	When First available door is clicked
	Then The door is unlocked
	When Log out
	Then Login screen appears
	
	Examples: 
	| FirstName | LastName | DateOfBirth | CountryCode | PhoneNumber | Location |
	| Franchi   | Test     | 01011991    | Egypt       | 1093490402  | Pearland |
