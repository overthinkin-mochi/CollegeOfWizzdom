extends Node2D

enum State { NORMAL, ROTO }

var state = State.NORMAL
var grid_position = Vector2()

func _ready():
	pass

func set_owner(player):
	owner = player
	player.panels_owned.append(self)

func set_grid_position(x, y):
	grid_position = Vector2(x, y)

func break_panel():
	state = State.ROTO

func repair_panel():
	state = State.NORMAL
