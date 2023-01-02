Feature: Cart

Testing order manipulations

@smoke
Scenario: Place order
	When Navigate to Cart
	Then Place order

Scenario: Remove item from cart
	When Navigate to Cart
	Then Remove item <Item>
Examples:
	| Item         |
	| Nexus 6      |
	| ASUS Full HD |
