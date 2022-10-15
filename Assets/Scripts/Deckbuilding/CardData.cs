using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Deckbuilding/Card Data")]
public class CardData : ScriptableObject
{
    public string Name = "Card Name";
    public Sprite Image;
    public Color Color;
}
