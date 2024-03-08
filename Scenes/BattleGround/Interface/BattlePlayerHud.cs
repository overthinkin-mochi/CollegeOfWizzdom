using Godot;
using System;

public partial class BattlePlayerHud : Control
{
    [Export] public BattlePlayer player {get; set;}

    [Export] public int playerNumber {get; set;}
    
    private Sprite2D mugshot;
    private Sprite2D border;
    private TextureProgressBar healthBar;
    private TextureProgressBar manaBar;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
        
        mugshot = GetNode<Sprite2D>("Mugshot/MugshotSprite");
        border = GetNode<Sprite2D>("Mugshot/MugshotBorder");
        healthBar = GetNode<TextureProgressBar>("Bars/HpBar");
        manaBar = GetNode<TextureProgressBar>("Bars/ManaBar");
         
        player.HealthUpdate += updateHealth;
        player.ManaUpdate += updateMana;
        
        updateHealth(player.CurrentHealth);
        updateMana(player.CurrentMana);

        if(playerNumber == 2){
            this.GlobalPosition = new Vector2(1152,0);
            this.Scale = new Vector2(-1.7f,1.7f);
        }
	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
    
    public void updateHealth(int health){
        healthBar.Value = player.CurrentHealth * 100 / player.MaxHealth;
    }

    public void updateMana(int mana){
        manaBar.Value = player.CurrentMana * 100 / player.MaxMana;
    }

}
