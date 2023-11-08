using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] Move Move;
    public List<Collider2D> Ground;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Ground.Add(collision);
        Move.CheckGround = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Ground.Remove(collision);
        if (Ground.Count < 1)
        {
            Move.CheckGround = false;
        }
    }
}
