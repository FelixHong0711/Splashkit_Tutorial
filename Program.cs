using SplashKitSDK;
using System;

public class Program
{
    // Create Main method for initializing program
    public static void Main()
    {
        // Create program window
        Window gameWindow = new Window("Tutorial", 700, 500);
        
        // Create bitmap objects
        Bitmap bitmap1 = SplashKit.LoadBitmap("Pig", "pig.png");
        Bitmap bitmap2 = SplashKit.LoadBitmap("Bat", "bat.webp");
        Bitmap bitmap3 = SplashKit.LoadBitmap("Ghost", "ghost.webp");

        // Create PhysicsObject objects
        PhysicsObject object1 = new PhysicsObject(bitmap1, new Vector2D { X = 100, Y = 100 }, new Vector2D { X = 5, Y = 5 });
        PhysicsObject object2 = new PhysicsObject(bitmap2, new Vector2D { X = 300, Y = 300 }, new Vector2D { X = -6, Y = -6 });
        PhysicsObject object3 = new PhysicsObject(bitmap3, new Vector2D { X = 200, Y = 200 }, new Vector2D { X = -5, Y = -5 });

        // While not close, continuously run the program
        while (!gameWindow.CloseRequested)
        {
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

            gameWindow.Refresh(60);
        }
    }
}