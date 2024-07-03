Feature: Perform Mixed Arithmetic Operations
![Calculator](https://specflow.org/wp-content/uploads/2020/09/calculator.png)
Simple calculator for adding **two** numbers

Link to a feature: [Calculator](SpecFlowProject1/Features/Calculator.feature)
***Further read***: **[Learn more about how to generate Living Documentation](https://docs.specflow.org/projects/specflow-livingdoc/en/latest/LivingDocGenerator/Generating-Documentation.html)**

@mytag
Scenario Outline: Perform Mixed Arithmetic Operations
	Given I  Launch Calculator
	Given the first number is <num1>
	And the second number is <num2>
	When the two numbers are added
	Given the third number is <num3>
	When the third number subtracted
	Given the fourth number is <num4>
	When the fourth number multiplied
	Then the result should be <result>
	Then Display testRun status for <testdataName>
	Then Close Calculator

Examples:
	| testdataName | num1 | num2 | num3 | num4 | result  |
	| data1        | 70   | 20   | 30   | 3    | 180     |
	| data2        | 50   | 40   | 100  | 5    | -50     |
	| data3        | 20   | 20   | 10   | 2    | 60      |
	| data4        | 30   | 70   | 30   | 80   | 5600    |
	| data5        | 90   | 60   | 20   | 9000 | 1170000 |

Scenario: Arithmetic Operations using table
	Given I  Launch Calculator
	Then I can verify Mixed calculations for the following rows
		| testdataName | num1 | num2 | num3 | num4 | result  |
		| data1        | 70   | 20   | 30   | 3    | 180     |
		| data2        | 50   | 40   | 100  | 5    | 50     |
		| data3        | 20   | 20   | 10   | 2    | 60      |
		| data4        | 30   | 70   | 30   | 80   | 5600    |
		| data5        | 90   | 60   | 20   | 9000 | 1170000 |
	Then Close Calculator


Scenario: Arithmetic Operations using createinstance
	Given I  Launch Calculator
	Then I can verify Mixed calculations for one row
		| testdataName | num1 | num2 | num3 | num4 | result |
		| data1        | 70   | 20   | 30   | 3    | 180    |
	Then Close Calculator




