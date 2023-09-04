extends Control


# Called when the node enters the scene tree for the first time.
func _ready():
	$FullScreenButton.set_pressed_no_signal(GameSettings.fullscreen)
	$MasterVolumeSlider.value = GameSettings.master_volume
	$MusicVolumeSlider.value = GameSettings.music_volume
	$EffectsVolumeSlider.value = GameSettings.effects_volume
	$VoicesVolumeSlider.value = GameSettings.voices_volume

func _on_back_button_pressed():
	GameManager.save_config()
	if GameManager.game_active:
		var pause_menu_scene = load("res://Scenes/Menus/PauseMenu/pause_menu.tscn")
		var pause_menu_instance = pause_menu_scene.instantiate()
		get_parent().add_child(pause_menu_instance)
		queue_free()
	else:
		get_tree().change_scene_to_file("res://Scenes/Menus/MainMenu/main_menu.tscn")
		queue_free()

func _on_full_screen_button_toggled(button_pressed):
	GameSettings.fullscreen = button_pressed
	GameSettings.initialize_video()

func _on_master_volume_slider_value_changed(value):
	$MasterVolumeSlider/Label.text = str(value)
	GameSettings.master_volume = value
	GameSettings.initialize_audio()

func _on_music_volume_slider_value_changed(value):
	$MusicVolumeSlider/Label.text = str(value)
	GameSettings.music_volume = value
	GameSettings.initialize_audio()

func _on_effects_volume_slider_value_changed(value):
	$EffectsVolumeSlider/Label.text = str(value)c
	GameSettings.effects_volume = value
	GameSettings.initialize_audio()

func _on_voices_volume_slider_value_changed(value):
	$VoicesVolumeSlider/Label.text = str(value)
	GameSettings.voices_volume = value
	GameSettings.initialize_audio()
	
