using Godot;
using System;

public class Enemy : KinematicBody2D
{
	[Export]
	public Vector2 Speed = new Vector2(100,100);
	[Export]
	public int Gravity = 400;
	public Vector2 Velocity = new Vector2();
	public StateMachine _stateMachine;
	public AnimatedSprite sprite;
	public RayCast2D raycast;
	[Export]
	public int MaxHealth = 5;
	public int _health;
	public Area2D AttackArea;
	public CollisionShape2D AttackCollision;
	public AudioStreamPlayer2D audio;

	private ProgressBar _Healthbar;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		SetPhysicsProcess(false);
		SetProcess(false);
		audio = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
		_stateMachine = GetNode<StateMachine>("StateMachine");
		sprite = GetNode<AnimatedSprite>("AnimatedSprite");
		raycast = GetNode<RayCast2D>("RayCast2D");
		AttackArea = GetNode<Area2D>("AttackArea");
		AttackCollision = GetNode<CollisionShape2D>("AttackArea/CollisionShape2D");
		_Healthbar = GetNode<ProgressBar>("ProgressBar");
		_Healthbar.MaxValue = MaxHealth;
		_health = MaxHealth;
		_Healthbar.Value = _health;
	}
	
	private void _on_Area2D_area_entered(object area)
	{
		_health -= 1;
		_Healthbar.Value = _health;
		audio.Play();
		if(_health <= 0)
			_stateMachine.TransitionTo("Death");
		else
			_stateMachine.TransitionTo("Hit");
	}
	
	public void MoveEnemy(float delta)
	{
		Velocity.x = Speed.x;
		Velocity.y += Speed.y * delta;
		Velocity = MoveAndSlide(Velocity);
	}
	
	public void TurnAround()
	{	
		sprite.FlipH = !sprite.FlipH;
		Speed.x *= -1;
		raycast.CastTo = raycast.CastTo * -1;
		AttackArea.Position =new Vector2(-AttackArea.Position.x, AttackArea.Position.y);
	}
}
