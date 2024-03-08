using Godot;
using System;
using System.Collections.Generic;

public partial class BattleStage : Node
{
    
    [Signal] public delegate void PanelOwnerUpdateEventHandler(BattlePlayer player);
    [Signal] public delegate void PanelStateUpdateEventHandler(BattlePanel.PanelState state);
    
    
    [Export] PackedScene playerScene;

    StageField stageField;

    private List<List<Vector3>> StagePanels;

    private List<List<Vector3>> PlayerOnePanels;
    private List<List<Vector3>> PlayerTwoPanels;
	
    // Called when the node enters the scene tree for the first time.
	public override void _Ready(){
        stageField = GetNode<StageField>("StageField");

	    int index = 0;
        foreach(var item in MultiplayerManager.Players){
            BattlePlayer currentPlayer = playerScene.Instantiate<BattlePlayer>();

            currentPlayer.Name = item.Id.ToString();
            currentPlayer.OwnedField = stageField;
            currentPlayer.equippedWeapon = item.EquippedWeapon;
            
            AddChild(currentPlayer);

            foreach(Node3D spawnPoint in GetTree().GetNodesInGroup("SpawnPoints")){
                if(int.Parse(spawnPoint.Name) == index){
                    currentPlayer.GlobalPosition = spawnPoint.GlobalPosition;
                }
            }
            index++;
       }
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta){
	}

}
