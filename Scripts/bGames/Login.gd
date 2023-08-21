extends Node

@onready var password_field = $Password_field
@onready var username_field = $Username_field
@onready var login_button = $LoginButton
@onready var status_label = $StatusLabel
@onready var http_request = $HTTPRequest

var endpointGet = "http://localhost:3001/player_all_attributes/"
var endpointPost = "http://localhost:3002/spend_attributes/"
var endpointLogin = "http://localhost:3010/player/"

var userId : String

func _ready():	
	pass

func _on_login_button_pressed():
	var username = username_field.text
	var password = password_field.text 
	
	print(username)
	if username == null or password == null:
		status_label.text = "porfavor rellene todos los campos!"
		return
	
	var url = str(endpointLogin) + str(username) + "/" + str(password)
	http_request.request(url)
	
func _on_login_request_request_completed(result, response_code, headers:Array, body:PackedByteArray):
	userId = body.get_string_from_utf8()
