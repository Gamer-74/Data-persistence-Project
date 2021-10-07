using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class SaveNames : MonoBehaviour
{
    public InputField textBox;

    //public static SaveNames Instance;

    public static string theName;

    public GameObject inputField;
    //public GameObject textDisplay;

    // Start is called before the first frame update
    void Awake()
    {
        //Instance = this;
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(transform.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public string SetNames(string theName)
    {
        theName = inputField.GetComponent<Text>().text;
        textBox.GetComponent<Text>().text = theName;
        
        DontDestroyOnLoad(gameObject);
        return theName;
    }

    public void ClickSaveButton()
    {
        PlayerPrefs.SetString("name", textBox.text);
        Debug.Log("Your Name is " + PlayerPrefs.GetString("name"));
    }

    [System.Serializable]
    
    class SaveData
    {
        public string theName;
    }

    public void SaveName()
    {
        SaveData data = new SaveData();
        data.theName = theName;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadName()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            theName = data.theName;
        }
    }

}
