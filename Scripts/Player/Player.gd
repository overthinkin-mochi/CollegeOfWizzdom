extends CharacterBody3D

signal toggle_inventory()

var level: int
var health: int

# Importacion de inventario y estructuras de datos 

@export var inventory_data: InventoryData
@export var equipment_data: EquipmentData
@export var weapon_data: WeaponData
#@export var b_points: BPoints

const SPEED = 2.5
const JUMP_VELOCITY = 4.0

var walking = false
var facing_left = false


var gravity = ProjectSettings.get_setting("physics/3d/default_gravity")
@onready var mesh_instance_3d = $Visuals/MeshInstance3D
@onready var animated_sprite_3d = $Visuals/MeshInstance3D/AnimatedSprite3D

@onready var interact_ray = $InteractRay
@onready var camera_point = $CameraPoint

func _ready():
	GameManager.set_player(self)

func _unhandled_input(event):
	if Input.is_action_just_pressed("InventoryToggle"):
		toggle_inventory.emit()
	if Input.is_action_just_pressed("interact"):
		interact() 

func _input(event):
	if event.is_action_pressed("Left"):
		facing_left = true
	elif event.is_action_pressed("Right"):
		facing_left = false
		
	if facing_left:
		animated_sprite_3d.flip_h = true
	
	else:
		animated_sprite_3d.flip_h = false

func _physics_process(delta):
	# Add the gravity.
	if not is_on_floor():
		velocity.y -= gravity * delta

	# Handle Jump.
	if Input.is_action_just_pressed("ui_accept") and is_on_floor():
		velocity.y = JUMP_VELOCITY

	# Get the input direction and handle the movement/deceleration.
	# As good practice, you should replace UI actions with custom gameplay actions.
	var input_dir = Input.get_vector("Left", "Right", "Up", "Down")
	var direction = (transform.basis * Vector3(input_dir.x, 0, input_dir.y)).normalized()
	if direction:
		velocity.x = direction.x * SPEED
		velocity.z = direction.z * SPEED
		
		if !walking:
			walking = true
			animated_sprite_3d.play("Running")
		
	else:
		velocity.x = move_toward(velocity.x, 0, SPEED)
		velocity.z = move_toward(velocity.z, 0, SPEED)
		
		if walking:
			walking = false 
			animated_sprite_3d.play("Idle")

	move_and_slide()
	
	if velocity.x != 0 and velocity.z != 0:
		interact_ray.look_at(global_transform.origin + direction)
	

func interact() -> void: 
	if interact_ray.is_colliding():
		interact_ray.get_collider().player_interact()
