using Godot;
using System;
using System.Collections.Generic;

public class Attack : EnemyState
{
	public override void Enter(Dictionary<string, bool> message = null)
	{
		_enemy.sprite.Play("Attack");
	}
	
	private void _on_AnimatedSprite_animation_finished()
	{
		if(_enemy.sprite.Animation == "Attack")
		{
			_enemy.AttackCollision.Disabled = true;
			_enemy.sprite.Play("Walk");
			_stateMachine.TransitionTo("Walk");
		}
	}
	
	public override void PhysicsUpdate(float delta)
	{
		if(_enemy.sprite.Animation == "Attack" && _enemy.sprite.Frame > 2)
			_enemy.AttackCollision.Disabled = false;
	}
}
