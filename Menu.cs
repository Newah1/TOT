using Godot;
using System;

public partial class Menu : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Center();
	}

	private void Center()
	{
		var xPosition = (GetViewportRect().Size.X - Size.X) / 2;
		Position = new Vector2(xPosition, Position.Y);
	}
}
