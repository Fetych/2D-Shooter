using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour, ICollectionFacilities
{
    [SerializeField] CollectionFacilities CollectionFacilitie;
    public CollectionFacilities CollectionFacilities { get => CollectionFacilitie; }
    [SerializeField] GameObject MoneySprite;
    public int Amount;
    public string TextString;
    GameObject MoneyImage;

    public GameObject ImageMoney()
    {
        MoneyImage = Instantiate(MoneySprite);
        return MoneyImage;
    }
}
