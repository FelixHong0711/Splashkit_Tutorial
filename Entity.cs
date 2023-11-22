using SplashKitSDK;

// Create abstract class
public abstract class Entity
{
    // Declare _Bitmap as auto property
    public Bitmap _Bitmap { get; private set; }

    // Declare Position as auto property
    public Vector2D Position { get; private set; }

    // Declare Velocity as protected property
    protected Vector2D Velocity;

    // Create constructor
    public Entity(Bitmap bitmap, Vector2D position, Vector2D velocity)
    {
        _Bitmap = bitmap;
        Position = position;
        Velocity = velocity;
    }

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

    // Create Draw method
    public void Draw()
    {
        _Bitmap.Draw(Position.X, Position.Y);
    }

    // Create abstract method CheckCollision
    public abstract bool CheckCollision(Entity other);
}

public class PhysicsObject : Entity
{
    // Create constructor
    public PhysicsObject(Bitmap bitmap, Vector2D position, Vector2D velocity) : base(bitmap, position, velocity) { }

    // Overrides the CheckCollision method
    public override bool CheckCollision(Entity other)
    {
        // Implement the collision check between this object and another entity using BitmapCollision
        return SplashKit.BitmapCollision(_Bitmap, Position.X, Position.Y, other._Bitmap, other.Position.X, other.Position.Y);
    }

    // Create ApplyCollisionResponse method
    public void ApplyCollisionResponse(Entity other)
    {
        // Invert the velocity to simulate a bounce when a collision is detected
        if (CheckCollision(other))
        {
            Velocity = SplashKit.VectorInvert(Velocity);
        }
    }
}