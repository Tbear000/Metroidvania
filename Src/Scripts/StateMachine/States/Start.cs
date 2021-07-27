using Godot;
using System;

public class Start : PlayerState
{
	public override void PhysicsUpdate(float delta)
	{
		_player.MovePlayer(delta);

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
	}
}
