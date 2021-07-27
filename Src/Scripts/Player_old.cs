using Godot;
using System;

public class Player_old : KinematicBody2D
{
	private Vector2 _speed = new Vector2(150,400);
	private Vector2 velocity;
	private AnimatedSprite sprite;
	private Area2D punchArea;
	private CollisionShape2D punch;
	private PlayerState _state;
	
	public enum PlayerState
	{
		Walk,
		Attack,
		Jump,
		Idle
	}
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		velocity = new Vector2();
		sprite = GetNode<AnimatedSprite>("Sprite");
		punchArea = GetNode<Area2D>("PunchArea");
		punch = punchArea.GetNode<CollisionShape2D>("CollisionShape2D");
	}
	
	private void _on_Sprite_animation_finished()
	{
		if(sprite.Animation == "Attack"){
			punch.Disabled = true;
			_state = PlayerState.Idle;
		}
	}
	
  // Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _PhysicsProcess(float delta)
  {
	var _input = (Input.GetActionStrength("ui_right")-Input.GetActionStrength("ui_left"));
	switch(_state)
	{
		case PlayerState.Walk:
			if(_input != 0)
			{
				velocity.x += _input * 10;
				if(IsOnFloor())
					sprite.Play("Walk");
			}
			if(velocity.x > 0)
				sprite.FlipH = false;
			else if(velocity.x < 0)
				sprite.FlipH = true;
				
			if(_input < 0)
				punchArea.Scale = new Vector2(-1, 1);
			else if(_input > 0)
				punchArea.Scale = new Vector2(1, 1);
				
			if(Input.IsActionPressed("ui_up") && IsOnFloor())
				_state = PlayerState.Jump;
			else if(Input.IsActionJustPressed("ui_accept"))
				_state = PlayerState.Attack;
			else if(_input == 0 && IsOnFloor())
				_state = PlayerState.Idle;
			else
				_state = PlayerState.Walk;
			break;
			
		case PlayerState.Attack:
			velocity.x = 0;
			sprite.Play("Attack");
			punch.Disabled = false;
			break;
			
		case PlayerState.Jump:
			sprite.Play("Jump");
			velocity.y -= _speed.y;
			if(IsOnFloor() && _input == 0)
				_state = PlayerState.Idle;
			else if(IsOnFloor() && _input !=0)
				_state = PlayerState.Walk;
			else
				_state = PlayerState.Jump;
			break;
			
		case PlayerState.Idle:
			velocity.x = 0;
			sprite.Play("Idle");
			if(Input.IsActionPressed("ui_up") && IsOnFloor())
				_state = PlayerState.Jump;
			else if(Input.IsActionJustPressed("ui_accept"))
				_state = PlayerState.Attack;
			else if(_input != 0)
				_state = PlayerState.Walk;
			else
				_state = PlayerState.Idle;
			
			break;
			
		default:
			break;
	}
	velocity.y += 20;
	velocity.x = Mathf.Clamp(velocity.x, -_speed.x, _speed.x);
	velocity = MoveAndSlide(velocity, Vector2.Up);
  }
}
