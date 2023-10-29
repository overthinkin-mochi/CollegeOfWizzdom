using Godot;
using System;
using System.Collections.Generic;

public partial class BattlePlayer : CharacterBody3D
{
	#region SIGNALS

	[Signal] public delegate void HealthUpdateEventHandler(int currentHealth);
    [Signal] public delegate void ManaUpdateEventHandler(int currentMana);
	[Signal] public delegate void ConsumeCardEventHandler(CardData card);
	[Signal] public delegate void DestroyCardEventHandler(CardData card);

	#endregion

	#region VARIABLES

	[Export] public float MoveSpeed {get; set;}
    [Export] public int MaxHealth {get; set;}
	[Export] public int CurrentHealth {get; set;}
	[Export] public int MaxMana {get; set;}
    [Export] public int CurrentMana {get; set;}
	[Export] public int attack {get; set;}
	

	[Export] public StageField OwnedField;
	
    [Export(PropertyHint.Range, "0,2,")] public int CurrentRow; 
	[Export(PropertyHint.Range, "0,5,")] public int CurrentColumn;
	
	#endregion
	
	#region BATTLEFIELD VARIABLES

	[Export] public WeaponData equippedWeapon;
	[Export] public CardCollection deck;
	
	private CardCollection hand;
	private CardCollection discardPile;

	private List<List<Vector3>> OwnedPanels;
	private AnimationPlayer animationPlayer;
    private RayCast3D bulletSpawn;

	private Vector3 CurrentPanel;

	#endregion

	#region EVENTS 

	private bool _canMove;
	private bool _canAttack;
	private bool _isAttacking;
	private bool _isMoving;

	#endregion

	private Tween tween;


	public override void _Ready(){

		_isMoving = false;
		_canMove = true; 
        
		OwnedPanels = OwnedField.childrenPositions;
		CurrentPanel = OwnedPanels[CurrentRow][CurrentColumn];
	
		this.GlobalPosition = CurrentPanel;
        
        bulletSpawn = GetNode<RayCast3D>("RayCast3D");
		animationPlayer = GetNode<AnimationPlayer>("Visuals/CharacterSprite/AnimationPlayer");
		animationPlayer.Play("Idle");

        EmitSignal(SignalName.HealthUpdate, CurrentHealth);
	}

	public override void _PhysicsProcess(double delta){
		
		if(Input.IsActionPressed("Right") && !_isMoving){
			animationPlayer.Play("Front_Dash");
			MoveToPanel(CurrentRow,(CurrentColumn - 1),  MoveSpeed);

		}
		if(Input.IsActionPressed("Left") && !_isMoving){
			animationPlayer.Play("Back_Dash");
			MoveToPanel(CurrentRow,(CurrentColumn + 1),  MoveSpeed);
			
		}
		if(Input.IsActionPressed("Up") && !_isMoving){
			animationPlayer.Play("Back_Dash");
			MoveToPanel((CurrentRow + 1), CurrentColumn,  MoveSpeed);
			
		}
		if(Input.IsActionPressed("Down") && !_isMoving){
			animationPlayer.Play("Front_Dash");
			MoveToPanel((CurrentRow - 1), CurrentColumn,  MoveSpeed);

		}
		if (Input.IsActionPressed("Attack") && !_isAttacking && !_isMoving){
			animationPlayer.Play("Basic_Attack");
			MainAttack(equippedWeapon);
			OnActionFinished(0.5f - (0.05f* (float)equippedWeapon.Speed));
		}
		else{
			return;
		}
		
	}

	///<summary>
	/// Mueve al jugador a la posicion entregada si es valida
	/// Sino corrige los indices
	/// </summary>
	public void MoveToPanel(int row, int column, float speed ){

		_isMoving = true;

		if (row < 0 || row > 2){
			animationPlayer.Play("Idle");
			_isMoving = false;
					
		}

		else if (column > 5 || column < 0){
			animationPlayer.Play("Idle");
			_isMoving = false;
		}

		else{
			Vector3 nextPanel = OwnedPanels[row][column];

			tween = CreateTween();
			tween.TweenProperty(this, "global_transform:origin", nextPanel, speed);
			tween.Finished += OnMovementFinished;
		
			CurrentRow = row;
			CurrentColumn = column;
			CurrentPanel = nextPanel;
		}

	}
	///<summary>
	/// Recibe daño y emite la señal HealthUpdate
	///</summary>
	public void GetDamage(int damage){
		CurrentHealth -= damage;
		EmitSignal(SignalName.HealthUpdate, CurrentHealth);
	}

    public void SpendMana(int mana){
        CurrentMana -= mana;
    }
	
	///<summary>
	/// Ejecuta la accion principal del arma equipada
	/// </summary>
	public void MainAttack(WeaponData weapondata){
        _isAttacking = true;
		switch(weapondata.weaponType){
			case WeaponData.WeaponType.MELEE:
            MeleeAttack();
			GD.Print("Melee attack");
			break;
			case WeaponData.WeaponType.THROWABLE:
			GD.Print("Throwable attack");
			break;
			case WeaponData.WeaponType.RANGED:
			GD.Print("Ranged attack");
			break;
			case WeaponData.WeaponType.MAGIC:
			GD.Print("Magic attack");
			break;
		}

	}
	
    public void MeleeAttack(){
        if (equippedWeapon.Projectile != null){  
            Node3D bullet = equippedWeapon.Projectile.Instantiate<Node3D>();
            bullet.RotationDegrees = bulletSpawn.TargetPosition;
            bullet.GlobalPosition = bulletSpawn.GlobalPosition;
            GetTree().Root.AddChild(bullet);
            
        }
    }

	public void UseCard(CardData card){
	}
	
	public void BreakCard(CardData card){
	}
	
	public async void OnActionFinished(float delay){
		GD.Print("action finished");
		await ToSignal(GetTree().CreateTimer(delay),"timeout");

		animationPlayer.Play("Idle");
		_isAttacking = false;
			
	}

	public async void OnMovementFinished(){
		
		animationPlayer.Play("Idle");
		await ToSignal(GetTree().CreateTimer(0.05),"timeout");

		_isMoving = false;
	}
	
}
