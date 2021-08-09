using Godot;
using System;
using System.Collections.Generic;

public class Punch : PlayerState
{
	public override void Enter(Dictionary<string, bool> message = null)
	{
		_player.sprite.Play("Attack");
		_player.punchCollision.Disabled = false;
	}
		
	private void _on_Sprite_animation_finished()
	{
		if(_player.sprite.Animation == "Attack"){
			_player.punchCollision.Disabled = true;
			_stateMachine.TransitionTo("Idle");
		}
	}
}
