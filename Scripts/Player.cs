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
    }

    public override void _PhysicsProcess(float delta)
    {
        base._PhysicsProcess(delta);
    }

}
