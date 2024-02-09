using Godot;
using System;

public class Bullet : Area
{
	private float speed = 30.0f;
	private int damage = 1;
	private bool hitSomething = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	public override void _Process(float delta)
	{
		Translation += GlobalTransform.basis.z * speed * delta;
	}

	public void Destroy()
	{
		QueueFree();
	}

	private void OnBulletBodyEntered(object body)
	{
		// if(body is Enemy e){
		// 	e.TakeDamage(damage);
		// 	Destroy();
		// }
	}

}



