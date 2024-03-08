using Godot;
using System;

public partial class ItemData : Resource{
    
    [Export] public string Name {get; set;}
    
    [Export(PropertyHint.MultilineText)] public string Description {get; set;}
    
    [Export] public int Id {get; set;}

    [Export] public bool Stackable {get; set;}
    [Export] public Texture Icon {get; set;}

    public ItemData() : this(null,null,0,false,null){}

    public ItemData(string name, string description, int id, bool stackable, Texture texture){
        Name = name;
        Description = description;
        Id = id;
        Stackable = stackable;
        Icon = texture;
    }
}
