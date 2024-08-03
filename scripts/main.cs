
using Godot;
using System;

public partial class main : Node
{
	[Export] PackedScene mobScene;
	public int score;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.NewGame();
	}

	public void NewGame() {
		score = 0;
		Vector2 test = GetNode<Marker2D>("StartPosition").Position;
		GetNode<player>("Player").Start(test);
		GetNode<Timer>("StartTimer").Start();
	}

	public void GameOver() {
		GetNode<Timer>("ScoreTimer").Stop();
		GetNode<Timer>("MobTimer").Stop();
	}

	public void OnStartTimerTimeout() {
		GetNode<Timer>("MobTimer").Start();
		GetNode<Timer>("ScoreTimer").Start();

	}

	public void OnScoreTimerTimeout() {
		score += 1;
	}

	public void OnMobTimerTimeout() {
		Mob mob = mobScene.Instantiate<Mob>();
		var mobSpawnLocation = GetNode<PathFollow2D>("MobPath/MobSpawnLocation");
		mobSpawnLocation.ProgressRatio = GD.Randf();

		var direction = mobSpawnLocation.Rotation + Mathf.Pi / 2;
		mob.Position = mobSpawnLocation.Position;

		Vector2 velocity = new Vector2(GD.RandRange(150,250), 0);
		mob.LinearVelocity = velocity.Rotated(direction);

		AddChild(mob);
	}
}
