using Godot;
using System;

public class Global : Node
{
	public static int PlayerHealth;
	public static int PlayerMaxHealth = 4;
	public static Vector2 PlayerInitialMapPosition = new Vector2(130,-50);
	public static bool _facing = false;
	public static bool HasGun = false;

	public static int WallJumps;
	public static int MaxWallJumps = 0;
	
	public override void _Ready()
	{
		PlayerHealth = PlayerMaxHealth;
	}
}
