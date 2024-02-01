Feature: GetTruck

@regression
Scenario: Get Truck by Code should return correct data
	Given I have a Trucks in database
	| Code							| Name							| Description | Status 
    | Code[[var=RegEx([0-9]{7})]]	| Name[[var=RegEx([0-9]{6})]]	| Description | 0   
	When I send a request of get a Truck 
	Then the response has an HTTP status code of 200
	And I get a truck detail as a response
