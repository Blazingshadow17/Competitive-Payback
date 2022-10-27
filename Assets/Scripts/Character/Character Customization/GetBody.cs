using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetBody : MonoBehaviour
{
    [Header("Sprites to Cycle Through")]
    public Sprite[] sprites;
    private SpriteRenderer mySprite;
    private readonly string selectedBody = "SelectedBody";
    private readonly string colorBody = "ColorBody";

    void Awake()
    {
        mySprite = this.GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        int getBody;
        Color color;

        //Grab saved color
        color = ColorPlayerPrefs.GetSaveColor(colorBody);
        getBody = PlayerPrefs.GetInt(selectedBody);

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

            if (i == getBody) //if the sprite is found, set it
            {
                mySprite.sprite = sprites[i];
                break;
            }
            i++;
        } //Head

    }

}
