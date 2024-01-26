using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccessCard : MonoBehaviour
{
    [SerializeField] GameObject Card;
    [SerializeField] Image Image;
    [SerializeField] List<Sprite> Sprite;
    [SerializeField] int IndexCard;
    GameObject CardImage;

    private void Start()
    {
        Image.sprite = Sprite[IndexCard];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Infentory>() != null)
        {
            CardImage = Instantiate(Card);
            foreach(Transform child in CardImage.transform)
            {
                child.GetComponent<Image>().sprite = Image.sprite;
            }
            collision.GetComponent<Infentory>().AddInInfentory(CardImage.transform);
            Destroy(gameObject);
        }
    }
}
