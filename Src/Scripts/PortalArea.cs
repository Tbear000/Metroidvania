using Godot;
using System;

public class PortalArea : Area2D
{
	[Export]
	public String NextScenePath = "";
	[Export]
	public Vector2 PlayerSpawnLocation = Vector2.Zero;
	private AnimationPlayer _UiArrow;
	private bool _overlap;
	
	public override void _Ready()
	{
		_overlap = false;
		_UiArrow = GetNode<AnimationPlayer>("AnimationPlayer");
		if (NextScenePath.GetFile() == null)
		{
			throw new InvalidProgramException("Next Scene is null in the Portal area check.");
		}
	}
	
	private void _on_PortalArea_body_entered(object body)
	{
		_UiArrow.Play("Active");
		_overlap = true;
	}

	private void _on_PortalArea_body_exited(object body)
	{
		_UiArrow.Play("Disabled");
		_overlap = false;
	}

    public override void _Process(float delta)
    {
        if(Input.IsActionJustPressed("ui_up") && _overlap){
			Global.PlayerInitialMapPosition = PlayerSpawnLocation;
			GetTree().ChangeScene(NextScenePath);
		}
    }

}
