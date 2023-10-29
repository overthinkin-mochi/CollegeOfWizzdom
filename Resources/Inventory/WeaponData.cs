using Godot;
using System;

public partial class WeaponData : ItemData
{
    public enum WeaponType{MELEE, THROWABLE ,RANGED ,MAGIC }
    
    [Export] public WeaponType weaponType {get; set;}
    [Export] public int Attack {get; set;}
    [Export] public float Speed {get; set;}
    
    [Export] public PackedScene Projectile {get; set;}
    
    public WeaponData() : this(WeaponType.MELEE, 0, 0, null){}

    public WeaponData(WeaponType type, int attack, float speed, PackedScene projectile){
        weaponType = type;
        Attack = attack;
        Speed = speed;
        Projectile = projectile;
            
            
    }
    
}
