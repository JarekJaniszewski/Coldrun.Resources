Feature: CreateTruck

@regression
Scenario: Create a truck to database 
	Given I have a new Truck
	| Code							| Name							| Description | Status 
    | Code[[var=RegEx([0-9]{7})]]	| Name[[var=RegEx([0-9]{6})]]	| Description | 0        
	When I send a request of create a Truck 
	Then the response has an HTTP status code of 200
    And truck added to database