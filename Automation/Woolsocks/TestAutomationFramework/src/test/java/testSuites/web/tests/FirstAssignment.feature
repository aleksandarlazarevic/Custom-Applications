Feature: Tests for the first assignment

  Scenario: Obtain temporary email address
    Given 'Mailinator' is used as online email service
    When Obtain a temporary email address
    Then Temporary email is set

  Scenario: Sign in to Woolsocks
    Given 'Woolsocks' website is opened
    When 'WoolsocksHomePage' page is displayed
    And Click 'Sign In' button
    And User enters temporary obtained email address
    And Click 'Continue' button
    Then 'Login link is sent to' message is shown in a popup
    When Open mailbox
    And Find Verification Mail
    Then Click 'Verify your email' button in the mail

  Scenario: Play a song on YouTube
    Given 'YouTube' website is opened
    When Search for song 'Michael Buble Welcome to the jungle'
    And Play the first found song
    Then The song plays successfully

