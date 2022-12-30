Feature: CRUDBookings
Creates a new booking in the API
Updates a current booking
Updates a current booking with a partial payload
Returns the ids of all the bookings that exist within the API. Can take optional query strings to search and return a subset of booking ids.

@smoke
Scenario: Create a booking
	Given The API is running
	Then Create a booking with <firstname> <lastname> <totalprice> <depositpaid> <checkin> <checkout> <additionalneeds>
Examples:
	| firstname | lastname | totalprice | depositpaid | checkin    | checkout   | additionalneeds |
	| Jim       | Brown    | 111        | true        | 2018-01-01 | 2019-01-01 | Breakfast       |

@smoke
Scenario: Update a booking
	Given The API is running
	Then Update a booking with <id> <firstname> <lastname> <totalprice> <depositpaid> <checkin> <checkout> <additionalneeds>
Examples:
	| id | firstname | lastname | totalprice | depositpaid | checkin    | checkout   | additionalneeds |
	| 1  | Jim       | Brown    | 111        | true        | 2018-01-01 | 2019-01-01 | Breakfast       |

@smoke
Scenario: Partial update a booking
	Given The API is running
	Then Partially update a booking with <id> <firstname> <lastname>
Examples:
	| id | firstname | lastname |
	| 1  | Jim       | Brown    |

@smoke
Scenario: Delete a booking
	Given The API is running
	Then Delete booking with <Id>
Examples:
	| example description | Id |
	| First booking       | 1  |
