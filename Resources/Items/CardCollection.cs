using Godot;
using System;

[GlobalClass]
public partial class CardCollection : Resource{

    [Export]Godot.Collections.Array<CardData> card;
}
