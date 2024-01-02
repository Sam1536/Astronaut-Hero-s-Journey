using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SamEbac.Core.Singleton;

public class SoundManager : Singleton<SoundManager>
{
    public List<MusicSetup> musicSetup; // musica
    public List<SFXSetup> sFXSetups; //Efeito sonoro

    public AudioSource musicSource;


    public void PlayMusicByType(MusicType musicType)
    {
        var music = GetMusicByType(musicType);
        musicSource.clip = music.audioClip;
        musicSource.Play();
    }

    public MusicSetup GetMusicByType(MusicType musicType)
    {
        return musicSetup.Find(i => i.musicType == musicType);
    }

    public SFXSetup GetSfxByType(SFXType sFXSetup)
    {
        return sFXSetups.Find(i => i.sfxTyp == sFXSetup );
    }

}

public enum MusicType
{
    NONE,
    TYPE_01,
    TYPE_02,
    TYPE_03

}

[System.Serializable]
public class MusicSetup
{
    public MusicType musicType;
    public AudioClip audioClip;
}


public enum SFXType
{
    NONE,
    TYPE_01,
    TYPE_02,
    TYPE_03

}


[System.Serializable]
public class SFXSetup
{
    public SFXType sfxTyp;
    public AudioClip audioClip;
}
