using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointBase : MonoBehaviour
{

    public MeshRenderer meshRenderer;
    public int key = 01;

    private bool checkpointActived = false;
    private string checkpointKey = "Checkpointkey"; 

    private void OnTriggerEnter(Collider other)
    {

        if(!checkpointActived && other.transform.tag == "Player")
        {
            CheckPoint();

        }
    }

    private void CheckPoint()
    {
        SaveCheckPoint();
        TurnItOn();
    }
    
    [NaughtyAttributes.Button]
    private void TurnItOn()
    {
        meshRenderer.material.SetColor("_EmissionColor", Color.white);
    }
    
    [NaughtyAttributes.Button]
    private void TurnItOff()
    {
        meshRenderer.material.SetColor("_EmissionColor", Color.black);
    }

    private void SaveCheckPoint()
    {
        /*if(PlayerPrefs.GetInt(checkpointKey,0) > key )
             PlayerPrefs.SetInt(checkpointKey, key);*/

        CheckPointManager.instance.SaveCheckPoint(key);

        checkpointActived = true;
    }
}
