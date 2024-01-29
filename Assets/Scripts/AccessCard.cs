using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccessCard : MonoBehaviour, ICollectionFacilities
{
    [SerializeField] CollectionFacilities CollectionFacilitie;
    public CollectionFacilities CollectionFacilities { get => CollectionFacilitie; }
    public string TextString;
    public int IndexCard;
    [SerializeField] GameObject Card;
    [SerializeField] Image Image;
    [SerializeField] List<Sprite> Sprite;
    
    GameObject CardImage;

    private void Start()
    {
        AppointmentIndex(IndexCard);
    }

    public void AppointmentIndex(int Index)
    {
        Image.sprite = Sprite[Index];
    }

    public GameObject ImageCard()
    {
        CardImage = Instantiate(Card);
        CardImage.GetComponent<CardUIInfentory>().IndexCard = IndexCard;
        foreach (Transform child in CardImage.transform)
        {
            child.GetComponent<Image>().sprite = Image.sprite;
        }
        return CardImage;
    }
}
