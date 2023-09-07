using Godot;
using System;

public partial class Title : RichTextLabel
{
	private int _speed = 400;
	private float _angularSpeed = Mathf.Pi;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("Test");
		
		
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
