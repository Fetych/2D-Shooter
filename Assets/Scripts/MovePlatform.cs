using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    public List<Transform> targetPositions; // Список точек, до которых нужно двигаться
    public float speed;
    public bool canMove;
    private Vector2 direction = Vector2.up;
    [SerializeField] private int OriginVector;

    private void Awake()
    {
        direction.y *= OriginVector;
    }

    private void Update()
    {
        if (canMove)
        {
            if (direction.y == 1 && transform.position.y >= targetPositions[0].position.y || direction.y == -1 && transform.position.y <= targetPositions[1].position.y )
            {
                Debug.Log(0);
                canMove = false;
                direction = -direction;
            }
            // Двигаем платформу
            transform.Translate(direction * speed * Time.deltaTime);       
        }
    }
}
