Feature: Automation Service HealthCheck

Automation Service HealthCheck And Smoke

    @API
    @HC
    Scenario: Automation Service HealthCheck - GET WorkFlows
        Given Automation Service: Get WorkFlows

    @API
    @HC
    Scenario: Automation Service HealthCheck - GET Automation Webhook Url
        Given Automation Service: Get the Automation Webhook Url

    @API
    @HC
    Scenario: Automation Service HealthCheck - GET WorkFlow By ID
        Given Automation Service: Get WorkFlows
        Then Automation Service: Get WorkFlow By ID

    @API
    @HC
    Scenario: Automation Service HealthCheck - GET SetPlans
        Given Automation Service: Get SetPlans

    @API
    @HC
    Scenario: Automation Service HealthCheck - GET SetPlan By ID
        Given Automation Service: Get SetPlans
        Then Automation Service: Get SetPlan By ID

    @API
    @HC
    Scenario: Automation Service HealthCheck - GET WorkFlow SetPlan By ID
        Given Automation Service: Get WorkFlows
        Then Automation Service: Get WorkFlow SetPlan By Work Flow ID

    @API
    @HC
    Scenario: Automation Service HealthCheck - GET Triggers
        Given Automation Service: Get Triggers

    @API
    @HC
    Scenario: Automation Service HealthCheck - GET Configurations
        Given Automation Service: Get Configurations

    @API
    @HC
    Scenario: Automation Service HealthCheck - GET WebHook Test
        Given Automation Service: Get WebHook Test

    @API
    @HC
    Scenario: Automation Service HealthCheck - POST WorkFlow
        Given Automation Service: Create WorkFlow

    @API
    @HC
    Scenario: Automation Service HealthCheck - Set WorkFlow Active
        Given Automation Service: Create WorkFlow
        Then Automation Service: Set WorkFlow Active

    @API
    @HC
    Scenario: Automation Service HealthCheck - Set WorkFlow In-Active
        Given Automation Service: Create WorkFlow
        Then Automation Service: Set WorkFlow In-Active

    @API
    @HC
    Scenario: Automation Service HealthCheck - Create SetPlan
        Given Automation Service: Create SetPlan

    @API
    @HC
    Scenario: Automation Service HealthCheck - Add Plan To Set
        Given Automation Service: Create WorkFlow
        Then Automation Service: Add Plan To Set

    @API
    @HC
    Scenario: Automation Service HealthCheck - Copy WorkFlow SetPlan To WorkFlow Set
        Given Automation Service: Copy WorkFlow SetPlan To WorkFlow Set

    @API
    @HC
    Scenario: Automation Service HealthCheck - OptIn WorkFlow Set
        Given Automation Service: OptIn To WorkFlow Set

    @API
    @HC
    Scenario: Automation Service HealthCheck - OptOut WorkFlow Set
        Given Automation Service: OptOut To WorkFlow Set

    @API
    @HC
    Scenario: Automation Service HealthCheck - Create Configurations
        Given Automation Service: Create Configurations