using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//TODO Combine with other UI scripts

public class MainMenu : MonoBehaviour
{
    public GameObject levelButtonPrefab;
    public GameObject levelButtonContainer;

    // I was just expirementing with loading sprites from the Resource Folder and applying it dynamically to the UI objects in scene.

    private void Start()
    {
        Sprite[] thumbnails = Resources.LoadAll<Sprite>("Levels");
        foreach(Sprite thumbnail in thumbnails)
        {
            GameObject container = Instantiate(levelButtonPrefab) as GameObject;
            container.GetComponent<Image>().sprite = thumbnail;
            container.transform.SetParent(levelButtonContainer.transform, false);
        }
    }
}
