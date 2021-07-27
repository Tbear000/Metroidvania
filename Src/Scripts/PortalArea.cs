using Godot;
using System;

public class PortalArea : Area2D
{
	[Export]
	public String NextScenePath = "";
	[Export]
	public Vector2 PlayerSpawnLocation = Vector2.Zero;
	
	public override void _Ready()
	{
		if (NextScenePath.GetFile() == null)
		{
			throw new InvalidProgramException("Next Scene is null in the Portal area check.");
		}
	}
	
	private void _on_PortalArea_body_entered(object body)
	{
		Global.PlayerInitialMapPosition = PlayerSpawnLocation;
		GetTree().ChangeScene(NextScenePath);
	}

}
