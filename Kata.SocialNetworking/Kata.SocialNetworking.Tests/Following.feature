Feature: Following
	In order to see other users posts
	As a user
	I want to subscribe to their posts

Scenario: Following an user
	Given User posts message: "Bob -> hello world!"
	When "Alice" follows "Bob"
	Then "Alice"'s wall should contain: "Bob - hello world! (0 seconds ago)"

Scenario: Following multiple users
	Given User posts message: "Bob -> hello world!"
	And User posts message: "Charlie -> hello!"
	When "Alice" follows "Bob"
	And "Alice" follows "Charlie"
	Then "Alice"'s wall should display: 
	| Message                            |
	| Bob - hello world! (0 seconds ago) |
	| Charlie - hello! (0 seconds ago)   |	
