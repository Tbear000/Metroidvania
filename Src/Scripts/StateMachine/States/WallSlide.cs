using Godot;
using System;
using System.Collections.Generic;

public class WallSlide : PlayerState
{
	public override void Enter(Dictionary<string, bool> message = null)
	{
		_player.sprite.Play("Idle");
	}
	
  	public override void PhysicsUpdate(float delta)
	{
		if(_player.IsOnWall())
		{
			_player.Velocity.y = Mathf.Min(_player.Velocity.y, 30);
			_player.MovePlayer(delta);
			if (Input.IsActionJustPressed("ui_up") && _player.WallJumps > 0)
			{
				_player.WallJumps -= 1;
				var message = new Dictionary<string, bool>()
				{
					{ "doJump", true }
				};
				_stateMachine.TransitionTo("Air", message);
			}
		}
		else if (!_player.IsOnFloor())
		{
			_stateMachine.TransitionTo("Air");
			return;
		}
		if(_player.IsOnFloor())
			_stateMachine.TransitionTo("Idle");
  	}
}
