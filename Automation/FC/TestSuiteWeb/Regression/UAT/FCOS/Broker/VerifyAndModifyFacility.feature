﻿Feature: VerifyAndModifyFacility

A short summary of the feature

@regression
Scenario: VerifyAndModifyFacility
	Given The website <Website> is started
	When Login to <Website> as <Email>, <Password>

Examples:
	| Website   | Email                         | Password     |
	| FcosAzure | broker.testim@franchiczar.com | FranchiCzar! |
