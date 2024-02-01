Feature: DeleteTruck

A short summary of the feature

@tag1
Scenario: Delete Truck should remove from database
	Given I have a Trucks in database
	| Code							| Name							| Description | Status 
    | Code[[var=RegEx([0-9]{7})]]	| Name[[var=RegEx([0-9]{6})]]	| Description | 0   
	| Code[[var=RegEx([0-9]{7})]]	| Name[[var=RegEx([0-9]{6})]]	| Description | 1   
	| Code33312						| Name[[var=RegEx([0-9]{6})]]	| Description | 2  
	When I delete the truck with code 'Code33312'
	Then the response has an HTTP status code of 200
    And Truck is deleted
