using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchObject : MonoBehaviour, IObjInteractionTarget
{
    [Header("Objects")]

    public bool Bagger;

    public GameObject Excavator;
    public GameObject Truck;

    public GameObject Excavator2;
    public GameObject Truck2;

    void OnEnable()
    {
        foreach (Renderer r in this.GetComponentsInChildren<Renderer>())
        {
            r.enabled = true;
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void targetInteract(int v)
    {
        if (Bagger)
        {
            Excavator.SetActive(true);
            Excavator2.SetActive(true);

            Truck.SetActive(false);
            Truck2.SetActive(false);
        }
        else
        {
            Excavator.SetActive(false);
            Excavator2.SetActive(false);

            Truck.SetActive(true);
            Truck2.SetActive(true);
        }
    }

   
}