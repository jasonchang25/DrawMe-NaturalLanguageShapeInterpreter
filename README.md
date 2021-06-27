# DrawMe-NaturalLanguageShapeInterpreter
DrawMe - Draws shapes based on the measurements provided in natural language

<strong>Specifications:</strong> 
    
    Build & software used:
    - .Net Core/5 - Back End
    - Textc natural language processing library 
    - MSTests
    - Angualar 12 - Front End
    
<strong>Building Project and testing:</strong> 

    Backend:
    1. Navigate to directory called "NaturalLanguageInterpretor" and launch the solution
    2. Run the 'InputInterpreter' project in debug mode using IIS
    3. Swagger has been added for ease of API testing
    
    Frontend:
    1. Navigate to directory called "Frontend"
    2. run 'npm i' in console
    3. run 'ng serve'
    
<strong>Features:</strong> 

    - Backend valdiation of input string formatting with error handling
    - Backend validation of invalid measurements and amounts provided for specified shape with error 
      handling
    - Front end example buttons that render the shape and specify example string input
    
<strong>Overview</strong>

A web application that allows the user to draw a shape of x measurements based on the format:
    Draw a(n) &lt;shape&gt; with a(n) &lt;measurement&gt; of &lt;amount&gt; (and a(n) &lt;measurement&gt; of &lt;amount&gt;)...
    
Example: Octagon rendered
![image](https://user-images.githubusercontent.com/21240686/123551879-0eaae580-d7b7-11eb-9ea4-c3e0fc0a5423.png)

Example: Invalid string input
![image](https://user-images.githubusercontent.com/21240686/123552243-a3621300-d7b8-11eb-9085-098130571380.png)

