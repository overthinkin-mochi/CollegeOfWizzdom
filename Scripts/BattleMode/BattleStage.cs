using Godot;
using System;
using System.Collections.Generic;

public partial class BattleStage : Node
{
    
    [Signal] public delegate void PanelOwnerUpdateEventHandler(BattlePlayer player);
    [Signal] public delegate void PanelStateUpdateEventHandler(BattlePanel.PanelState state);
    
    
    private List<List<Vector3>> StagePanels;

    private List<List<Vector3>> PlayerOnePanels;
    private List<List<Vector3>> PlayerTwoPanels;
	
    // Called when the node enters the scene tree for the first time.
	public override void _Ready(){
	   
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta){
	}

}
