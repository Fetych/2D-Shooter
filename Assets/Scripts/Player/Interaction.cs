using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField] private Interface Interface;

    private void Awake()
    {
        Interface = FindAnyObjectByType<Interface>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.GetComponent<Point>() != null)
        {
            Interface.ScoreNum++;
            Interface.Score.text = Interface.ScoreNum.ToString();
            Destroy(collider.gameObject);
        }
    }
}
