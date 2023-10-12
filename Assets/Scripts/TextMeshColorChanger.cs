using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextMeshColorChanger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;

    private Color _initColor;
    [SerializeField] private Color[] _colorArray;

    private void Start()
    {
        if (textMesh == null)
        {
            // If the TextMeshPro component is not assigned, try to find it on the same GameObject
            textMesh = GetComponent<TextMeshProUGUI>();
        }

        _initColor = textMesh.color;
    }

    public void SetColorByIndex(int index)
    {
        textMesh.color = _colorArray[index];
    }

    public void SetColorToInit()
    {
        textMesh.color = _initColor;
    }

}
