Feature: Posting
	In order to record my thoughts
	As a user
	I want to post to my wall

Scenario: Post a message
	Given I post the message "Alice -> I love the weather today"
	When I visit "Alice"'s wall
	Then I should see "Alice - I love the weather today (0 seconds ago)"

Scenario: Messages carry their elapsed time
	Given I post the message "Alice -> I love the weather today"
	And I wait for 2 seconds
	When I visit "Alice"'s wall
	Then I should see "Alice - I love the weather today (2 seconds ago)"