using Godot;
using System;
using System.Collections.Generic;

public class Idle : PlayerState
{
	public override void Enter(Dictionary<string, bool> message = null)
	{
		_player.sprite.Play("Idle");
		_player.Velocity = Vector2.Zero;
	}

	public override void PhysicsUpdate(float delta)
	{
		_player.MovePlayer(delta);
		if (!_player.IsOnFloor())
		{
			_stateMachine.TransitionTo("Air");
			return;
		}

		if (Input.IsActionJustPressed("ui_up"))
		{
			// As we'll only have one air state for both jump and fall, we use the `msg` dictionary 
			// to tell the next state that we want to jump.
			var message = new Dictionary<string, bool>()
			{
				{ "doJump", true }
			};
			_stateMachine.TransitionTo("Air", message);
		}
		else if (Input.IsActionPressed("ui_left") || Input.IsActionPressed("ui_right"))
		{
			_stateMachine.TransitionTo("Run");
		}
		else if (Input.IsActionJustPressed("ui_accept"))
		{
			if(Global.HasGun)
				_stateMachine.TransitionTo("Shoot");
			else
				_stateMachine.TransitionTo("Punch");
		}
	}
}
