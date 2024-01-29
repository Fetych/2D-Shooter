using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Storage : MonoBehaviour, IEnvironmentObject
{
    [SerializeField] EnvironmentObject EnvironmentObject;
    public EnvironmentObject EnvironmentObjects { get => EnvironmentObject; }
    [SerializeField] int IndexCard;
    [SerializeField] private List<GameObject> Object;
    [SerializeField] private GameObject Background;
    [SerializeField] private Image ImageFill;
    [SerializeField] bool Fill;
    public bool Empty;
    [SerializeField] float SerachTime;

    private void Start()
    {
        Background.SetActive(false);
        for (int i = 0; i < Object.Count; i++)
        {
            if (Object[i].GetComponent<AccessCard>() != null)
            {
                Object[i].GetComponent<AccessCard>().AppointmentIndex(IndexCard);
            }
        }
    }

    private void Update()
    {
        if (Fill)
        {
            if(ImageFill.fillAmount < 1)
            {
                ImageFill.fillAmount += Time.deltaTime / SerachTime;
            }
            else
            {
                ImageFill.fillAmount = 0;
            }
        }
    }

    public void OpenStorage(GameObject Player)
    {
        if(Fill == false && Empty == false)
        {            
            Fill = true;
            Background.SetActive(true);
            StartCoroutine(Search(Player));
        }
        else
        {
            Fill = false;
            Empty = true;
            Background.SetActive(false);
            GetComponent<EnvironmentInterface>().InteractionText.enabled = false;
        }
    }
    private IEnumerator Search(GameObject Player)
    {
        Player.GetComponent<Move>().enabled = false;
        Player.GetComponent<Shot>().enabled = false;
        yield return new WaitForSeconds(SerachTime);
        Player.GetComponent<Move>().enabled = true;
        Player.GetComponent<Shot>().enabled = true;
        OpenStorage(Player);
        if(Object.Count > 0)
        {
            while(Object.Count> 0)
            {
                Player.GetComponent<Interaction>().CheckCollectionFacilities(Object[0], false);
                Object.RemoveAt(0);
                Debug.Log(0);
            }
        }
    }
}
