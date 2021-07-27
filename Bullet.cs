using Godot;
using System;

public class Bullet : Area2D
{
	[Export]
	public int bulletSpeed = 5;
	
	private Vector2 MovePos;
	private Sprite sprite;
	private AudioStreamPlayer2D audio;
	
	public override void _Ready()
	{
		sprite = GetNode<Sprite>("Sprite");
		audio = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer");
		if(Global._facing)
		{
			sprite.FlipH = true;
			MovePos = new Vector2(-bulletSpeed, 0);
		}
		else
		{
			sprite.FlipH = false;
			MovePos = new Vector2(bulletSpeed, 0);
		}
	}
	
	public override void _PhysicsProcess(float delta)
	{
		
		Position += MovePos;
	}
	
	private void _on_AudioStreamPlayer_finished()
	{
		QueueFree();
	}

	private void _on_Bullet_body_entered(object body)
	{
		audio.Play();
		this.Visible = false;
	}
}
