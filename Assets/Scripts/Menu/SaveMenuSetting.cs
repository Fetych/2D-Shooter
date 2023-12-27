using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaveMenuSetting : MonoBehaviour
{
    public Menu Menu;

    private void Awake()
    {
        Menu = GetComponent<Menu>();
    }

    public void SaveSliderSetting(int Index)
    {
        PlayerPrefs.SetFloat("AudioSlider" + Index, Menu.Slider[Index].value);
        Save(Menu.Slider[Index].value);
    }
    public void SavePermissionDropdown(int Index)
    {
        Menu.PremissionDropdown();
        PlayerPrefs.SetInt("PremissionDropdown", Menu.PremissionDrop.value);
        Save(Menu.PremissionDrop.value);
    }
    public void SaveBackgroundDropdown(int Index)
    {
        Menu.BackgroundDropdown();
        PlayerPrefs.SetInt("BackgroundDropdown", Menu.BackgroundDrop.value);
        Save(Menu.BackgroundDrop.value);
    }
    void Save(float Value)
    {
        PlayerPrefs.SetFloat("PlayerSetting", Value);
    }
}
