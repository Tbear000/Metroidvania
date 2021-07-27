using Godot;
using System;

public class Global : Node
{
	public static int PlayerHealth;
	public int PlayerMaxHeath = 4;
	public static Vector2 PlayerInitialMapPosition = new Vector2(130,-50);
	public static bool _facing = false;
	public static bool HasGun = false;
	
	public override void _Ready()
	{
		PlayerHealth = PlayerMaxHeath;
	}
}
