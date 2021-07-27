using Godot;
using System;

public class FadeIn : ColorRect
{
	[Signal]
	public delegate void Finished();
	
	public void PlayFade()
	{
		var anim = GetNode<AnimationPlayer>("AnimationPlayer");
		this.Show();
		anim.Play("FadeIn");
	}
	
	private void _on_AnimationPlayer_animation_finished(String anim_name)
	{
		EmitSignal("Finished");
	}

}
