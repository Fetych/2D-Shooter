using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField] private Interface Interface;
    [SerializeField] private List<GameObject> InteractionObject;
    [SerializeField] private GameObject SelectedObject;
    //[SerializeField] private TextMeshProUGUI SelectedText;
    [SerializeField] EnvironmentInterface LastEnvironment, SelectedEnvironment;

    private void Awake()
    {
        Interface = FindAnyObjectByType<Interface>();
    }

    private void Update()
    {
        CheckInteraction();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Point>() != null)
        {
            Interface.ScoreNum++;
            Interface.Score.text = Interface.ScoreNum.ToString();
            Destroy(collision.gameObject);
        }
        if(collision.GetComponent<IEnvironmentObject>() != null)
        {
            InteractionObject.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<IEnvironmentObject>() != null)
        {
            InteractionObject.Remove(collision.gameObject);
        }
    }

    void CheckInteraction()
    {
        if (InteractionObject.Count != 0)
        {
            CheckDistance();
            CheckObject();
        }
        else if(SelectedObject != null)
        {
            SelectedObject = null;
            SetSelected(null);
        }
    }

    void CheckDistance()
    {
        float Distance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (GameObject obj in InteractionObject)
        {
            float distance = Vector3.Distance(obj.transform.position, currentPosition);
            if (distance < Distance)
            {
                Distance = distance;
                SelectedObject = obj;
            }
        }
    }
    
    void CheckObject()
    {
        if(SelectedObject.TryGetComponent(out EnvironmentInterface environmentInterface))
        {
            if (environmentInterface != LastEnvironment)
            {
                SetSelected(environmentInterface);
            }
        }
        else
        {
            SetSelected(null);
        }
    }

    void SetSelected(EnvironmentInterface EnvironmentInterface)
    {
        if (EnvironmentInterface == SelectedEnvironment)
        {
            SelectedEnvironment.InteractionText.enabled = true;
        }
        else if(SelectedEnvironment != null)
        {
            SelectedEnvironment.InteractionText.enabled = false;
        }
        SelectedEnvironment = EnvironmentInterface;
    }
}
