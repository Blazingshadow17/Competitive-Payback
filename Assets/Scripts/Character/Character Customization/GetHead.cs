using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHead : MonoBehaviour
{
    [Header("Sprites to Cycle Through")]
    public Sprite[] sprites;
    private SpriteRenderer mySprite;
    private readonly string selectedHead = "SelectedHead";
    private readonly string colorHead = "ColorHead";

    void Awake()
    {
        mySprite = this.GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        int getHead;
        Color color;

        //Grab saved color
        color = ColorPlayerPrefs.GetSaveColor(colorHead);
        getHead = PlayerPrefs.GetInt(selectedHead);


        //Set Color
        mySprite.color = color;

        int i = 0;

        while (true)
        {
            if (i > sprites.Length - 1) //Check if out of bounds and set default
            {
                mySprite.sprite = sprites[sprites.Length - 1];
                break;
            }

            if (i == getHead) //if the sprite is found, set it
            {
                mySprite.sprite = sprites[i];
                break;
            }
            i++;
        } //Head

    }

}
