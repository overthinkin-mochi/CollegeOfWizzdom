using Godot;
using System;

[GlobalClass]
public partial class ProjectileData : Resource
{
    [Export] public int Damage;
    [Export] public float Speed; 

    [Export] public Vector3 _direction;
    [Export] AtlasTexture ProjectileTexture;
    
    public ProjectileData() : this(0,0,Vector3.Zero, null){}

    public ProjectileData(int damage, float speed, Vector3 direction, AtlasTexture projectileTexture)
    {
        Damage = damage;
        Speed = speed;
        _direction = direction;
        ProjectileTexture = projectileTexture;
    }
}

