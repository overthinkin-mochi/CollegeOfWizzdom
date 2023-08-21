extends Node2D

var panels = []

func _ready():
	for y in range(4):
		for x in range(3):
			var panel = Panel.new()
			panel.set_owner(owner)
			panel.set_grid_position(x, y)
			panels.append(panel)
			add_child(panel)
