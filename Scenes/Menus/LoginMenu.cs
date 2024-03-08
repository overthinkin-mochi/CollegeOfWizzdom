using Godot;
using System.Text;

public partial class LoginMenu : Control
{
    string password;
    string username;
    string userId;

    string endpointGet = "http://localhost:3001/player_all_attributes/";
    string endpointPost = "http://localhost:3002/spend_attributes/";
    string endpointLogin = "http://localhost:3010/player/";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public void _on_login_button_pressed(){
        // get username and password
        TextEdit UsernameField = GetNode<TextEdit>("Username_field");
        TextEdit PasswordField = GetNode<TextEdit>("Password_field");

        endpointLogin += UsernameField.Text + "/" + PasswordField.Text;
        
        
        GD.Print(endpointLogin);

        // http request
        HttpRequest loginRequest = GetNode<HttpRequest>("LoginRequest");
        loginRequest.RequestCompleted += OnLoginCompleted;

        loginRequest.Request(endpointLogin);
        
    }

    private void OnLoginCompleted(long result, long responseCode, string[] headers, byte[] body){
        Godot.Collections.Dictionary json = Json.ParseString(Encoding.UTF8.GetString(body)).AsGodotDictionary();
        GD.Print(json);
    }

    private void OnGetCompleted(){
        HttpRequest getRequest = GetNode<HttpRequest>("getRequest");
    }

    private void OnPostCompleted(){
        HttpRequest postRequest = GetNode<HttpRequest>("postRequest");
    }
}
