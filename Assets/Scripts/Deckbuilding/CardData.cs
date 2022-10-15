using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ERarity
{
    Common,
    Uncommon,
    Rare
}

[CreateAssetMenu(menuName = "Deckbuilding/Card Data")]
public class CardData : ScriptableObject
{
    [Title("Visual Identity")]
    public string Name = "Card Name";
    public Sprite Image;
    public Color Color;

    [Title("Usage")]
    public ERarity Rarity;
    public int ManaCost = 3;

    [Title("Mechanics")]
    [Multiline(10)]
    public string Description = "Description";
}
