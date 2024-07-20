Feature: Tests for the fourth assignment

  @performance
  Scenario: Register a new user
    Given 'Webgoat' website is opened
    When Register multiple users
      | numberOfUsers |
      | 50            |
      | 100           |
      | 200           |

  @performance
  Scenario: Log in as an existing user
    Given 'Webgoat' website is opened
    When Login multiple users
      | numberOfUsers |
      | 50            |
      | 100           |
      | 200           |