Feature: Smoke tests
  Tests in this suite should test login and charging station selection mechanisms

  @smoke
  Scenario: Login and select a charging station
    Given 'Login' screen is shown
    When Enter username "test" and password "passport"
    Then Login error message is displayed
    And Return to the Login screen
    When Enter username "test" and password "pass"
    Then 'Map' screen is shown
    When Find the charging station on the map
    Then Verify charging station data is properly displayed