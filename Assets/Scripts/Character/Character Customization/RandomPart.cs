using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RandomPart : MonoBehaviour
{
    [Header("Renderer to Change Part")]
    public SpriteRenderer part;

    [Header("Sprites to Cycle Through")]
    public List<Sprite> options = new List<Sprite>();

    void Start()
    {
        int randomOption = Random.Range(0, options.Count - 1);
        part.sprite = options[randomOption];
    }

}
