Feature: Favourite List
    A simple favourite list that we can add or remove products in order to buy them later
    
@mytag
Scenario: Create a new favourite list
    When I create a new favourite list
    Then the favourite list should be empty
    
Scenario: Add a new product to the favourite list
    Given I create a new favourite list
    When I select the favourite list and press the add favourite button on the product detail page
    Then the product should be added to the favourite list

Scenario: Remove a product from the favourite list
    Given I create a new favourite list
    And I select the favourite list and press the add favourite button on the product detail page
    When I press the remove product button on the favourite list page
    Then the product should be removed from the favourite list
    