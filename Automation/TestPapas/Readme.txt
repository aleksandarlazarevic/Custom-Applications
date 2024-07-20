Tech stack: Java + Selenium + Maven + Cucumber + JUnit + Log4j + ExtentReports

The main approach to the testing framework is to separate the code into 2 main areas: one specific to the driver logic, helper, extension and reporting utilities (i.e. the code the tester would rarely change once set up properly) and the other are that consists of tests related logic like tests themselves, test step definition methods and page object model structure where new pages and elements would be defined (this is the area the tester works most on when writing tests). This approach allows for non-skilled testers or any other stakeholders to be able to quickly adapt and write tests with ease (using BDD approach with Cucumber really helps there as, once there is enough test actions implemented in the code, writing new tests can come down to using just plain english in the cucumber feature files). 

Driver engine related logic is organized in a modular way such that a configuration file determines which driver should be initialized. This allows for greater scalability so that for instance a module for running mobile tests (Appium driver) or API tests could be easilly added. Test hooks perform actions related to choosing which engine to run, based on the data from the configuration file provided.

TestRunParameters folder contains configuration file that dictates the parameters for the test run (like browser usage, urls, timeouts, reports destination etc.). This file should not be a part of the repository files but rather this folder should remain empty, as the idea is for the pipeline to copy the appropriate configuration there when executing, hence providing scalability in terms of being able to run tests in multiple environments. For this assignment I've intentionally commited the configuration file so that the tests can be run locally, and I've also configured runSmokeTests.yml file for running the tests from the pipeline.

AutomationReports folder contains reporting data, like html report, log file and screenshots for failed tests only (this could be adjusted for all test steps to have screenshots but the practice has shown that such an approach would just generate unnecessary disk consumption so the focus is only on failed steps).

Tasks:
- 1. DB + API Test - addressed in 1_DbAndApiTest.feature file

- 2. API Test - addressed in 2_ApiTest.feature file

- 3. Google meet call (UI Test) - addressed in 3_GoogleMeetCall.feature file. The test scenario assumes starting a call and joining it on a same device (i.e. my laptop) which is not possible given the fact that 
the camera cannot be shared across browsers, so I improvised.s

- 4. WebGoat Performance Testing - addressed in 4_WebGoatPerformanceTesting.feature file. I have fairly limited experience with performance testing, hence another improvisation, but I did enjoy working on it.