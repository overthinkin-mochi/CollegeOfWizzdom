extends Node



func _ready():
	var peer = NetworkedMultiplayerENet.new()
	peer.create_server(7777, 2)  # Crea un servidor en el puerto 7777 para 2 jugadores.
	get_tree().set_network_peer(peer)

	create_player(1)  # Crea el jugador del host.

func _process(delta):
	if get_tree().is_network_server() and get_tree().get_network_connected_peers().size() == 2 and not has_node("Player2"):
		create_player(2)  # Crea el jugador del cliente cuando se conecta.

func create_player(player_id):
	var player = player_scene.instance()
	player.set_name("Player" + str(player_id))
	player.set_network_master(player_id)
	add_child(player)

	var zone = Zone.new()
	zone.set_name("Zone" + str(player_id))
	zone.set_owner(player)
	add_child(zone)
