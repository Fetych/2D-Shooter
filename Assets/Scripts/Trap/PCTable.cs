using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PCTable : MonoBehaviour, IEnvironmentObject
{
    [SerializeField] EnvironmentObject EnvironmentObject;
    public EnvironmentObject EnvironmentObjects { get => EnvironmentObject; }
    [SerializeField] private GameObject PCImage;
    [SerializeField] private int CardIndex;
    [SerializeField] private GameObject Trap;
    [SerializeField] private List<ElectricTrap> ElectricTraps;
    [SerializeField] private List<int> IndexCards;
    public GameObject Player;

    private void Start()
    {
        ElectricTraps.Add(Trap.GetComponentInChildren<ElectricTrap>());
        PCImage.SetActive(false);
    }

    public void OpenPC(Infentory infentory, GameObject Player)
    {
        PCImage.SetActive(true);
        this.Player = Player;
        Player.GetComponent<Move>().enabled = false;
        Player.GetComponent<Shot>().enabled = false;
        for (int i = 0; i < infentory.Cell.Count; i++)
        {
            if (infentory.Cell[i].GetComponent<Cell>().CellObject != null && infentory.Cell[i].GetComponent<Cell>().CellObject.GetComponent<CardUIInfentory>() != null)
            {
                IndexCards.Add(infentory.Cell[i].GetComponent<Cell>().CellObject.GetComponent<CardUIInfentory>().IndexCard);
            }
        }
    }

    public void CloseTable()
    {
        PCImage.SetActive(false);
        IndexCards.Clear();
        Debug.Log(gameObject);
        Debug.Log(Player);
        Player.GetComponent<Move>().enabled = true;
        Player.GetComponent<Shot>().enabled = true;
        Player = null;
    }

    public void CompletingTask()
    {
        if(IndexCards.Count > 0)
        {
            for (int i = 0; i < IndexCards.Count; i++)
            {
                if (CardIndex == IndexCards[i])
                {
                    Debug.Log(0);
                    while (ElectricTraps.Count > 0)
                    {
                        ElectricTraps[0].OffTrap();
                        ElectricTraps.RemoveAt(0);
                    }
                    break;
                }
            }
        }
    }
}
