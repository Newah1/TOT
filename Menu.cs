using Godot;
using System;
using TheOregonTrailAI.Models;

public partial class Menu : Control
{
	// Called when the node enters the scene tree for the first time.
	public override async void _Ready()
	{
		GetViewport().Connect("size_changed", new Callable(this, "_OnWindowResized"));
		
		var response =
			await Provider.AiService.GetResponseSimple("Generate a new title for a game based on the Oregon trail that's under 30 characters without quotation marks");

		var title = GetNode<RichTextLabel>("Title");
		title.Text = response.Response.Replace("\"", "");
		Center();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

	private void Center()
	{
		var xPosition = (GetViewportRect().Size.X - Size.X) / 2;
		Position = new Vector2(xPosition, Position.Y);
	}
	
	private void _on_button_pressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/game.tscn");
	}
	private void _on_exit_pressed()
	{
		GetTree().Quit();
	}
	private void _OnWindowResized() 
	{
		Center();
	}


}
