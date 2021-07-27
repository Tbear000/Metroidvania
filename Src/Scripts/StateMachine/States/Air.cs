using Godot;
using System;
using System.Collections.Generic;

public class Air : PlayerState
{
	[Export]
	public AudioStream _jumpSound;
	
	/// If we get a message asking us to jump, we jump.
	public override void Enter(Dictionary<string, bool> message = null)
	{
		if (message != null &&
			message.ContainsKey("doJump") &&
			message["doJump"] == true)
		{
			_player.Velocity.y = -_player.Speed.y;
			_player.audio.Stream = _jumpSound;
			_player.audio.Play();
		}
		_player.sprite.Play("Jump");
	}

	public override void PhysicsUpdate(float delta)
	{
		_player.MovePlayer(delta);
		if(Input.IsActionJustPressed("ui_accept"))
			_stateMachine.TransitionTo("Punch");
			
		if(_player.IsOnWall())
			_stateMachine.TransitionTo("WallSlide");

		// Landing
		if (_player.IsOnFloor())
		{
			if (Godot.Mathf.IsEqualApprox(_player.Velocity.x, 0.0f))
			{
				_stateMachine.TransitionTo("Idle");
			}
			else
			{
				_stateMachine.TransitionTo("Run");
			}
		}
		else 
		{
			if(Input.IsActionJustPressed("ui_accept"))
				_stateMachine.TransitionTo("Dive");
			else if (Input.IsActionJustPressed("ui_down") && _player.inputDirectionX != 0)
				_stateMachine.TransitionTo("Teleport");
		}
	}
}
