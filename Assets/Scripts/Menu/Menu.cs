using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Menu : MonoBehaviour
{
    [SerializeField] private Image Panel;
    [SerializeField] private List<Sprite> BackgroundPanel;
    [SerializeField] private GameObject SettingImage;
    public TMP_Dropdown PremissionDrop, BackgroundDrop;
    public AudioMixer AudioMixer;
    public TextMeshProUGUI[] NumText;
    public Slider[] Slider;

    private void Awake()
    {
        SettingImage.SetActive(false);
    }

    void Update()
    {
        for (int i = 0; i < NumText.Length; i++)
        {
            NumText[i].text = (Mathf.Round(Slider[i].value * 100)).ToString();
            float Volume = Slider[i].value;
            if (i == 0)
            {
                AudioMixer.SetFloat("Music", Mathf.Log10(Volume) * 20);
            }
            else if (i == 1)
            {
                AudioMixer.SetFloat("Sound", Mathf.Log10(Volume) * 20);
            }
        }
    }

    public void StartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Setting()
    {
        bool ActivePanel = !SettingImage.activeSelf;
        SettingImage.SetActive(ActivePanel);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void PremissionDropdown()
    {
        if (PremissionDrop.value == 0)
        {
            Screen.SetResolution(1920, 1080, true);
        }
        else if (PremissionDrop.value == 1)
        {
            Screen.SetResolution(2560, 1080, true);
        }
        else if (PremissionDrop.value == 2)
        {
            Screen.SetResolution(2560, 1440, true);
        }
    }

    public void BackgroundDropdown()
    {
        if (BackgroundDrop.value == 0)
        {
            Panel.sprite = BackgroundPanel[0];
        }
        else if (BackgroundDrop.value == 1)
        {
            Panel.sprite = BackgroundPanel[1]; ;
        }
        else if (BackgroundDrop.value == 2)
        {
            Panel.sprite = BackgroundPanel[2];
        }
    }
}
