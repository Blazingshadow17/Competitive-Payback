using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColor : MonoBehaviour
{
    [Header("Renderer to Change Color")]
    public SpriteRenderer part;
    void Start()
    {
        Color color = new Color(
            Random.Range(0f, 1f),
            Random.Range(0f, 1f),
            Random.Range(0f, 1f)

        );
        part.color = color;
    }
}
