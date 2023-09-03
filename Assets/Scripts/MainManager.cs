using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance { get; private set; }
    public Color TeamColor;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadColor();
    }

    //you create a class that has data to fill in for a save file
    //its a template for your save file and the various fields within the save file
    [System.Serializable]
    class SaveData
    {
        public Color TeamColor;
    }

    //save the save file
    public void SaveColor()
    {
        //create a new instance of the class SaveData "template"
        SaveData data = new SaveData();

        //fill in the blanks for the save attributes - in this case, TeamColor
        data.TeamColor = TeamColor;

        //convert the class instance (SaveData data) to a JSON
        string json = JsonUtility.ToJson(data);

        //write the json string to a file in the specified path (the last json in the method is the string above: the actual savedata content string)
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    //load the save file (this is just the reverse process of the save file)
    public void LoadColor()
    {
        //define the path
        string path = Application.persistentDataPath + "/savefile.json";

        //if the file exists load it
        if (File.Exists(path))
        {
            //recreate the save string from the file
            string json = File.ReadAllText(path);

            //recreate the SaveData class instance from the string
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            //assign the color to the in game public variable
            TeamColor = data.TeamColor;
        }
    }
}
