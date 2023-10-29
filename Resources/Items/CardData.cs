using Godot;
using System;

[GlobalClass]
public partial class CardData : Resource
{    
    public enum CardElement{NULL, VOID, STATIC, FREE, ASYNC, BURNT} 

    [Export] public int CardId {get; set;}               
    
    [Export(PropertyHint.Range, "1,3,")] public int CardLevel {get; set;}
    [Export] public String CardName {get; set;}          
    [Export] public String CardDescription {get; set;}
    [Export] public int CardDamage {get; set;}
    [Export] public int CardCost {get; set;}
    [Export] public int CardValue {get; set;}

    [Export] public CardElement ElementType {get; set;}
    [Export] public Texture CardArt {get; set;}

    public CardData() : this(0,0,null,null,0,0,0,CardElement.NULL, null){}

    public CardData(int cardId, int cardLevel, String cardName, String cardDescription, int cardDamage, int cardCost, int cardValue, CardElement elementType, Texture cardArt)
    {
        CardId = cardId;
        CardLevel = cardLevel;
        CardName = cardName;
        CardDescription = cardDescription;
        CardDamage = cardDamage;
        CardCost = cardCost;
        CardValue = cardValue;
        ElementType = elementType;
        CardArt = cardArt;
    }
}
