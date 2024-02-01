Feature: UpdateTruck

@regression
Scenario: Update Truck should update Truck properties
	Given I have a Trucks in database
	| Code							| Name			| Description | Status 
    | Code111	| Name[[var=RegEx([0-9]{6})]]		| Description | 0   
	When I update Truck properties
	| Code							| Name			| Description | Status 
    | Code111						| Name22222		| Description | 1   
    Then the response has an HTTP status code of 200
    And I get an updated truck properties as a response
