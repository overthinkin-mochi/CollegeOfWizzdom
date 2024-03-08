using Godot;
using System;

public partial class Projectile : Node3D
{
	[Export] public ProjectileData projectileData;
    

    private float speed;


    private AnimationPlayer animationPlayer;
	private Tween tween;
    private Area3D projectileArea;

    private Vector3 direction = new Vector3();

	public override void _Ready(){
        
        speed =  projectileData.Speed;
        direction = projectileData._direction;

        animationPlayer = GetNode<AnimationPlayer>("MeshInstance3D/AnimationPlayer");
        projectileArea = GetNode<Area3D>("ProjectileArea");
        
	    animationPlayer.Play("Idle");

    }

	public override void _PhysicsProcess(double delta){

        GlobalTranslate( speed * direction * (float)delta );
            
    }

	public void destroy(){
        animationPlayer.Play("Hit");
		QueueFree();
	}
    private void OnAreaEntered(Area3D area){
        if(area.IsInGroup("Player")){
            destroy();
        }
    }
    private void OnBodyEntered(Node3D body){
        if(body.IsInGroup("Player")){

            destroy();
        }
    }
    private void OnVisibleOnScreenNotifier3DScreenExited(){
        destroy();
    }
    

}
