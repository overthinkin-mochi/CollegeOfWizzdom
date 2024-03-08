using Godot;
using System;

public partial class MultiplayerController : Control
{
    // Se declaran los parametros de conexion
    [Export] private int port = 8910;
    [Export] private string adress = "127.0.0.1";
    
    // Se inicializa el peer
    private ENetMultiplayerPeer peer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
        Multiplayer.PeerConnected += PeerConnected;
        Multiplayer.PeerDisconnected += PeerDisconnected;
        Multiplayer.ConnectedToServer += ConnectedToServer;
        Multiplayer.ConnectionFailed += ConnectionFailed;
            
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta){
	}

    /// <summary>
    /// corre en todas las instancias cuando un jugador se connecta
    /// </summary>
    public void  PeerConnected(long id){
        GD.Print("PLAYER CONNECTED!, ID: "+ id.ToString());
    }

    /// <summary>
    /// corre en todas las instancias cuando un jugador se desconecta
    /// </summary>
    public void PeerDisconnected(long id){
        GD.Print("PLAYER DISCONNECTED!, ID: "+ id.ToString());
    }

    /// <summary>
    /// corre en el cliente cuando la conexion es exitosa
    /// </summary>
    public void ConnectedToServer(){

        GD.Print("CONNECTED TO SERVER");
        RpcId(1,"sendPlayerInformation", GetNode<LineEdit>("LineEdit").Text, Multiplayer.GetUniqueId());
    }
    
    /// <summary>
    /// corre en el cliente cuando la conexion falla
    /// </summary>
    public void ConnectionFailed(){
        GD.Print("CONNECTION FAILED");
    }


    ///--| SEÑALES DE BOTONES |--------------------------------------------------------------------|


    /// <summary>
    /// corre en el servidor cuando un cliente se conecta
    /// </summary> 
    public void _on_host_button_down(){
        peer = new ENetMultiplayerPeer();
        var error = peer.CreateServer(port,2);
        if (error != Error.Ok){
            GD.Print("ERROR: "+ error.ToString());
            return;
        }
        peer.Host.Compress(ENetConnection.CompressionMode.RangeCoder);
        Multiplayer.MultiplayerPeer = peer;
        GD.Print("ESPERANDO JUGADORES");
        sendPlayerInformation(GetNode<LineEdit>("LineEdit").Text, 1);
            
    }
    

    /// <summary>
    ///  corre en el cliente cuando se une a un servidor
    ///  </summary>
    public void _on_join_button_down(){
        peer = new ENetMultiplayerPeer();
        peer.CreateClient(adress,port);

        peer.Host.Compress(ENetConnection.CompressionMode.RangeCoder);
        Multiplayer.MultiplayerPeer = peer;
        GD.Print("UNIENDOSE A PARTIDA");
    }

    public void _on_start_game_button_down(){
        Rpc("startGame");
        startGame();
    }


    ///--| FUNCIONES DE BOTONES |--------------------------------------------------------------------|


    /// <summary>
    /// Inicia la partida en el servidor
    /// </summary>
    [Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = true, TransferMode = MultiplayerPeer.TransferModeEnum.Reliable)]
    private void startGame(){
        foreach(var item in MultiplayerManager.Players){
            GD.Print(item.Name + " is playing");
        }

        var scene = ResourceLoader.Load<PackedScene>("res://Scenes/BattleGround/BattleStage.tscn").Instantiate<BattleStage>();
        GetTree().Root.AddChild(scene);
        this.Hide();
    }
    

    /// <summary>
    /// Envia la informacion del jugador a todos los jugadores
    /// </summary>
    [Rpc(MultiplayerApi.RpcMode.AnyPeer)]
    private void sendPlayerInformation(string name, int id ){
        PlayerInfo playerInfo = new PlayerInfo(){
            Name = name,
            Id = id
        };

        if (!MultiplayerManager.Players.Contains(playerInfo)){
            MultiplayerManager.Players.Add(playerInfo);
        }
        if ( Multiplayer.IsServer()){
            foreach(var item in MultiplayerManager.Players){
                Rpc("sendPlayerInformation", name ,id);
            }
        }

    }

}
