using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public AudioMixer mixer;
    public Toggle mute;
    public Slider masterVolume;
    string MasterVolParameter = "Volume";

    // Start is called before the first frame update
    void Start()
    {
        masterVolume.onValueChanged.AddListener(HandleSliderChange);
        mute.onValueChanged.AddListener(HandleToggleChange);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void HandleSliderChange(float value)
    {
        if (!mute.isOn)
            mixer.SetFloat(MasterVolParameter, value);
    }

    void HandleToggleChange(bool value)
    {
        if (value)
        {
            mixer.SetFloat(MasterVolParameter, -80);
        }
        else
        {
            mixer.SetFloat(MasterVolParameter, masterVolume.value);
        }
    }
}
