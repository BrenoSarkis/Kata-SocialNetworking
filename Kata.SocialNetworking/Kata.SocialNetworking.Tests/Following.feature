Feature: Following
	In order to see other users posts
	As a user
	I want to subscribe to their posts

Scenario: Following an User
	Given User posts message: "Bob -> hello world!"
	When "Alice" follows "Bob"
	Then "Alice"'s wall should contain: "Bob - hello world! (0 seconds ago)"
