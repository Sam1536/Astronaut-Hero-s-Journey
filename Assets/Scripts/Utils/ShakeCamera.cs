using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SamEbac.Core.Singleton;
using Cinemachine;

public class ShakeCamera : Singleton<ShakeCamera>
{
    public CinemachineVirtualCamera virtualCamera;
    public float shakeTime;
    private CinemachineBasicMultiChannelPerlin channelPerlin;

    [Header("Shake Value")]
    public float frequency = 3f;
    public float amplitude = 3f;
    public float time = .3f;

    [NaughtyAttributes.Button]
    public void Shake()
    {
        ShakeCam(amplitude, frequency, time);
    }

    public void ShakeCam(float amplitude,float frequency,float time)
    {
        //channelPerlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitude;
        virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = frequency;

        shakeTime = time;
    }

    private void Update()
    {
        if(shakeTime > 0)
        {
            shakeTime -= Time.deltaTime;
        }
        else
        {

            //frequency = 0f;
            //amplitude = 0f;
            //time = 0f;
            virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0f;
            virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0f;
        }
    }
}
