using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public Text nameBox;
    public Text highScore;

    private MainManager mainManager;
    

    // Start is called before the first frame update
    void Start()
    {
        
        mainManager = GameObject.Find("MainManager").GetComponent<MainManager>();
        nameBox.text = PlayerPrefs.GetString("name");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AppearText()
    {
        if(mainManager)
        {
            mainManager.m_Started = true;
            nameBox.text = PlayerPrefs.GetString("name");
            
        }
    }
}
