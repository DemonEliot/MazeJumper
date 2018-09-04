using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//[ExecuteInEditMode]
public class BlockManager : MonoBehaviour
{
    public GameObject blockButton;
    public InputField height, width;
    public Texture up, down, left, right, gate, start, end;

    Vector2 size;
    public void GenerateMap()
    {
        RectTransform rect = gameObject.GetComponent<RectTransform>();

        size = new Vector2(int.Parse(width.text), int.Parse(height.text));
        rect.sizeDelta = size;
        GenerateButtons();
    }
    void GenerateButtons()
    {
        for (int w = 0; w <= size.x; w++)
        {
            for (int h = 0; h <= size.y; h++)
            {
                Instantiate(blockButton, new Vector3(w, 0, h), Quaternion.Euler(-90,0,0),transform);
            }
        }
    }
}
