Feature: Tests for the first assignment

  Scenario: Obtain temporary email address
    Given 'Mailinator' is used as online email service
    When Obtain a temporary email address
    Then Temporary email is set

  Scenario: Sign in to Woolsocks
    Given 'Woolsocks' website is opened
    When Accept all cookies
    And Confirm that the selected country is correct
    Then 'WoolsocksHomePage' page is displayed
    When Click 'Sign In' button
    And User enters temporary obtained email address
    And Click 'Continue' button
    Then 'Login link is sent to' message is shown in a popup
    When Verification mail is received and the link is clicked
    Then Login succeeds

  Scenario: Play a song on YouTube
    Given 'YouTube' website is opened
    When Search for song 'Michael Buble Welcome to the jungle'
    Then Play the first found song