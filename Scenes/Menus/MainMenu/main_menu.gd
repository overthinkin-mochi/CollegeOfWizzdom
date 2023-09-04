extends Control

# Called when the node enters the scene tree for the first time.
func _ready():
	GameManager.game_active = false
	GameSettings.load_config()


func _on_start_button_pressed():
	get_tree().change_scene_to_file("res://Scenes/world.tscn")


func _on_settings_button_pressed():
	get_tree().change_scene_to_file("res://Scenes/Menus/SettingsMenu/setting_menu.tscn")


func _on_exit_button_pressed():
	get_tree().quit()
