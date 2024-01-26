using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PCTable : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI InteractionText;

    private void Awake()
    {
        InteractionText.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<ILivingOrganisms>() != null && collision.GetComponent<ILivingOrganisms>().LivingOrganisms == LivingOrganisms.Player)
        {
            InteractionText.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<ILivingOrganisms>() != null && collision.GetComponent<ILivingOrganisms>().LivingOrganisms == LivingOrganisms.Player)
        {
            InteractionText.enabled = false;
        }
    }
}
