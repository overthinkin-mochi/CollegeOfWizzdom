extends PanelContainer

signal slot_clicked(index: int, button: int)

@onready var texture_rect: TextureRect = $MarginContainer/TextureRect
@onready var quantity_label: Label = $CuantityLabel

func set_slot_data(slot_data: SlotData) -> void: 
	var item_data = slot_data.item_data
	texture_rect.texture = item_data.texture
	tooltip_text = "%s\n%s" % [item_data.name, item_data.description]
	
	if slot_data.quantity > 1:
		quantity_label.text = "%s" % slot_data.quantity
		quantity_label.show()

func _on_gui_input(event: InputEvent) -> void:
	if event is InputEventMouseMotion:
		var cursor_position = event.position
#		print("Cursor movido a: ", cursor_position)

	elif event is InputEventMouseButton:
		if event.pressed:
#			print("Botón del ratón presionado.")
			if event.button_index == MOUSE_BUTTON_LEFT:
				slot_clicked.emit(get_index(), event.button_index)
				print("Botón izquierdo del ratón presionado.")
			elif event.button_index == MOUSE_BUTTON_RIGHT:
				slot_clicked.emit(get_index(), event.button_index)
				print("Botón derecho del ratón presionado.")
#		else:
#			print("Botón del ratón liberado.")
			

