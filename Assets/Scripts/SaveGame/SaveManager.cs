using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SamEbac.Core.Singleton;

public class SaveManager : Singleton<SaveManager >
{
   [SerializeField] private SaveSetup _saveSetup;

    private string _path = Application.streamingAssetsPath + "/Save.txt";

    public int lastLevel;

    public  Action<SaveSetup> fileLoaded;

    public SaveSetup Setup
    {
        get
        {
            return _saveSetup;
        }
       
    }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        
    }

    private void Start()
    {
        Invoke(nameof(Load), .1f);
    }


    private void CreateNewSave()
    {
        _saveSetup = new SaveSetup();
        _saveSetup.LastLevel = 0;
        _saveSetup.PlayerName = "Samuel";
    }


    #region SAVE

    [NaughtyAttributes.Button ]
    private void Save()
    {
        string setupToJson = JsonUtility.ToJson(_saveSetup, true);
        SaveFile(setupToJson);
        Debug.Log(setupToJson);
    }


    public void SaveItens()
    {
        _saveSetup.Coins = Itens.ItemManager.instance.GetByType(Itens.ItemType.COIN).sOint.value;
        _saveSetup.Health = Itens.ItemManager.instance.GetByType(Itens.ItemType.LIFE_PACK).sOint.value;
        Save();
    }

    public void SaveName(string text)
    {
        _saveSetup.PlayerName = text;
        Save();
    }

    public void SaveLastLevel(int level)
    {
        _saveSetup.LastLevel = level;
        SaveItens();
        Save();
    }

    #endregion
   
    private void SaveFile(string json)
    {

        Debug.Log(_path);
        File.WriteAllText(_path, json);
    }
    
    
    [NaughtyAttributes.Button]
    private void SaveLevelOne()
    {
        SaveLastLevel(1);
    }

    [NaughtyAttributes.Button]
    private void Load()
    {
        string fileLoad = "";

        if (File.Exists(_path)) 
        { 
            fileLoad = File.ReadAllText(_path);
            _saveSetup = JsonUtility.FromJson<SaveSetup>(fileLoad);
            lastLevel = _saveSetup.LastLevel;
        }
        else
        {
            CreateNewSave();
            Save();
        }
      
        fileLoaded.Invoke(_saveSetup);
    }

}


[System.Serializable]
public class SaveSetup
{
    public int LastLevel;
    public string PlayerName;
    public float Coins;
    public float Health;
}
