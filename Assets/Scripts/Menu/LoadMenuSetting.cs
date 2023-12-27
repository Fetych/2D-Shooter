using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMenuSetting : MonoBehaviour
{
    [SerializeField] private Menu Menu;

    void Awake()
    {
        Menu = GetComponent<Menu>();
        if (PlayerPrefs.HasKey("PlayerSetting"))
        {
            for (int i = 0; i < Menu.Slider.Length; i++)
            {
                Menu.Slider[i].value = PlayerPrefs.GetFloat("AudioSlider" + i);
            }
            Menu.PremissionDrop.value = PlayerPrefs.GetInt("PremissionDropdown");
            Menu.PremissionDropdown();
            Menu.BackgroundDrop.value = PlayerPrefs.GetInt("BackgroundDropdown");
            Menu.BackgroundDropdown();
            //for (int i = 0; i < Menu.Dropdown.Count; i++)
            //{
            //    Menu.Dropdown[0].value = PlayerPrefs.GetInt("PremissionDropdown" + 0);
            //    Menu.PremissionDropdown();
            //}
        }
        else
        {
            for (int i = 0; i < Menu.Slider.Length; i++)
                Menu.Slider[i].value = .50f;
            Menu.PremissionDrop.value = 0;
            Menu.PremissionDropdown();
            Menu.BackgroundDrop.value = 0;
            Menu.BackgroundDropdown();
            //for (int i = 0; i < Menu.Dropdown.Count; i++)
            //    Menu.Dropdown[i].value = 0;
        }
    }
}
