using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]

public class LocalizationText : MonoBehaviour
{
    public TextMeshProUGUI text;
    private string key;

    public void Start()
    {
        LocalizeText();
    }

    public void LocalizeText()
    {
        Localize();
        LocalizationManager.OnLanguageChange += OnLanguageChange;
    }

    private void OnDestroy()
    {
        LocalizationManager.OnLanguageChange -= OnLanguageChange;
    }

    private void OnLanguageChange()
    {
        Localize();
    }


    void Init()
    {
        text = GetComponent<TextMeshProUGUI>();
        key = text.text;
    }

    public void Localize(string newKey = null)
    {
        if(text == null)
            Init();
        if (newKey != null)
            key = newKey;
        text.text = LocalizationManager.GetTranslate(key);
    }
}
