using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Title("Audio Settings")]
    [SerializeField] private AudioSource _masterAudioSource;
    [SerializeField] private AudioSource _effectsAudioSource;

    [Title("Music Clips")]
    public AudioClip _overworldClip;
    public AudioClip _battleClip;
    public AudioClip _mainMenuClip;

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
    public AudioClip _clickUI;
    public AudioClip _hoverUI;
    public AudioClip _statusUpdateUI;

    [Title("Listening on")]
    public VoidEventChannelSO _eventOnCombatPlay;
    public VoidEventChannelSO _eventOnCombatStop;
    public VoidEventChannelSO _eventOnOverworldPlay;
    public VoidEventChannelSO _eventOnOverworldStop;
    public VoidEventChannelSO _eventOnMainMenuPlay;
    public VoidEventChannelSO _eventOnMainMenuStop;
    public VoidEventChannelSO _eventOnUIClick;
    public VoidEventChannelSO _eventOnUIHover;
    public VoidEventChannelSO _eventOnVictoryStinger;
    public VoidEventChannelSO _eventOnLoseStinger;
    public VoidEventChannelSO _eventOnUltimateVictoryStinger;

    private void Awake()
    {
        _eventOnCombatPlay.OnEventRaised += BattleMusicPlay;
        _eventOnCombatStop.OnEventRaised += StopMusic;
        _eventOnOverworldPlay.OnEventRaised += OverworldMusicPlay;
        _eventOnOverworldStop.OnEventRaised += StopMusic;
        _eventOnUIClick.OnEventRaised += UIClick;
        _eventOnUIHover.OnEventRaised += UIHover;
        _eventOnMainMenuPlay.OnEventRaised += PlayMainMenu;
        _eventOnMainMenuStop.OnEventRaised += StopMusic;
        _eventOnVictoryStinger.OnEventRaised += PlayBattleVictoryMusic;
        _eventOnUltimateVictoryStinger.OnEventRaised += PlayGameOverMusic;
        _eventOnLoseStinger.OnEventRaised += PlayGameOverMusic;
    }

    public void OverworldMusicPlay()
    {
        _masterAudioSource.clip = _overworldClip;
        _masterAudioSource.Play();
    }
    public void BattleMusicPlay()
    {
        _masterAudioSource.clip = _battleClip;
        _masterAudioSource.Play();
    }
    public void PlayMainMenu()
    {
        _masterAudioSource.clip = _mainMenuClip;
        _masterAudioSource.Play();
    }
    public void StopMusic()
    {
        if (_masterAudioSource != null) _masterAudioSource.Stop();
    }
    public void PlayGameOverMusic()
    {
        _effectsAudioSource.clip = _gameOver;
        _effectsAudioSource.Play();
    }
    public void PlayGameVictoryMusic()
    {
        _effectsAudioSource.clip = _gameVictory;
        _effectsAudioSource.Play();
    }
    public void PlayBattleVictoryMusic()
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
    public void UIClick()
    {
        _effectsAudioSource.PlayOneShot(_clickUI);
    }
    public void UIHover()
    {
        _effectsAudioSource.PlayOneShot(_hoverUI);
    }
}
