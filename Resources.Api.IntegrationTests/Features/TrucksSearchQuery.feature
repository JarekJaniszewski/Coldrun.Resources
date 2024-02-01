Feature: TrucksSearchQuery

@regression
Scenario: Search trucks should return trucks with the searched name
	Given I have a Trucks in database
	| Code		| Name		| Description | Status 
    | Code1121	| Truck1121	| Description1 | 0   
	| Code22234	| Name22234	| Description2 | 1   
	| Code33312	| Truck3331	| Description3 | 2  
	When I search for trucks which name contain 'Truck' and sorted by 'Status'
	Then the response has an HTTP status code of 200
	And I get trucks as a response with the searched name

@regression
Scenario: Search trucks should return trucks with the searched status
	Given I have a Trucks in database
	| Code		| Name		| Description | Status 
    | Code1123	| Truck1121	| Description1 | 0   
	| Code22234	| Name22234	| Description2 | 1   
	| Code33315	| Truck3331	| Description3 | 0  
	When I search for trucks which status equal '0' and sorted by 'Code desc'
	Then the response has an HTTP status code of 200
	And I get trucks as a response with the searched status