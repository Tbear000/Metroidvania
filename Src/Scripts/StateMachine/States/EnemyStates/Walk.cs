using Godot;
using System;
using System.Collections.Generic;

public class Walk : EnemyState
{
	public override void Enter(Dictionary<string, bool> message = null)
	{
		//_enemy.sprite.Play("Walk");
	}
	
	public override void PhysicsUpdate(float delta)
	{
		_enemy.MoveEnemy(delta);
		if(_enemy.raycast.IsColliding())
		{
			if(_enemy.raycast.GetCollider().IsClass("KinematicBody2D"))
				_stateMachine.TransitionTo("Attack");
			else
				_enemy.TurnAround();
		}
	}
}
