using Godot;
using System;

public class EnemyStart : EnemyState
{
	public override void PhysicsUpdate(float delta)
	{
		_enemy.MoveEnemy(delta);
		if(_enemy.IsOnFloor())
			_stateMachine.TransitionTo("Walk");
	}
}
