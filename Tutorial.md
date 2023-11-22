# Tutorial: 2D Physics Simulation Using SplashKit with OOP Concepts (VSCode)
## Introduction
By the end of this tutorial, you will know how to create a simple 2D physics simulation where objects with different properties (like velocity) interact with each other through collisions. These objects can be viewed moving and bouncing around in a window created using SplashKitSDK
## Prerequisites
Before we begin, please ensure you have the following:
* [SplashKitSDK](https://splashkit.io/articles/installation/mac/) Installed: On your MacOS, setup and install the SplashKitSDK. This SDK is essential as it provides the libraries and tools necessary for the graphics and physics functionalities that will be used.
* Visual Studio Code: A basic code editor installed to write and manage your C# code.
* C# Programming Knowledge: Basic understanding of C# programming language to follow along with the coding parts of this tutorial.
* OOP Concepts Understanding: Familiarity with Object-Oriented Programming concepts, as the structure of the simulation will rely heavily on these concepts.


## Understanding Basic Concepts
### 1. What is a Class?
A class is a blueprint for creating objects with shared characteristics and behaviors. It is a fundamental concept in OOP. This allows for easy organization and reuse of code.

### 2. What is an Object?
An object is an instance of class that can carry actual data and perform actions. A class can create many different objects. For example, the blueprint of Truck can result in many different colors, styles of trucks.

## What is SplashKitSDK?
SplashKitSDK is a library that provides a range of functionalities including graphics, audio, and input handling which can be utilized to create 2D games or simulations in a variety of programming languages including C#.

## Role of SplashKitSDK in this tutorial
### 1. Graphics Rendering
SplashKitSDK provides tools for rendering graphics onto the screen. It allows the loading and drawing of bitmap images, which are used to represent the entities. Here, `SplashKit.LoadBitmap` was used to load images and `Draw` was used to draw the loaded images at the entities' respective positions.

### 2. Collision Detection
SplashKit SDK simplifies the process of detecting collisions between entities. It offers functions like `BitmapCollision`, which was used in the `CheckCollision` method to detect collisions between entities. Once a collision is detected, the entities' behaviors can be adjusted accordingly.

### 3. Mathematical Operations
To simulate physics properties such as velocity and position, mathematical operations are necessary. SplashKit provides various vector functions that assist in this, like `VectorAdd` and `VectorInvert`, which were used to update the position and velocity of the entities, respectively.

### 4. Window Management
SplashKit aids in creating and managing the window where the program runs. The Window class allows for the creation of a window, the title and dimensions are set. It also provides methods to clear the window, refresh it, and check if a close has been requested, which are used in the main loop to continuously update and render the simulation.

## Steps for creating the program
### 1. Setting Up Project
In this step, you create a new project in a folder of your choice. You will use terminal commands to navigate to your chosen folder and set up a new C# project. The SplashKit library is then integrated into your project so you can use its functionalities.
* ```cd + your-directory-here```: It is used to change the directory to your project's folder.
* ```skm dotnet new console```: This command sets up a new console project for coding.
* ```skm dotnet restore```: It installs necessary packages for your project.
* ```using SplashKitSDK;```: This line of code is added at the top of your C# files (.cs file) to use the SplashKit library in your project.

### 2. Creating a Base Entity Class
Creating a Base Entity Class
Here, you create a base class named Entity which will represent general objects in the simulation. It contains properties for image representation, position, and velocity, and methods for updating the object's position and drawing it on the screen. Before that, there are some fundamental concepts for better understanding.
* ***Constructor***: Special method in a class that initializes new objects created from that class. Constructors are a fundamental link between classes and objects in OOP.
* ***Properties***: Variables inside a class that hold data for each object created from the class.
* ***Methods***: Functions inside a class that define the behaviors of objects created from the class.
* ***Abstract Class and Method***: A class/method that cannot be instantiated itself but must be inherited by other classes. The abstract method should be implemented in child classes.
* ***Access Modifiers***: Access modifiers are keywords that define the accessibility of a member (a property, method, or variable) in a class. They determine the visibility of that member from other classes or assemblies. There are 3 types of access modifiers: public, private, protected.
  * **public**: This access modifier allows the member to be accessed from anywhere — both inside and outside of its containing class or assembly. Members declared as public are fully accessible, making it the least restrictive access level.
  * **private**: This access modifier allows the members to be accessed only inside of its containing class.
  * **protected**: A protected member is accessible within its containing class and also within classes that inherit from the containing class. This means that derived classes can access protected members of their base class, but other classes cannot.
* ***Getters and Setters (Property Accessors)***: Properties in C# provide a flexible mechanism to read, write, or compute the value of a private field. Properties can be used as if they are public data members, but they are actually special methods called accessors, namely "getters" and "setters".
  * **Getter (get)**: The get accessor is used to return the property value.
  * **Setter (set)**: The set accessor is used to assign a new value to the property.

Here is the code for declaring an Entity abstract class
```csharp
public abstract class Entity
{
    // The code here
}
```

Next, properties will be added to the Entity class to store the image, position, and velocity of objects. Note that these are the attributes of the class itself, so they have to be declared inside the class.
```csharp
// This line of code defines a public property of type Bitmap. The property can be read from any class (public get) but can only be modified from within the class where it is defined (private set).
public Bitmap _Bitmap { get; private set; }
// Similar to _Bitmap, this is a public property of type Vector2D
public Vector2D Position { get; private set; }
// This line declares a field, not a property. It is of type Vector2D and has a protected access modifier
protected Vector2D Velocity;
```

Next step is to add a constructor to the Entity class. The method signature are as below:
* ***public***: This is an access modifier that indicates that the constructor is accessible from any class, not just within its own class or from derived classes.
* ***Entity***: This is the name of the constructor, which matches the name of the class. This indicates that it is a constructor, not a regular method.
* ***(Bitmap bitmap, Vector2D position, Vector2D velocity)***: These are the parameters that the constructor takes. When creating a new Entity object, you will need to provide values for these parameters.

Inside the method body, the constructor initializes the new object's state:
* ```_Bitmap = bitmap;```: This line of code assigns the value passed as the bitmap parameter to the _Bitmap property of the new Entity object. _Bitmap holds a reference to a Bitmap object, which contains image data that represents the entity visually.
* ```Position = position;```: This line assigns the value passed as the position parameter to the Position property of the new Entity object. Position holds a Vector2D object that represents the two-dimensional coordinates (X and Y) of the entity in the simulation.
* ```Velocity = velocity;```: This line assigns the value passed as the velocity parameter to the Velocity field of the new Entity object. Velocity also holds a Vector2D object that represents the velocity of the entity, describing how its position changes over time.
```csharp
public Entity(Bitmap bitmap, Vector2D position, Vector2D velocity)
{
    _Bitmap = bitmap;
    Position = position;
    Velocity = velocity;
}
```

After that, methods are added to the Entity class. An object contains methods guiding it to perform specific behaviors. Here, the program needs methods to update the object's position, to draw the object on the screen. Below, there is an abstract method named `CheckCollision`. This method allows child class to override it to implement specific behaviors unique to that class.
```csharp
// Create Update method
public void Update(Window gameWindow)
{
    // Modify the position of the entity based on its velocity
    Position = SplashKit.VectorAdd(Position, Velocity);

    // Invert the velocity to simulate a bounce when a collision occurs
    if (Position.X < 0 || Position.X + _Bitmap.Width > gameWindow.Width)
    {
        Velocity.X = -Velocity.X;
    }
    if (Position.Y < 0 || Position.Y + _Bitmap.Height > gameWindow.Height)
    {
        Velocity.Y = -Velocity.Y;
    }
}
```
This method is responsible for updating the state of an entity in the simulation. It takes a parameter ```gameWindow```, which is an instance of the Window class, representing the game's window. The entity's ```Position``` is updated based on its current ```Velocity``` using the ```SplashKit.VectorAdd``` method, which adds the X and Y values of the velocity vector to the X and Y values of the position vector, respectively, thus updating the entity's position. The conditional statements check whether the entity has hit the boundaries of the ```gameWindow```:
* If it hits the left or right boundaries, the X component of its velocity is inverted, causing it to "bounce" and move in the opposite horizontal direction. 
* If it hits the top or bottom boundaries, the Y component of its velocity is inverted, causing it to "bounce" and move in the opposite vertical direction.
```csharp
// Create Draw method
public void Draw()
{
    _Bitmap.Draw(Position.X, Position.Y);
}
```
This method is in charge of drawing the entity onto the game window:
* The ```_Bitmap``` field, which stores the image representing the entity, has its ```Draw``` method called, which draws the image at the current Position (X and Y coordinates) of the entity.
```csharp
// Create abstract method CheckCollision
public abstract bool CheckCollision(Entity other);
```
This line of code defines the signature for an abstract method named ```CheckCollision```:
* Being abstract, it means that this method must be implemented by any non-abstract classes that inherit from this class.
* The method takes a parameter named ```other```, which is another Entity object, and it is expected to return a boolean value, indicating whether a collision with the other entity has occurred or not.

### 3. Creating a Physics Object Class
Create a new class named PhysicsObject that inherited from the Entity class. This class will have methods to check collisions between objects and to respond to collisions.
```csharp
// Create child class
public class PhysicsObject : Entity
{
    // Your code here
}
```
This line declares a new public class named PhysicsObject.
* ```PhysicsObject : Entity``` - This indicates that PhysicsObject is a subclass of the Entity class, and it will inherit all of the public and protected members of the Entity class.

```csharp
    // Create constructor
    public PhysicsObject(Bitmap bitmap, Vector2D position, Vector2D velocity) : base(bitmap, position, velocity){}
```
This line defines a constructor for the PhysicsObject class.
* The constructor takes three parameters: a Bitmap to represent the object's image, and two Vector2D objects to represent the object's initial position and velocity.
* ```public PhysicsObject(Bitmap bitmap, Vector2D position, Vector2D velocity) : base(bitmap, position, velocity)``` - This calls the base class (Entity) constructor to initialize the base class properties with the given arguments.

```csharp
    // Overrides the CheckCollision method
    public override bool CheckCollision(Entity other)
    {
        // Implement the collision check between this object and another entity using BitmapCollision
        return SplashKit.BitmapCollision(_Bitmap, Position.X, Position.Y, other._Bitmap, other.Position.X, other.Position.Y);
    }
```
This method overrides the ```CheckCollision``` method from the Entity class.
* It takes another Entity as an argument and checks if there is a collision with that entity using the ```SplashKit.BitmapCollision``` method. The method returns true if there is a collision and false otherwise.
```csharp
    // Create ApplyCollisionResponse method
    public void ApplyCollisionResponse(Entity other)
    {
        // Invert the velocity to simulate a bounce when a collision is detected
        if (CheckCollision(other))
        {
            Velocity = SplashKit.VectorInvert(Velocity);
        }
    }
```
This method is unique to the PhysicsObject class.
* It also takes another Entity as an argument and uses the ```CheckCollision``` method to determine if a collision has occurred.
* If a collision is detected (if CheckCollision returns true), it inverts the velocity of the object using ```SplashKit.VectorInvert```, simulating a bounce effect as a response to the collision.
  
### 4. Implementing the Program
Now let's create the program to set up the simulation with the defined classes.
```csharp
// Create class for implementation
public class Program
{
    // Create Main method for initializing program
    public static void Main()
    {
        // Your code here
    }
}
```
This line declares the Main method, which is the starting point of any C# console application. Being static means it can be called without an instance of the Program class.
```csharp
        // Create program window
        Window gameWindow = new Window("Tutorial", 700, 500);
        
        // Create bitmap objects
        Bitmap bitmap1 = SplashKit.LoadBitmap("Pig", "pig.png");
        Bitmap bitmap2 = SplashKit.LoadBitmap("Bat", "bat.webp");
        Bitmap bitmap3 = SplashKit.LoadBitmap("Ghost", "ghost.webp");
```
A window for the application is created with the title "Tutorial" and dimensions 700x500. Bitmap objects are loaded which will be used as the images for various objects in the simulation. Each bitmap is loaded with a name and the path to the image file.

```csharp
        // Create PhysicsObject objects
        PhysicsObject object1 = new PhysicsObject(bitmap1, new Vector2D { X = 100, Y = 100 }, new Vector2D { X = 5, Y = 5 });
        PhysicsObject object2 = new PhysicsObject(bitmap2, new Vector2D { X = 300, Y = 300 }, new Vector2D { X = -6, Y = -6 });
        PhysicsObject object3 = new PhysicsObject(bitmap3, new Vector2D { X = 200, Y = 200 }, new Vector2D { X = -5, Y = -5 });
```
Here, three PhysicsObject instances are created. They are initialized with a bitmap, a starting position, and a starting velocity.
```csharp
        // While not close, continuously run the program
        while (!gameWindow.CloseRequested)
        {
            // Your code here
        }
```
* A while loop runs indefinitely until the window is closed by the user.
```csharp
            // Clear window
            gameWindow.Clear(Color.White);

            SplashKit.ProcessEvents();

            object1.Update(gameWindow);
            object2.Update(gameWindow);
            object3.Update(gameWindow);

            object1.ApplyCollisionResponse(object2);
            object2.ApplyCollisionResponse(object1);
            object3.ApplyCollisionResponse(object1);
            object3.ApplyCollisionResponse(object2);
            object2.ApplyCollisionResponse(object3);
            object1.ApplyCollisionResponse(object3);

            object1.Draw();
            object2.Draw();
            object3.Draw();

            // Refresh window
            gameWindow.Refresh(60);
```
* At the beginning of each loop iteration, the window is cleared to white color.
* Events such as user inputs are processed by calling ```SplashKit.ProcessEvents()```.
* Each physics object is updated, which means their positions are updated based on their velocity, and collisions with the window boundaries are checked.
* Collision responses are applied to make objects bounce off each other when they collide.
* The objects are drawn in their new positions.
* The window is refreshed at a rate of 60 frames per second to show the updated graphics.
### 5. Program Running
After completing the code, the program can be carried out by using command `skm dotnet run` in the terminal