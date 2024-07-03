Feature: Calculator
![Calculator](https://specflow.org/wp-content/uploads/2020/09/calculator.png)
Simple calculator for adding **two** numbers

Link to a feature: [Calculator](SpecFlowProject1/Features/Calculator.feature)
***Further read***: **[Learn more about how to generate Living Documentation](https://docs.specflow.org/projects/specflow-livingdoc/en/latest/LivingDocGenerator/Generating-Documentation.html)**

@mytag
Scenario: Add two numbers
	Given I  Launch Calculator
	Given the first number is 60
	And the second number is 40
	When the two numbers are added
	Then the result should be 100
	Then Display testRun status
	Then Close Calculator
	
Scenario:Write into notepad
	Given I Launch Notepad
	Given I type system time
	Then Display File content




