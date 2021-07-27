using Godot;
using System;
using System.Collections.Generic;

public class Hit : EnemyState
{
	public override void Enter(Dictionary<string, bool> message = null)
	{
		_enemy.sprite.Play("Hit");
	}
	
	private void _on_AnimatedSprite_animation_finished()
	{
		if(_enemy.sprite.Animation == "Hit")
			_enemy.sprite.Play("Walk");
			_stateMachine.TransitionTo("Walk");
	}

	public override void PhysicsUpdate(float delta)
	{
		
	}
}
