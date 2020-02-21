using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class will be attached to the player and crystal, in order to set the tags for the floor underneath them at runtime.
public class NodeAssignDynamic : MonoBehaviour
{
    private void Start()
    {
        Vector2 position = Vector3Extension.AsVector2(this.transform.position);
        if (AllNodes.DoesDictionaryContainKey(position) == false)
        {
            Debug.Log("WARNING! " + this.name + " is not above a node!");
        }
        else
        {
            GameObject node = AllNodes.GetNodeByPosition(position);
            
            switch (this.gameObject.tag)
            {
                case Tags.PLAYER:
                    node.tag = Tags.START;
                    break;
                case Tags.CRYSTAL:
                    node.tag = Tags.END;
                    break;
            }
        }
    }
}
