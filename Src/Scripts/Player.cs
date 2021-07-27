using Godot;
using System;

public class Player : KinematicBody2D
{
	[Export]
	public Vector2 Speed = new Vector2(100,100);
	[Export]
	public int Gravity = 400;
	public Vector2 Velocity = new Vector2();
	public StateMachine _stateMachine;
	public AnimatedSprite sprite;
	public float inputDirectionX;
	public CollisionShape2D punchCollision;
	public RayCast2D TeleportCast;
	private int _maxWallJumps;
	public int WallJumps = 1;
	public AudioStreamPlayer2D audio;
	[Export]
	private AudioStream _hurtSound;
	public Node2D muzzle;

	public override void _Ready()
	{
		_stateMachine = GetNode<StateMachine>("StateMachine");
		sprite = GetNode<AnimatedSprite>("Sprite");
		punchCollision = GetNode<CollisionShape2D>("PunchArea/CollisionShape2D");
		TeleportCast = GetNode<RayCast2D>("RayCast2D");
		audio = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");
		muzzle = GetNode<Node2D>("Muzzle");
		_maxWallJumps = 1;
		this.GlobalPosition = Global.PlayerInitialMapPosition;
	}
	
	private void _on_HitArea_area_entered(object area)
	{
		audio.Stream = _hurtSound;
		audio.Play();
		Global.PlayerHealth -= 1;
		GD.Print(Global.PlayerHealth);
		if(Global.PlayerHealth <= 0)
			Start(new Vector2(130,-52));
	}
	
	public override void _Process(float delta)
	{
		if(IsOnFloor())
			WallJumps = _maxWallJumps;
	}

	
	public void Start(Vector2 pos)		//Reset Player Pos.
	{
		Position = pos;
		Velocity = Vector2.Zero;
	}
	
	public void MovePlayer(float delta)
	{
		inputDirectionX = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
		if(inputDirectionX > 0) 
			sprite.FlipH = false;
		else if(inputDirectionX < 0)
			sprite.FlipH = true;

		MovePunch(sprite.FlipH);
		Velocity.x = Speed.x * inputDirectionX;
		Velocity.y += Gravity * delta;
		Velocity = MoveAndSlide(Velocity, Vector2.Up);
	}
	
	void MovePunch(bool flipped)
	{
		if(!flipped)
		{
			punchCollision.Position = new Vector2(11,-3);
			TeleportCast.CastTo = new Vector2(80,0);
			muzzle.RotationDegrees = 0;
			muzzle.Position = new Vector2(4,0);
		}
		else
		{
			punchCollision.Position = new Vector2(-11,-3);
			TeleportCast.CastTo = new Vector2(-80,0);
			muzzle.RotationDegrees = 180;
			muzzle.Position = new Vector2(-4,0);
		}
		Global._facing = flipped;
	}
}
