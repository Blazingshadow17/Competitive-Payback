using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class NameTag : MonoBehaviour
{
    [Header("Text Mesh Name Tag")]
    public TextMeshProUGUI nameTag;

    void Start()
    {
        string name = PlayerPrefs.GetString("name");
        Debug.Log("nameGot=" + name);
        //Choose name if name was entered or force random name if no name was entered
        if (name != "")
        {
            nameTag.text = name;
        }
        else
        {
            nameTag.text = RandomName();
        }
    }
    private string RandomName()
    {
        string path = "Assets/Resources/npc_names.txt";
        List<string> names = new List<string>();
        int nameAmmount = 0;
        int nameIndex;

        StreamReader reader = new StreamReader(path);
        //read names a place the into list until there are no more names to read
        while (reader.ReadLine() != null)
        {
            names.Add(reader.ReadLine());
            nameAmmount++;
        }
        Debug.Log("nameAmmount=" + nameAmmount);

        //Choose the index of a random name
        nameIndex = Random.Range(0, nameAmmount);
        Debug.Log("nameIndex=" + nameIndex);
        //grab the name and set the name tag
        string name = names[nameIndex];
        return name;
    }
}
