using Godot;
using System.Collections.Generic;

public class Run : PlayerState
{
	public override void Enter(Dictionary<string, bool> message = null)
	{
		_player.sprite.Play("Walk");
	}
	
	public override void PhysicsUpdate(float delta)
	{
		if (!_player.IsOnFloor())
		{
			_stateMachine.TransitionTo("Air");
		}
		
		_player.MovePlayer(delta);

		if (Input.IsActionJustPressed("ui_up"))
		{
			var message = new Dictionary<string, bool>()
			{
				{ "doJump", true}
			};
			_stateMachine.TransitionTo("Air", message);
		}
		else if (Godot.Mathf.IsEqualApprox(_player.inputDirectionX, 0.0f))
		{
			_stateMachine.TransitionTo("Idle");
		}
		else if(Input.IsActionJustPressed("ui_accept"))
		{
			if(Global.HasGun)
				_stateMachine.TransitionTo("Shoot");
			else
				_stateMachine.TransitionTo("Punch");
		}
		else if(Input.IsActionJustPressed("ui_down"))
			_stateMachine.TransitionTo("Teleport");
	}
}
