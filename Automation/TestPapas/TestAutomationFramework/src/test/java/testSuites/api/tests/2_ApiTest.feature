Feature: Tests for the second assignment

  @smoke
  Scenario: Save random image as favorite
    Given 'TheDogAPI' testing API is used
    When Obtain random image id
    And Save obtained image as favorite
    Then Image is successfully saved as favorite