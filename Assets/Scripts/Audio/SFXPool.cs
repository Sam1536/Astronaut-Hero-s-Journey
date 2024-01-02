using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SamEbac.Core.Singleton;

public class SFXPool : Singleton<SFXPool>
{
    private List<AudioSource> _audioSourcesList;

    public int poolSize = 10;

    private int _index = 0;

    private void Start()
    {
        CreatePool();
    }

    private void CreatePool()
    {
        _audioSourcesList = new List<AudioSource>();

        for (int i = 0; i < poolSize; i++)
        {
            CreateAudioSourceIten();
        }
      

    }

    private void CreateAudioSourceIten()
    {
        GameObject go = new GameObject("SFX_Pool");
        go.transform.SetParent(gameObject.transform);
        _audioSourcesList.Add(go.AddComponent<AudioSource>());
    }


    public void Play(SFXType sFxType)
    {
        if (sFxType == SFXType.NONE) return; 

        var sfx = SoundManager.instance.GetSfxByType(sFxType);

        _audioSourcesList[_index].clip = sfx.audioClip;
        _audioSourcesList[_index].Play();

        _index++;

        if (_index >= _audioSourcesList.Count) _index = 0;



    }
}
