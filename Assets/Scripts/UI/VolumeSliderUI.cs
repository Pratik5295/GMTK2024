using UnityEngine;
using UnityEngine.UI;

public class VolumeSliderUI : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    private void Start()
    {
        _slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        var value = Mathf.Pow(10, AudioManager.Instance.BG_Volume) / 20;
        _slider.value = value;
    }

    public void UpdateSliderValue()
    {
        var val = Mathf.Log10(_slider.value * 20);
        AudioManager.Instance.SetBackgroundVolume(val);
    }
}
