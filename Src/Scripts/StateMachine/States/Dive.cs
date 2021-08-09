using Godot;
using System;
using System.Collections.Generic;

public class Dive : PlayerState
{
	private bool doDive;
	public override void Enter(Dictionary<string, bool> message = null)
	{
		_player.sprite.Play("Attack");
		_player.sprite.SpeedScale = 1.5f;
		if(_player.Velocity.y < 0)
			_player.Velocity.y = 0;
		doDive = _player.inputDirectionX != 0;
	}
		
	private void _on_Sprite_animation_finished()
	{
		if(_player.sprite.Animation == "Attack"){
			_player.punchCollision.Disabled = true;
			_stateMachine.TransitionTo("Air");
		}
	}
	
	public override void PhysicsUpdate(float delta)
	{
		if(doDive)
			DivePlayer(delta);
		else
			_player.MovePlayer(delta);
		_player.punchCollision.Disabled = false;
		if(_player.IsOnFloor()){
			_player.punchCollision.Disabled = true;
			_stateMachine.TransitionTo("Idle");
		}
		else if(_player.IsOnWall()){
			_player.punchCollision.Disabled = true;
			_stateMachine.TransitionTo("WallSlide");
		}
	}
	
	public void DivePlayer(float delta)
	{
		_player.Velocity.x = _player.Speed.x * _player.inputDirectionX * 2;
		_player.Velocity.y += _player.Gravity * delta / 2;
		_player.Velocity = _player.MoveAndSlide(_player.Velocity, Vector2.Up);
	}
}
