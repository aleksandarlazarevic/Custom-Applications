Feature: Tests for the first assignment

  @smoke
  Scenario: Create a test on google testing api
    Given Google testing API is used
    When Create a test with a name of 'test777' and description of 'description777'
    Then Test is successfully created

  @smoke
  Scenario: Read a test from google testing api
    Given Google testing API is used
    When Get a test with an id of '777777'
    Then Test is successfully retrieved

  @smoke
  Scenario: Update a test on google testing api
    Given Google testing API is used
    When Update a test with an id of '777777' with name of 'test778' and description of 'description778'
    Then Test is successfully updated

  @smoke
  Scenario: Delete a test from google testing api
    Given Google testing API is used
    When Delete a test with an id of '777777'
    Then Test is successfully deleted

  @smoke
  Scenario: Retrieve data fom MSSQL database
    When Get data from the database table 'spt_monitor'
    Then Data is successfully retrieved