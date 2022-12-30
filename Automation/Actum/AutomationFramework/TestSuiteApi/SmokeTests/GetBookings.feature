Feature: GetBookings

Returns the ids of all the bookings that exist within the API. Can take optional query strings to search and return a subset of booking ids.

@smoke
Scenario: Get all bookings
	Given The API is running
	Then Get all bookings

@smoke
Scenario: Get specific booking
	Given The API is running
	Then Get booking with <Id>
Examples:
	| example description | Id |
	| First booking       | 1  |
	| Second booking      | 2  |