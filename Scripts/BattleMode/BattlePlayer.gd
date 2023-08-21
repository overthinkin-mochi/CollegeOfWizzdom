extends Node2D

var panels_owned = []
var current_panel = null

func _ready():
	pass

func _physics_process(delta):
	if is_multiplayer_authority():
		if Input.is_action_pressed('ui_right'):
			move_to_panel(1, 0)
		elif Input.is_action_pressed('ui_left'):
			move_to_panel(-1, 0)
		elif Input.is_action_pressed('ui_up'):
			move_to_panel(0, -1)
		elif Input.is_action_pressed('ui_down'):
			move_to_panel(0, 1)

		if Input.is_action_just_pressed('attack'):
			attack()

func move_to_panel(dx, dy):
	var new_position = current_panel.grid_position + Vector2(dx, dy)
	for panel in panels_owned:
		if panel.grid_position == new_position and panel.state != Panel.State.ROTO:
			self.position = panel.position
			current_panel = panel
			break

func attack():
	# Define el comportamiento del ataque aquí
	pass 

func detect_panel_state():
	if current_panel.state == Panel.State.ROTO:
		# La reacción si el panel está roto
		pass
	elif current_panel.state == Panel.State.NORMAL:
		# La reacción si el panel está normal
		pass
