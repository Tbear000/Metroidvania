using Godot;
using System;

public class TitleScreen : Control
{
	private string _sceneToLoad;
	public FadeIn FadeIn;
	private AudioStreamPlayer _buttonSound;
	
	public override void _Ready(){
		FadeIn = GetNode<FadeIn>("FadeIn");
		_buttonSound = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
	}
	
	private void _on_New_Game_pressed()
	{
		_buttonSound.Play();
		_sceneToLoad = "res://Src/World.tscn";
		FadeIn.PlayFade();
	}


	private void _on_OptionsButton_pressed()
	{
	}


	private void _on_ExitButton_pressed()
	{
		_buttonSound.Play();
		GetTree().Quit();
	}
	
	private void _on_FadeIn_Finished()
	{
		GetTree().ChangeScene(_sceneToLoad);
	}

}
