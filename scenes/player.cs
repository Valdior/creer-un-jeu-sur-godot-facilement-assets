using Godot;
using System;

public partial class player : Area2D
{
	[Export]
	public int _speed = 400;
	[Signal]
	public delegate void HitEventHandler();
	Vector2 screenSize;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		screenSize = GetViewportRect().Size;
		Hide();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector2 velocity = Vector2.Zero;
		if (Input.IsActionPressed("move_right"))
			velocity.X += 1;
		if (Input.IsActionPressed("move_left"))
			velocity.X -= 1;
		if (Input.IsActionPressed("move_up"))
			velocity.Y -= 1;
		if (Input.IsActionPressed("move_down"))
			velocity.Y += 1;

		if (velocity.Length() > 0) {
			velocity = velocity.Normalized() * _speed;
			GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play();
		}
		else {
			GetNode<AnimatedSprite2D>("AnimatedSprite2D").Stop();
		}

		Position += velocity * (float)delta;
		Position = Position.Clamp(Vector2.Zero, screenSize);

		if (velocity.X != 0) {
			GetNode<AnimatedSprite2D>("AnimatedSprite2D").Animation = "walk";
			GetNode<AnimatedSprite2D>("AnimatedSprite2D").FlipH = velocity.X < 0;
		}
		else if (velocity.Y != 0) {
			GetNode<AnimatedSprite2D>("AnimatedSprite2D").Animation = "up";
		}
	}

	public void OnBodyEntered(Node2D body) {
		Hide();
		EmitSignal(SignalName.Hit);
		GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred("disabled", true);
	}

	public void Start(Vector2 pos)
	{
		Position = pos;
		Show();
		GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
	}
}
