using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locker : MonoBehaviour, IEnvironmentObject
{
    [SerializeField] EnvironmentObject EnvironmentObject;
    public EnvironmentObject EnvironmentObjects { get => EnvironmentObject; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
