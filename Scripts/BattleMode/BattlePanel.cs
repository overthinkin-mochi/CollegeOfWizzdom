using Godot;
using System;

public partial class BattlePanel: Node3D
{
	public enum PanelState {Normal, Roto, Envenenado}

	//[Export]
	//private BattlePlayer owner; 

	private PanelState currentState;
    private BattlePlayer owner;
                
	private bool _canWalkIn;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
		
		//owner = GetNode<BattlePlayer>("Player");
		currentState = PanelState.Normal;
		_canWalkIn = true;
	}
	
	public void CambioEstado(PanelState nuevoEstado){
		
		currentState = nuevoEstado; 
		
		switch(currentState){
			case PanelState.Normal:
				_canWalkIn = true;
				break;
			case PanelState.Roto:
				_canWalkIn = false;
				break;
			case PanelState.Envenenado:
				_canWalkIn = true;
				break;
		}
	}
	public void SetOwner(BattlePlayer nuevoDueño){
	
	    owner = nuevoDueño;
	}

}
