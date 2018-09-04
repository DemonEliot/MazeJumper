using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO Combine with other UI scripts
// Also, may get rid of this in the future when I tutorial elements of the game get redesigned.

public class TutorialCanvas : MonoBehaviour {

    public GameObject canvas, text1, text2, text3;
    public GameObject player;
    Vector3 pos;
    bool check = false;
    bool check2 = false;
    float timer = 0;

    void Message2()
    {
        text1.SetActive(false);
        text2.SetActive(true);
    }

    void Message3()
    {
        text2.SetActive(false);
        text3.SetActive(true);
    }

    void Finish()
    {
        canvas.SetActive(false);
    }

    void Start()
    {
        
    }

    void Update()
    {
        // Tracks player position and then shows tutorial messages when the player is in certain locations.
        pos = player.transform.position;

        if (pos.x >= 2.9 && pos.x <= 3.1 && pos.z <= 1.1 && pos.z >= 0.9 &&  !check)
        {
            Message2();
            check = true;
        }

        if (pos.x != 3 && pos.z != 1 && check)
        {
            text2.SetActive(false);
        }

        if (pos.x == 1 && pos.z == 4 && !check2 )
        {
            Message3();
            check2 = true;
        }

        if (pos.x != 1 && pos.z != 4 && check2)
        {
            text3.SetActive(false);
            Finish();
        }
    }
}
