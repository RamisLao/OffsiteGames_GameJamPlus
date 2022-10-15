using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffectResolver : MonoBehaviour
{
    [Title("Listening on")]
    [SerializeField] private ApplyCardEffectEventChannelSO _eventApplyCardEffect;

    private void Awake()
    {
        _eventApplyCardEffect.OnEventRaised += ApplyEffect;
    }

    private void ApplyEffect(CardData cardData, EnemyAI enemy)
    {
        if (cardData.AppliesBlock) AddBlockPoints(cardData, enemy);
        if (cardData.AppliesSappling) AddSapplings(cardData, enemy);
        if (cardData.AppliesStun) ApplyStun(cardData, enemy);
        if (cardData.AppliesExposed) ApplyExposed(cardData, enemy);
        if (cardData.AppliesProtected) ApplyProtected(cardData, enemy);
        if (cardData.AppliesDamage) ApplyDamage(cardData, enemy);
        if (cardData.AppliesHeal) ApplyHeal(cardData, enemy);
        Debug.Log(cardData);
        Debug.Log(enemy);
    }

    private void AddBlockPoints(CardData cardData, EnemyAI enemy)
    {
        if (enemy.TryGetComponent(out Health health))
        {
            health.AddBlockPoints(cardData.BlockAmount);
        }
    }

    private void AddSapplings(CardData cardData, EnemyAI enemy)
    {
        if (enemy.TryGetComponent(out Health health))
        {
            health.AddSapplings(cardData.SapplingAmount);
        }
    }

    private void ApplyStun(CardData cardData, EnemyAI enemy)
    {
        enemy.ApplyStun();
    }

    private void ApplyExposed(CardData cardData, EnemyAI enemy)
    {
        if (enemy.TryGetComponent(out Health health))
        {
            health.AddExposedPoints(cardData.ExposedAmount);
        }
    }

    private void ApplyProtected(CardData cardData, EnemyAI enemy)
    {
        if (enemy.TryGetComponent(out Health health))
        {
            health.AddProtectedPoints(cardData.ProtectedAmount);
        }
    }

    private void ApplyDamage(CardData cardData, EnemyAI enemy)
    {
        if (enemy.TryGetComponent(out Health health))
        {
            health.SubtractHealth(cardData.DamageAmount);
        }
    }

    private void ApplyHeal(CardData cardData, EnemyAI enemy)
    {
        if (enemy.TryGetComponent(out Health health))
        {
            health.AddHealth(cardData.HealAmount);
        }
    }
}
