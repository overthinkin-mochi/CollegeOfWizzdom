extends Node3D

@onready var background_view_port = $BaseCamera/Background_ViewPortContainer/Background_ViewPort
@onready var forground_view_port = $BaseCamera/Forground_ViewPortContainer2/Forground_ViewPort

@onready var background_camera = $BaseCamera/Background_ViewPortContainer/Background_ViewPort/BackgroundCamera
@onready var forground_camera = $BaseCamera/Forground_ViewPortContainer2/Forground_ViewPort/ForgroundCamera


# Called when the node enters the scene tree for the first time.
func _ready():
	resize();

func resize():
	background_view_port.size = DisplayServer.window_get_size()
	forground_view_port.size = DisplayServer.window_get_size()
# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	background_camera.global_transform = GameManager.player.camera_point.global_transform
	forground_camera.global_transform = GameManager.player.camera_point.global_transform
	
