using System;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    private const string PlayerPrefs_Volume = "Volume";

    private float eatDotTimer = 0;


    public static float Volume
    {
        get => PlayerPrefs.GetFloat(PlayerPrefs_Volume, 1f);
        set
        {
            PlayerPrefs.SetFloat(PlayerPrefs_Volume, value);
            UpdateVolume(Volume);
        }
    }

    public static bool IsVolumeOn => Mathf.RoundToInt(Volume) == 1;

    public static bool IsStartMusicPlaying => Instance.musics.startMusic.isPlaying;
    public static bool IsGameOverPlaying => Instance.sfxs.gameOver.isPlaying;


    public static void StopAll()
    {
        foreach (var music in Instance.musics.All)
        {
            music.Stop();
        }

        foreach (var sfx in Instance.sfxs.All)
        {
            sfx.Stop();
        }
    }

    public static void PlayStartMusic()
    {
        Instance.musics.startMusic.Play();
    }

    public static void PlayEatDot()
    {
        Instance.eatDotTimer = 0.25f;

        if (Instance.sfxs.eatDot.isPlaying) return;

        Instance.sfxs.eatDot.Play();
    }

    public static void StopEatDot()
    {
        Instance.sfxs.eatDot.Stop();
    }

    public static void PlayEatGhost()
    {
        Instance.sfxs.eatGhost.Play();
    }

    public static void PlayGameOver()
    {
        Instance.sfxs.gameOver.Play();
    }

    public static void PlayGhostMove()
    {
        if (Instance.sfxs.ghostMove.isPlaying) return;

        Instance.sfxs.ghostMove.Play();
    }

    public static void StopGhostMove()
    {
        Instance.sfxs.ghostMove.Stop();
    }

    public static void PlayGhostReturnBase()
    {
        if (Instance.sfxs.ghostReturnBase.isPlaying) return;

        Instance.sfxs.ghostReturnBase.Play();
    }

    public static void StopGhostReturnBase()
    {
        Instance.sfxs.ghostReturnBase.Stop();
    }

    public static void PlayGhostTurnBlue()
    {
        if (Instance.sfxs.ghostTurnBlue.isPlaying) return;

        Instance.sfxs.ghostTurnBlue.Play();
    }

    public static void PauseGhostTurnBlue()
    {
        Instance.sfxs.ghostTurnBlue.Pause();
    }

    public static void UnPauseGhostTurnBlue()
    {
        Instance.sfxs.ghostTurnBlue.UnPause();
    }

    public static void StopGhostTurnBlue()
    {
        Instance.sfxs.ghostTurnBlue.Stop();
    }


    private void Update()
    {
        TryStopEatDot();
        UpdateGhostReturnBase();
    }

    private void TryStopEatDot()
    {
        if (!sfxs.eatDot.isPlaying) return;

        eatDotTimer -= Time.deltaTime;

        if (eatDotTimer > 0) return;

        sfxs.eatDot.Stop();
    }

    private void UpdateGhostReturnBase()
    {
        try
        {
            if (GhostContainer.HasDeadGhost && !GameController.IsFreezed)
            {
                PlayGhostReturnBase();
                return;
            }

            StopGhostReturnBase();
        }
        catch (System.Exception){}
    }

    private static void UpdateVolume(float volume)
    {
        foreach (var music in Instance.musics.All)
        {
            music.volume = volume;
        }

        foreach (var sfx in Instance.sfxs.All)
        {
            sfx.volume = volume;
        }
    }




    [SerializeField] private Musics musics;

    [Serializable]
    internal struct Musics
    {
        public AudioSource[] All => new AudioSource[] { startMusic };

        public AudioSource startMusic;
    }

    [SerializeField] private SFXs sfxs;

    [Serializable]
    internal struct SFXs
    {
        public AudioSource[] All => new AudioSource[] 
        {
            eatDot,
            eatGhost,
            gameOver,
            ghostMove,
            ghostReturnBase,
            ghostTurnBlue
        };

        public AudioSource eatDot;
        public AudioSource eatGhost;
        public AudioSource gameOver;
        public AudioSource ghostMove;
        public AudioSource ghostReturnBase;
        public AudioSource ghostTurnBlue;
    }
}
