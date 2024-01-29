using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turnstile : MonoBehaviour, IEnvironmentObject
{
    [SerializeField] EnvironmentObject EnvironmentObject;
    public EnvironmentObject EnvironmentObjects { get => EnvironmentObject; }
    public bool Empty;
    [SerializeField] private int IndexCard;
    [SerializeField] private GameObject Door, MovePlatform;
    bool NeedCard;

    public void Action(Infentory infentory)
    {
        for (int i = 0; i < infentory.Cell.Count; i++)
        {
            if (infentory.Cell[i].GetComponent<Cell>().CellObject != null && infentory.Cell[i].GetComponent<Cell>().CellObject.GetComponent<CardUIInfentory>() != null)
            {
                if (IndexCard == infentory.Cell[i].GetComponent<Cell>().CellObject.GetComponent<CardUIInfentory>().IndexCard)
                {
                    NeedCard = true;
                    break;
                }
            }
        }
        if (NeedCard)
        {
            if (Door != null)
            {
                Door.SetActive(false);
                Empty = true;
            }
            else if (MovePlatform != null)
            {
                Debug.Log(0);
                MovePlatform.GetComponent<MovePlatform>().canMove = true;
            }
        }


    }
}
