using Godot;
using System;

public class Pickup : Area2D
{
	private void _on_Gun_Area_body_entered(object body)
	{
		Global.HasGun = true;
		QueueFree();
	}
}
