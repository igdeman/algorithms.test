using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Block : MonoBehaviour
{
    public RectTransform rectTransform { get => (RectTransform)transform; }
    public Image image { get => gameObject.GetComponent<Image>(); }
    public int C;
    public int R;
    public int I;

    private bool isOpen;
    public bool IsOpen
    {
        get => isOpen;
        set
        {
            isOpen = value;
            image.color = (isOpen) ? new Color(1, 0, 0) : new Color();
        }
    }
}
