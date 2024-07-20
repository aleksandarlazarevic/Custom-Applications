Feature: Tests for the first assignment

  Background: Required data exists in the database
    Given 'MongoDB' database is used
    And 'testDb' database exists
    Then 'MongoDB' database has required 'Activity' collection data

  @smoke
  Scenario: Save random image as favorite
    Given Data is retrieved from 'Activity' collection
    And 'BoredAPI' testing API is used
    When Get data from 'BoredAPI'
    Then Compare api and database data
    When 'Boredapi' website is opened
    Then Validate 'Submit' button color
    When Press 'Submit' button
    Then Compare value from Activity object element with value in Response