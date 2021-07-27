using Godot;
using System;
using System.Collections.Generic;

public class Death : EnemyState
{
	public override void Enter(Dictionary<string, bool> message = null)
	{
		var collision = _enemy.GetNode<CollisionShape2D>("CollisionShape2D");
		if(collision != null)
			collision.QueueFree();
		_enemy.sprite.Play("Death");
	}
	
	private void _on_AnimatedSprite_animation_finished()
	{
		if(_enemy.sprite.Animation == "Death")
			_enemy.QueueFree();
	}
}



