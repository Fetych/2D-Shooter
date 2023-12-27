using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextAS : MonoBehaviour
{
    [SerializeField] private LocalizationText textA;

    public void Start()
    {
        textA.Localize("Tutor_Key");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
