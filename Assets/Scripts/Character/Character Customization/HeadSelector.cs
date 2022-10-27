using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HeadSelector : MonoBehaviour
{
    [Header("Sprite to Change")]
    public SpriteRenderer head;

    [Header("Sprites to Cycle Through")]
    public List<Sprite> options = new List<Sprite>();

    private int currentOption = 0;
    private readonly string selectedHead = "SelectedHead";

    public void NextOption()
    {
        currentOption++;
        if (currentOption >= options.Count)
        {
            currentOption = 0; //Reset if cycled through the list
        }

        head.sprite = options[currentOption];
        //save Sprite
        PlayerPrefs.SetInt(selectedHead, currentOption);

    }

    public void PreviousOption()
    {
        currentOption--;
        if (currentOption < 0)
        {
            currentOption = options.Count - 1; //Reset if cycled through the list
        }

        head.sprite = options[currentOption];
        //save Sprite
        PlayerPrefs.SetInt(selectedHead, currentOption);
    }

    public void RandomOption()
    {
        int randomOption = Random.Range(0, options.Count - 1);
        //don't allow repeats
        while (randomOption == currentOption)
        {
            randomOption = Random.Range(0, options.Count - 1);
        }
        currentOption = randomOption;
        head.sprite = options[currentOption];
        //save Sprite
        PlayerPrefs.SetInt(selectedHead, currentOption);
    }

    public void Submit()
    {
        //Save C
        SceneManager.LoadScene("2DTopDown");
    }

}
