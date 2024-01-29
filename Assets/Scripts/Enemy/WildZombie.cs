using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildZombie : MonoBehaviour
{
    [SerializeField] private float Distance;
    public LayerMask layerMask;

    void Update()
    {
        // Получаем центр объекта
        Vector2 origin = transform.position;

        // Создаем вектор направления луча
        Vector2 direction = Vector2.right * transform.localScale.x;

        // Выпускаем луч из центра объекта в указанном направлении
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, Distance, layerMask);

        //Если луч пересекает collider объекта
        if (hit.collider != null)
        {
            // Проверяем наличие скрипта на объекте
            //SampleScript sampleScript = hit.collider.gameObject.GetComponent<SampleScript>();

            if (hit.collider.GetComponent<Stats>() != null && hit.collider.GetComponent<Stats>().LivingOrganisms == LivingOrganisms.Player)
            {
                // Ваш код, который нужно выполнить, если скрипт найден
                Debug.Log("Скрипт найден на объекте " + hit.collider.gameObject.name);
            }
        }

        // Визуализация луча в Scene
        Debug.DrawRay(transform.position, direction * Distance, Color.red);
    }
}
