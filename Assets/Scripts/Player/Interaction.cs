using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField] private Interface Interface;
    [SerializeField] private List<GameObject> InteractionObject;
    [SerializeField] private GameObject SelectedObject, TextObject;
    [SerializeField] private Transform ContentTransform;
    [SerializeField] private float TimeMessage;
    private EnvironmentInterface LastEnvironment, SelectedEnvironment;
    GameObject NewObject;
    PlayerControl PlayerControl;
    int EnvironmentIndex;

    private void Awake()
    {
        Interface = FindAnyObjectByType<Interface>();
        PlayerControl = new PlayerControl();
        PlayerControl.Player.Interact.performed += context => EnvironmentObject();
    }
    private void OnEnable()
    {
        PlayerControl.Enable();
    }
    private void OnDisable()
    {
        PlayerControl.Disable();
    }

    private void Update()
    {
        CheckInteraction();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Point>() != null)
        {
            Interface.ScoreNum++;
            Interface.Score.text = Interface.ScoreNum.ToString();
            Destroy(collision.gameObject);
        }
        if(collision.GetComponent<IEnvironmentObject>() != null)
        {
            if (collision.GetComponent<IEnvironmentObject>().EnvironmentObjects == global::EnvironmentObject.Locker)
            {
                if (collision.GetComponent<Storage>().Empty != true)
                    InteractionObject.Add(collision.gameObject);
            }
            else if (collision.GetComponent<IEnvironmentObject>().EnvironmentObjects == global::EnvironmentObject.Turnstile)
            {
               if (collision.GetComponent<Turnstile>().Empty != true)
                    InteractionObject.Add(collision.gameObject);
            }
            else
                InteractionObject.Add(collision.gameObject);
        }
        if(collision.GetComponent<ICollectionFacilities>() != null)
        {
            CheckCollectionFacilities(collision.gameObject, true);
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

    public GameObject CheckSelectedObject(GameObject Selected)
    {
        if(Selected != null)
        {
            if(Selected.GetComponent<IEnvironmentObject>().EnvironmentObjects == global::EnvironmentObject.Locker)
            {
                InteractionObject.Remove(Selected);
                EnvironmentIndex = 0;
                return Selected;
            }
            else if(Selected.GetComponent<IEnvironmentObject>().EnvironmentObjects == global::EnvironmentObject.PCTable)
            {
                EnvironmentIndex = 1;
                return Selected;
            }
            else if (Selected.GetComponent<IEnvironmentObject>().EnvironmentObjects == global::EnvironmentObject.Turnstile)
            {
                EnvironmentIndex = 2;
                return Selected;
            }
        }
        EnvironmentIndex = -1;
        return null;
    }

    void EnvironmentObject()
    {
        GameObject Item = CheckSelectedObject(SelectedObject);
        switch (EnvironmentIndex)
        {
            case 0:
                Item.GetComponent<Storage>().OpenStorage(this.gameObject);
                break;
            case 1:
                Item.GetComponent<PCTable>().OpenPC(GetComponent<Infentory>(), gameObject);
                break;
            case 2:
                Item.GetComponent<Turnstile>().Action(GetComponent<Infentory>());
                break;
        }
    }

    public void CheckCollectionFacilities(GameObject Object, bool CaneDestroy)
    {
        if ((Object.GetComponent<ICollectionFacilities>().CollectionFacilities == CollectionFacilities.Card))
        {
            NewObject = Instantiate(Object.GetComponent<AccessCard>().ImageCard());
            GetComponent<Infentory>().AddInInfentory(NewObject.transform);
            StartCoroutine(SendMessage($"{Object.GetComponent<AccessCard>().TextString}", $"{Object.GetComponent<AccessCard>().IndexCard}"));
        }
        if ((Object.GetComponent<ICollectionFacilities>().CollectionFacilities == CollectionFacilities.Money))
        {
            bool CheckMoney = true;
            for (int i = 0; i < GetComponent<Infentory>().IndexInfentory; i++)
            {
                if (GetComponent<Infentory>().Cell[i].GetComponent<Cell>().CellObject.GetComponent<AmountMoney>() != null)
                {
                    GetComponent<Infentory>().Cell[i].GetComponent<Cell>().CellObject.GetComponent<AmountMoney>().AmountOfMoney += Object.GetComponent<Money>().Amount;
                    CheckMoney = false;
                }
            }
            if (CheckMoney)
            {             
                NewObject = Instantiate(Object.GetComponent<Money>().ImageMoney());
                NewObject.GetComponent<AmountMoney>().AmountOfMoney = Object.GetComponent<Money>().Amount;
                GetComponent<Infentory>().AddInInfentory(NewObject.transform);
            }
            StartCoroutine(SendMessage($"{Object.GetComponent<Money>().TextString}", $"{Object.GetComponent<Money>().Amount}"));
        }
        if(CaneDestroy)
            Destroy(Object.gameObject);
    }

    private IEnumerator SendMessage(string Text, string NumText)
    {
        GameObject Message;
        Message = Instantiate(TextObject, ContentTransform);
        Message.GetComponent<TextMeshProUGUI>().text = Text;
        yield return new WaitForSeconds(.01f);
        if (NumText != null)
        {
            Message.GetComponent<TextMeshProUGUI>().text += NumText;
        }
        yield return new WaitForSeconds(TimeMessage);
        Destroy(Message);
    }
}
