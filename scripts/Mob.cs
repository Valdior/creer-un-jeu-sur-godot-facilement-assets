using Godot;
using System;

public partial class Mob : RigidBody2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		string[] mobNames = GetNode<AnimatedSprite2D>("AnimatedSprite2D").SpriteFrames.GetAnimationNames();
		GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play(mobNames[GD.Randi() % mobNames.Length]);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnVisibleOnScreenNotifier2DScreenExited() {
		QueueFree();
	}
}
