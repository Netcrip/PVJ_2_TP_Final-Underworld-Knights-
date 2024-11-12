using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudioControllerUI : MonoBehaviour
{
    [Header("Audio UI Elements")]
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Toggle muteToggle;
    [SerializeField] private TextMeshProUGUI volumeText;

    private void Start()
    {
        // Configura el rango y valor inicial del slider
        volumeSlider.minValue = 0f;
        volumeSlider.maxValue = 1f;

        // Cargar el volumen guardado
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 0.5f); // Si no hay valor guardado, se usará 0.5f por defecto
        UpdateVolumeText(volumeSlider.value);

        muteToggle.isOn = !AudioController.Instance.IsMuted();

        volumeSlider.onValueChanged.AddListener(OnVolumeSliderChange);
        muteToggle.onValueChanged.AddListener(OnMuteToggleChange);
    }

    private void OnVolumeSliderChange(float value)
    {
        // Ajusta el volumen en el AudioController
        AudioController.Instance.SetVolume(volumeSlider.value * 100);
        UpdateVolumeText(volumeSlider.value);

        // Guardar el valor del volumen
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        PlayerPrefs.Save();
    }

    private void OnMuteToggleChange(bool isOn)
    {
        AudioController.Instance.ToggleMute();
    }

    private void UpdateVolumeText(float sliderValue)
    {
        int volumePercentage = Mathf.RoundToInt(sliderValue * 100);
        volumeText.text = volumePercentage + "%";
    }
}