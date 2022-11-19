using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class volumeSliderScript : MonoBehaviour
{
    [SerializeField] private Slider _slider; 
    // Start is called before the first frame update
    void Start()
    {
        soundManager.Instance.ChangeMasterVolume(_slider.value);
        _slider.onValueChanged.AddListener(val => soundManager.Instance.ChangeMasterVolume(val));
    }


}