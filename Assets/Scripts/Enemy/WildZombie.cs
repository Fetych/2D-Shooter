using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildZombie : MonoBehaviour
{
    [SerializeField] private float Distance;
    public LayerMask layerMask;

    void Update()
    {
        if(GetComponent<Health>().Dead != true)
        {
            // ������� ������ ����������� ����
            Vector2 direction = Vector2.right * transform.localScale.x;

            // ��������� ��� �� ������ ������� � ��������� �����������
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, Distance, layerMask);

            //���� ��� ���������� collider �������
            if(GetComponent<EnemyMove>().Check == false)
            {
                if (hit.collider != null)
                {
                    //Debug.Log(hit.collider.name);
                    if (hit.collider.GetComponent<Stats>() != null && hit.collider.GetComponent<Stats>().LivingOrganisms == LivingOrganisms.Player && GetComponent<EnemyMove>().Check == false) // && GetComponent<EnemyMove>().currentState != 3)
                    {
                        // ��� ���, ������� ����� ���������, ���� ������ ������
                        GetComponent<EnemyMove>().Target = true;
                        //Debug.Log("������ ������ �� ������� " + hit.collider.gameObject.name);
                        if (GetComponent<EnemyMove>().Target && GetComponent<EnemyMove>().Opponent.Count == 0)
                        {
                            GetComponent<EnemyMove>().currentState = 4;
                        }
                        else if (GetComponent<EnemyMove>().Target && GetComponent<EnemyMove>().Opponent.Count != 0)
                        {
                            GetComponent<EnemyMove>().currentState = 3;
                        }
                    }
                    else
                    {
                        GetComponent<EnemyMove>().Target = false;
                        if (GetComponent<EnemyMove>().Check == false)
                        {
                            GetComponent<EnemyMove>().currentState = 1;
                        }
                    }

                }
                else
                {
                    GetComponent<EnemyMove>().Target = false;
                    if (GetComponent<EnemyMove>().Check == false)
                    {
                        GetComponent<EnemyMove>().currentState = 1;
                    }
                }
            }
            
            // ������������ ���� � Scene
            Debug.DrawRay(transform.position, direction * Distance, Color.red);

        }
    }

    //public void Attack()
    //{
    //    if(GetComponent<EnemyMove>().Opponent.Count != 0)
    //    {
    //        for(int i = 0; i < GetComponent<EnemyMove>().Opponent.Count; i++)
    //        {
    //            GetComponent<EnemyMove>().Opponent[0].GetComponent<Health>().Damage(GetComponent<Stats>().Damage);
    //        }
    //    }
    //}
}
