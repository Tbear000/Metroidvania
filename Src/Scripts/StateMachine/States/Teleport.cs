using Godot;
using System;
using System.Collections.Generic;

public class Teleport : PlayerState
{
	public float TeleportDistance;
	
	public override void Enter(Dictionary<string, bool> message = null)
	{
		TeleportDistance = _player.TeleportCast.CastTo.x;
		if(_player.TeleportCast != null)
			_player.TeleportCast.Enabled = true;
		else
			_stateMachine.TransitionTo("Idle");
	}
	
  	public override void PhysicsUpdate(float delta)
	{
		if(!_player.TeleportCast.IsColliding()){
			_player.Position = new Vector2(_player.Position.x + TeleportDistance, _player.Position.y);
			_player.Velocity.x = 0;
		}
		_player.TeleportCast.Enabled = false;
		if (_player.IsOnFloor())
			_stateMachine.TransitionTo("Idle");
		else
			_stateMachine.TransitionTo("Air");
  	}
}
