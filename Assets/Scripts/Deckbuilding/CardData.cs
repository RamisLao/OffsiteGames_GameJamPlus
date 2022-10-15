using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ERarity
{
    Common,
    Uncommon,
    Rare,
    Legendary
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
    public bool TargetsAll = false;
    public bool GetsDestroyed = false;

    public bool AppliesDamage = false;
    [ShowIf("@this.AppliesDamage")]
    public int DamageAmount = 3;

    public bool AppliesHeal = false;
    [ShowIf("@this.AppliesHeal")]
    public int HealAmount = 3;

    public bool AppliesBlock = false;
    [ShowIf("@this.AppliesBlock")]
    public int BlockAmount = 3;

    public bool AppliesSappling = false;
    [ShowIf("@this.AppliesSappling")]
    public int SapplingAmount = 1;

    public bool AppliesStun = false;

    public bool AppliesExposed = false;
    [ShowIf("@this.AppliesExposed")]
    public int ExposedAmount = 1;

    public bool AppliesProtected = false;
    [ShowIf("@this.AppliesProtected")]
    public int ProtectedAmount = 1;

    public bool AppliesAbsorb = false;
    [ShowIf("@this.AppliesAbsorb")]
    public int AbsorbAmount = 2;
}
