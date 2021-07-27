using Godot;
using System;
using System.Collections.Generic;

public class Shoot : PlayerState
{	
	[Export]
	public AudioStream _shootSound;
	
	public override void Enter(Dictionary<string, bool> message = null)
	{
		var Bullet = GD.Load<PackedScene>("res://Src/Bullet.tscn");
		Area2D _bullet = (Area2D)Bullet.Instance();
		_bullet.Transform = _player.punchCollision.GlobalTransform;
		_player.GetParent().AddChild(_bullet);
		_player.audio.Stream = _shootSound;
		_player.audio.Play();
	}
	
	public override void PhysicsUpdate(float delta)
	{
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
