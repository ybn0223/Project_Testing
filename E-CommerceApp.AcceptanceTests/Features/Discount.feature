Feature: Apply Discount to Order

  Scenario: Apply 10% discount to an order
    Given the order has items with prices and quantities
    When the discount of 10% is applied
    Then the total price of the order should be 31.5
    And the discount flag should be true

  Scenario: Apply 0% discount to an order
    Given the order has items with prices and quantities
    When the discount of 0% is applied
    Then the total price of the order should be 35
    And the discount flag should be false