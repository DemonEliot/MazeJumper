using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{

    public GameObject portal;
    public GameObject gate;

    public void InstantiatePortal(Vector3 position, bool upDown)
    {
        portal.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        Vector3 newPosition = new Vector3(position.x, 1.5f, position.z);
        Quaternion rotation;
        if (upDown)
        {
            rotation = Quaternion.Euler(0, 90, 90);
        }
        else
        {
            rotation = Quaternion.Euler(0, 0, 90);
        }

        Instantiate(portal, newPosition, rotation);
    }

    public void InstantiateGate(Vector3 position)
    {
        gate.transform.localScale = new Vector3(1, 1, 1);
        Vector3 newPosition = new Vector3(position.x, 0.6f, position.z);
        Quaternion rotation = Quaternion.Euler(-90, -180, 0);
        Instantiate(gate, newPosition, rotation);
    }
}
