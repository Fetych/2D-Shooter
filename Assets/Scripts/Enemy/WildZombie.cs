using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildZombie : MonoBehaviour
{
    [SerializeField] private float Distance;
    public LayerMask layerMask;

    void Update()
    {
        // �������� ����� �������
        Vector2 origin = transform.position;

        // ������� ������ ����������� ����
        Vector2 direction = Vector2.right * transform.localScale.x;

        // ��������� ��� �� ������ ������� � ��������� �����������
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, Distance, layerMask);

        //���� ��� ���������� collider �������
        if (hit.collider != null)
        {
            // ��������� ������� ������� �� �������
            //SampleScript sampleScript = hit.collider.gameObject.GetComponent<SampleScript>();

            if (hit.collider.GetComponent<Stats>() != null && hit.collider.GetComponent<Stats>().LivingOrganisms == LivingOrganisms.Player)
            {
                // ��� ���, ������� ����� ���������, ���� ������ ������
                Debug.Log("������ ������ �� ������� " + hit.collider.gameObject.name);
            }
        }

        // ������������ ���� � Scene
        Debug.DrawRay(transform.position, direction * Distance, Color.red);
    }
}
