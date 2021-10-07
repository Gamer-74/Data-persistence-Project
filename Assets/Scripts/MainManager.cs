using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;

    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text highScore;
    public GameObject GameOverText;

    string m_playerNames;
    
    public bool m_Started = false;
    private int m_Points;
    private int currentScore;
    private int m_highScore;
    
    public bool m_GameOver = false;

    
    // Start is called before the first frame update
    void Start()
    {
        highScore.text = PlayerPrefs.GetInt("HighScore", m_Points).ToString();
        Instance = this;
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();               

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);

                UpdateScore();
                GameOverText.SetActive(false);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else if(Input.GetKeyDown(KeyCode.Z))
            {
                SceneManager.LoadScene(0);
                Destroy(gameObject);
            }
        }
    }

    public void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {PlayerPrefs.GetString("name")}  {m_Points}";
    }

    public int UpdateScore()
    {
        int number = m_Points;
        ScoreText.text = number.ToString();

        if (number > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", number);
            
        }
        return number;
    }

    public void GameOver()
    {
        
        if (m_Started)
        {
           m_GameOver = true;
           highScore.text = $"Score : {PlayerPrefs.GetString("name")}  {m_Points}";
           PlayerPrefs.SetInt("HighScore", m_Points);
           GameOverText.SetActive(true);
        }
    }

    public string SetNames(string PlayerNames)
    {
        string cName = SaveNames.theName;
        cName = m_playerNames;
        m_playerNames = PlayerNames;
        return m_playerNames;
    }

    public void SaveScore()
    {
        ScoreText.text = currentScore.ToString();
        if(PlayerPrefs.GetInt("HighScore", m_Points) < currentScore)
        {
            highScore.text = m_highScore.ToString();
            PlayerPrefs.SetInt("HighScore", m_highScore);
        }

    }
}
