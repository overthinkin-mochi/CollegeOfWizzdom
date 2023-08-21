extends Node

#signal get_attributes()
#signal spend_attributes()

@onready var password_field = $Password_field
@onready var username_field = $Username_field
@onready var login_button = $LoginButton
@onready var status_label = $StatusLabel

@onready var login_request = $LoginRequest
@onready var get_request = $GetRequest
@onready var post_request = $PostRequest

var endpointGet = "http://localhost:3001/player_all_attributes/"
var endpointPost = "http://localhost:3002/spend_attributes/"
var endpointLogin = "http://localhost:3010/player/"

var userId : String
var points: Array = [0, 0, 0, 0, 0]
var _isLogged : bool


func _on_login_button_pressed():
	var username = username_field.text
	var password = password_field.text 
	
	if username == null or password == null:
		status_label.text = "porfavor rellene todos los campos!"
		return
	
	var url = str(endpointLogin) + str(username) + "/" + str(password)
	login_request.request(url)
	
func _on_http_request_request_completed(result, response_code, headers:Array, body:PackedByteArray):
	if response_code == 200 :
		_isLogged = true
		userId = body.get_string_from_utf8()	
	else:
		print("Error de conexion")
	
func get_attributes():
	var url = str(endpointGet) + str(userId)
	get_request.request(url)
	
func spend_attribute(attribute_index, amount):
	
	var new_value = points[attribute_index] - amount
	if new_value > 0: 
		var url = str(endpointPost + str(userId) + "/" + str(attribute_index) + "/" + str(amount))
		post_request.request(url)
	else:
		print("no se tienen los puntos suficientes")


func _on_login_request_request_completed(result, response_code, headers:Array, body:PackedByteArray):
	if response_code == 200 :
		_isLogged = true
		userId = body.get_string_from_utf8()	
	else:
		print("Error de conexion")

func _on_get_request_request_completed(result, response_code, headers:Array, body:PackedByteArray):
	if response_code == 200 :
		var salida = body.get_string_from_utf8();
		print(salida)
	else:
		print("Error de conexion")



