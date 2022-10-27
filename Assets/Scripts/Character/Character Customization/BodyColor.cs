using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; //for text

public class BodyColor : MonoBehaviour
{
    [Header("Text Mesh to Fill")]
    public TextMeshProUGUI DebugText;
    [Header("Object to Preview Color")]
    public ColorEvent OnColorPreview;
    [Header("Object to Change Color")]
    public ColorEvent OnColorSelect;
    [Header("Color Picker to Disable on Click")]
    public GameObject picker;

    RectTransform Rect; //Every UI object has rect transform
    Texture2D ColorTexture;
    void Start()
    {
        Rect = GetComponent<RectTransform>(); //Caching

        ColorTexture = GetComponent<Image>().mainTexture as Texture2D;
    }

    // Update is called once per frame
    void Update()
    {
        //fix outside select click
        if (RectTransformUtility.RectangleContainsScreenPoint(Rect, Input.mousePosition, Camera.main))
        {
            //Getting Screen coords
            Vector2 delta;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(Rect, Input.mousePosition, Camera.main, out delta); //grabs point in screen and converts it into point within smaller rect. Pass in Rect, Position, Camera (can be null if in overlay), and vector
            //Make origin in bottom left (Rect Coords)
            float width = Rect.rect.width;
            float height = Rect.rect.height;
            delta += new Vector2(width * .5f, height * .5f);

            //covert coords 0 to 1
            float x = Mathf.Clamp(delta.x / width, 0f, 1f);
            float y = Mathf.Clamp(delta.y / height, 0f, 1f);

            //Convert coordinates into original texture coordinates of picture
            int texX = Mathf.RoundToInt(x * ColorTexture.width);
            int texY = Mathf.RoundToInt(y * ColorTexture.height);

            //Get Color
            Color color = ColorTexture.GetPixel(texX, texY);

            //Debug Text within a text emsh pro object
            /*string debug = "mousePosition=" + Input.mousePosition;
            debug += "<br>delta=" + delta;
            debug += "<br>x=" + x + ", y=" + y;
            debug += "<br>texX=" + texX + ", texY=" + texY;

            DebugText.text = debug;
            DebugText.color = color;*/

            //Call color event items / set color
            OnColorPreview?.Invoke(color);

            if (Input.GetMouseButtonDown(0))
            {
                //changes the color of the object
                OnColorSelect?.Invoke(color);
                //turns off slector on click
                picker.SetActive(false);
                //saves color to be grabbed later
                ColorPlayerPrefs.SaveColor(color, "ColorBody");
            }
        }//if
        //For on Select even, drag object in, go to function -> SpriteRenderer -> Color
    }
    public void RandomColor()
    {

    }
}
