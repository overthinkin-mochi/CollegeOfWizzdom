using Godot;
using System;
using System.Collections.Generic;

public partial class StageField : Node3D
{
	
	public List<List<Vector3>> childrenPositions;
	public List<List<String>> childrenNames;
    
    

    public Node3D[][] fieldPanels;

	public override void _Ready(){
		

		// Inicializacion de parametros
		childrenPositions = new List<List<Vector3>>();
		childrenNames = new List<List<String>>();
            
		// Se guardan en los arreglos el nombre y posicion global del nodo
		foreach (Node3D child in GetChildren()){

			if(child.Name != "SpawnPoint"){
				GetRowArray(child, childrenPositions, childrenNames);
			}
	       
        }
	}
	


	/// <summary>
	/// Devuelve un arreglo de arreglos con las posiciones globales de los hijos de un nodo
	/// </summary>
	public void GetRowArray(Node3D row,  List<List<Vector3>> positions, List<List<String>> names){
		
		List<Vector3> auxPositionList= new List<Vector3>(); 
		List<String> auxNameList= new List<String>(); 
        
		foreach (Node3D child in row.GetChildren()){
			auxPositionList.Add(child.GlobalPosition);
			auxNameList.Add(child.Name);
                    
		}
        
		positions.Add(auxPositionList);
		names.Add(auxNameList);
    }

    public void OwnerUpdate(int row, int column, BattlePlayer player){
        
    }

    public void StateUpdate(int row, int column, BattlePanel.PanelState state){
    }
}
