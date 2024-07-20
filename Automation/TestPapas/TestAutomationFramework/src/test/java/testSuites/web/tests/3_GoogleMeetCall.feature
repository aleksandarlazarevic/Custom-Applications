Feature: Tests for the third assignment

  @smoke
  Scenario: Validate Google Meet call
    Given Google meet call is started
    When Open 'Firefox' browser
    And Join Google meet call
    Then There is a call stream
    And Buttons are visible in both browsers
    And 'LeaveCall' button has 'red' color