using Godot;
using System;

public partial class Card : Node2D
{
    [Export] public CardData CardData;

    private Label _cardName;
    private Label _cardDescription;
    private Label _cardDamage;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

        _cardName = GetNode<Label>("CardFront/CardImage/Nombre"); 
        _cardDescription = GetNode<Label>("CardFront/CardText/Descripcion");
        _cardDamage = GetNode<Label>("CardFront/CardBorder/Daño");
        
        GD.Print(_cardName.Text);
        
        _cardName.Text = (CardData.CardName);
        _cardDescription.Text = CardData.CardDescription;
        _cardDamage.Text = CardData.CardDamage.ToString();
            
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
