using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Title("Audio Settings")]
    [SerializeField] private AudioSource _masterAudioSource;
    [SerializeField] private AudioSource _effectsAudioSource;

    [Title("Overworld Clips")]
    public AudioClip _overworldClip;
    public AudioClip _battleClip;

    [Title("Battle Clips")]
    public AudioClip _blockedDamage;
    public AudioClip _buritiDamaged;
    public AudioClip _discardCard;
    public AudioClip _drawCard;
    public AudioClip _slashFoley;
    public AudioClip _healingFoley;
    public AudioClip _drainedFoley;
    public AudioClip _transformationFoley;
    public AudioClip _enemyDamaged;

    [Title("GameStates Clips")]
    public AudioClip _gameOver;
    public AudioClip _battleVictory;
    public AudioClip _gameVictory;

    [Title("UI Clips")]
    public AudioClip _enterUI;
    public AudioClip _hoverUI;
    public AudioClip _statusUpdateUI;

    [Title("Listening on")]
    public VoidEventChannelSO _eventOnCombatActivated;
    public VoidEventChannelSO _eventOnCombatDeactivated;

    private void Start()
    {
        _eventOnCombatActivated.OnEventRaised += BattleMusic;
        _eventOnCombatDeactivated.OnEventRaised += OverworldMusic;
    }

    public void OverworldMusic()
    {
        _masterAudioSource.clip = _overworldClip;
        _masterAudioSource.Play();
    }
    public void BattleMusic()
    {
        _masterAudioSource.clip = _battleClip;
        _masterAudioSource.Play();
    }
    public void GameOverMusic()
    {
        _masterAudioSource.clip = _gameOver;
        _masterAudioSource.loop = false;
        _masterAudioSource.Play();
    }
    public void GameVictoryMusic()
    {
        _masterAudioSource.clip = _gameVictory;
        _masterAudioSource.loop = false;
        _masterAudioSource.Play();
    }
    public void BattleVictoryMusic()
    {
        _effectsAudioSource.clip = _battleVictory;
        _effectsAudioSource.Play();
    }
    public void DrawCardEffect()
    {
        _effectsAudioSource.PlayOneShot(_drawCard);
    }
    public void DiscardCardEffect()
    {
        _effectsAudioSource.PlayOneShot(_discardCard);
    }
    public void SlashEffect()
    {
        _effectsAudioSource.PlayOneShot(_slashFoley);
    }
    public void BlockedDamageEffect()
    {
        _effectsAudioSource.PlayOneShot(_blockedDamage);
    }
    public void BuritiDamagedEffect()
    {
        _effectsAudioSource.PlayOneShot(_buritiDamaged);
    }
    public void HealingEffect()
    {
        _effectsAudioSource.PlayOneShot(_healingFoley);
    }
    public void DraineddEffect()
    {
        _effectsAudioSource.PlayOneShot(_drainedFoley);
    }
    public void TransformationEffect()
    {
        _effectsAudioSource.PlayOneShot(_transformationFoley);
    }
    public void StatusUpdate()
    {
        _effectsAudioSource.PlayOneShot(_statusUpdateUI);
    }
    public void EnemyDamaged()
    {
        _effectsAudioSource.PlayOneShot(_enemyDamaged);
    }
}
