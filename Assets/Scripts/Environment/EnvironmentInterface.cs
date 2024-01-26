using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnvironmentInterface : MonoBehaviour
{
    public TextMeshProUGUI InteractionText;

    private void Awake()
    {
        InteractionText.enabled = false;
    }
}
