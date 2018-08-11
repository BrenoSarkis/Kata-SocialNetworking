Feature: Posting
	In order to record my thoughts
	As a user
	I want to post to my wall

Scenario: Post a message
	Given I'm using the system as "Alice"
	When I post the message "I love the weather today"
	Then I should see "Alice -> I love weather today"
