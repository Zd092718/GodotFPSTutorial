using Godot;
using System;

public class Player : KinematicBody
{
    // stats
    private int currentHp = 10;
    private int maxHp = 10;
    private int ammo = 15;
    private int score = 0;

    // physics
    private float moveSpeed = 5.0f;
    private float jumpForce = 5.0f;
    private float gravity = 12.0f;

    // camera
    private float minLookAngle = -90.0f;
    private float maxLookAngle = 90.0f;
    [Export]
    private float lookSensitivity = 10.0f;

    //vectors
    private Vector3 velocity = Vector3.Zero;
    private Vector2 mouseDelta = Vector2.Zero;

    // components
    private Camera camera;
    private Spatial muzzle;

    public override void _Ready()
    {
        camera = GetNode<Camera>("Camera");
        muzzle = GetNode<Spatial>("Camera/Muzzle");

        // Hide and lock mouse cursor
        Input.MouseMode = Input.MouseModeEnum.Captured;
    }

    public override void _PhysicsProcess(float delta)
    {
        // reset x and z velocity
        velocity.x = 0;
        velocity.z = 0;

        Vector2 input = Vector2.Zero;

        // movement inputs
        if (Input.IsActionPressed("move_forward"))
        {
            input.y -= 1;
        }
        if (Input.IsActionPressed("move_back"))
        {
            input.y += 1;
        }
        if (Input.IsActionPressed("move_left"))
        {
            input.x -= 1;
        }
        if (Input.IsActionPressed("move_right"))
        {
            input.x += 1;
        }

        input = input.Normalized();

        // get forward and right directions
        Vector3 forward = GlobalTransform.basis.z;
        Vector3 right = GlobalTransform.basis.x;

        Vector3 relativeDirection = forward * input.y + right * input.x;

        //set velocity
        velocity.x = relativeDirection.x * moveSpeed;
        velocity.z = relativeDirection.z * moveSpeed;

        // apply gravity
        velocity.y -= gravity * delta;

        // move player
        velocity = MoveAndSlide(velocity, Vector3.Up);

        // jumping
        if (Input.IsActionPressed("jump") && IsOnFloor())
        {
            velocity.y = jumpForce;
        }
    }

    public override void _Process(float delta)
    {
        var newCameraRotationDegrees = camera.RotationDegrees;
        var newPlayerRotationDegrees = RotationDegrees;
        // rotate camera along x axis
        newCameraRotationDegrees.x -= mouseDelta.y * lookSensitivity * delta;

        // clamp camera x axis
        newCameraRotationDegrees.x = Mathf.Clamp(newCameraRotationDegrees.x, minLookAngle, maxLookAngle);

        // rotate the player along y axis
        newPlayerRotationDegrees.y -= mouseDelta.x * lookSensitivity * delta;

        // reassigning new rotation to degrees
        camera.RotationDegrees = newCameraRotationDegrees;
        RotationDegrees = newPlayerRotationDegrees;

        // reset mouse delta vector
        mouseDelta = Vector2.Zero;

    }
    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseMotion ev)
        {
            mouseDelta = ev.Relative;
        }
    }
}
