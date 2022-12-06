<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li><a href="#description">Description</a></li>
    <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    <li><a href="#adding-new-functions">Adding new functions</a></li>
    <li><a href="#answer-to-part-two">Answer to part two</a></li>
  </ol>
</details>

<!-- DESCRIPTION -->
## Description

This solution is comprised of 3 projects:
- <b>ChannelProcessing</b>
   - This project is a C# library that contains all the logic necessary to calculate the values for a set of functions (as per the "Channel Processing System" document
   - The calculators are split into calculators for metric values like 'b' and channel values like 'Y'
   - There is also an additional calculator called 'SelectedChannelCalculator' (and 'SelectedMetricCalculator') which is a generic function and allows the method call to specific a channel type (or metric type) rather than calling a specific calculator
- <b>ChannelProcessingUnitTests</b>
   - This project is a C# unit test project built with xUnit and Moq.
- <b>ExampleChannelProcessorApp</b>
   - This project is a Console application that allows a user to select the type of value it would like calculated (either a metric or channel) and the app will return the value based on the example data provided.
   - The example data can be found in their original format as embedded resources in this project directory


<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- GETTING STARTED -->
## Getting Started

### Prerequisites

In order to run the console application, you will need:
- .NET 6.0 SDK
- Visual Studio

### Installation

In order to run the application, do the following:
- Set the 'ExampleChannelProcessorApp' project (console application) to be the Startup Project in Visual Studio
- Run the application

If you wish to change the inputs to the application:
- Replace the existing files called "parameters.txt" or "channels.txt" in the directory ExampleChannelProcessorApp.ExampleInputs
- Please ensure that you maintain the existing format for values where each variable name and its set of values are written on an individual line. See the existing files for reference.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- UNIT TESTS -->
## Unit tests

- The project named <b>ChannelProcessingUnitTests</b> is a unit test project built with xUnit and Moq.
- Each file is a set of unit tests for a single implementation of a calculator
- All calculator implementations have been covered by the unit tests, including the 'SelectedChannelCalculator'
- In order to ensure that all calculators are unit testable, I have used dependency injection and used Moq as the framework for mocking those dependencies

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- ADDING NEW FUNCTIONS -->
## Adding new functions

If we require to extend the existing list of functions, then the following new code would need to be added (please see the examples suggested for reference):
- Create a new Enum value in MetricType.cs or ChannelType.cs (depending on what the function will return)
- Create a new interface for your calculator that inherits from IChannelCalculator or IMetricCalculator (depending on what the function will return) e.g. 'IChannelACalculator'
- Create a new class for the calculator that implements the new interface e.g. 'ChannelACalculator'
- Create a new unit test class for your new calculator e.g. 'ChannelACalculatorTests'

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- ANSWER TO PART TWO -->
## Answer to Part Two

As requested in the "Channel Processing System" document, the answer to part 2 (and the required value of metric b) = 6.2698521667770072347862686994.

This has been calculated using the Console application in this repository.

<p align="right">(<a href="#readme-top">back to top</a>)</p>