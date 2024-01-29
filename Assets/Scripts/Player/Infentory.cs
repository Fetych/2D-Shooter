using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Infentory : MonoBehaviour
{
    PlayerControl PlayerControl;
    bool InfentoryOpen;
    public int IndexInfentory;
    [SerializeField] GameObject InfentoryObject;
    public List<GameObject> Cell;
    [SerializeField] Stats Stats;
    [SerializeField] TextMeshProUGUI HP, Armor, Damage;

    private void Awake()
    {
        PlayerControl = new PlayerControl();
        PlayerControl.Player.Infentory.performed += context => CheckInfentory();
    }
    private void OnEnable()
    {
        PlayerControl.Enable();
    }
    private void OnDisable()
    {
        PlayerControl.Disable();
    }
    private void Start()
    {
        HP.text = Stats.MaxHealth.ToString();
        Armor.text = Stats.Armor.ToString();
        Damage.text = Stats.Damage.ToString();
        InfentoryObject.SetActive(false);
        for (int i = 0; i < Cell.Count; i++)
        {
            if (Cell[i].GetComponent<Cell>().CellObject != null)
            {
                IndexInfentory = i;
                break;
            }
        }
    }
    public void AddInInfentory(Transform Object)
    {
        Object.SetParent(Cell[IndexInfentory].transform);
        Object.localPosition = new Vector3(0, 0, 0);
        //Cell[IndexInfentory].GetComponent<Cell>().TheCellIsOccupied = true;
        Cell[IndexInfentory].GetComponent<Cell>().CellObject = Object.gameObject;
        IndexInfentory++;
    }
    void CheckInfentory()
    {
        InfentoryOpen = !InfentoryOpen;
        InfentoryObject.SetActive(InfentoryOpen);
        if (InfentoryOpen)
        {
            Time.timeScale = 0;
            GetComponent<Move>().enabled = false;
        }
        else
        {
            Time.timeScale = 1;
            GetComponent<Move>().enabled = true;
        }
    }
}
