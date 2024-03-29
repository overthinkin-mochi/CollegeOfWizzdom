extends Node3D

@onready var player: CharacterBody3D = $Player
@onready var inventory_interface: Control = $UI/InventoryInterface
@onready var pause_menu = $UI/pause_menu

func _ready() -> void:
	
	player.toggle_inventory.connect(toggle_inventory_interface)
	inventory_interface.set_player_inventory_data(player.inventory_data)
	inventory_interface.set_player_equipment_data(player.equipment_data)
	inventory_interface.set_player_weapon_data(player.weapon_data)
	
	Input.mouse_mode = Input.MOUSE_MODE_CAPTURED
	GameManager.game_active = true
	
	for node in get_tree().get_nodes_in_group("external_inventory"):
		node.toggle_inventory.connect(toggle_inventory_interface)
		
func _input(_event):
	if Input.is_action_just_pressed("Menu"):
		var pause_menu_scene = load("res://Scenes/Menus/PauseMenu/pause_menu.tscn")
		var pause_menu_instance = pause_menu_scene.instantiate()
		$UI.add_child(pause_menu_instance)
		get_tree().paused = true
		
func toggle_inventory_interface(external_inventory_owner = null) -> void:
	inventory_interface.visible = not inventory_interface.visible
	
	if inventory_interface.visible:
		Input.mouse_mode = Input.MOUSE_MODE_VISIBLE
	else:
		Input.mouse_mode = Input.MOUSE_MODE_CAPTURED
	
	if external_inventory_owner and inventory_interface.visible:
		inventory_interface.set_external_inventory(external_inventory_owner)
	else:
		inventory_interface.clear_external_inventory()
		
