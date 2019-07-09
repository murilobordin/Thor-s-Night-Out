using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    
    [SerializeField]
    private Text scoreHud;
    public GameObject pauseMenu, winMenu, loseMenu, dieMenu;
    public float score, enemySpeed;
    public enum GameStatus {WIN, LOSE, DIE, PLAY };
    public GameStatus status;
    public bool pressJump = false;
    [SerializeField]
    private float coinsOnScene;
    private int levelPassed;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        status = GameStatus.PLAY;
        Physics2D.IgnoreLayerCollision(9, 10, false);
        Physics2D.IgnoreLayerCollision(8, 10, false);
        pauseMenu.SetActive(false);
        winMenu.SetActive(false);
        loseMenu.SetActive(false);
        dieMenu.SetActive(false);
        Time.timeScale = 1;
        levelPassed = PlayerPrefs.GetInt("levelPassed");

        if (levelPassed > 4)
            levelPassed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (status == GameStatus.PLAY)
        {
            scoreHud.text = score.ToString() + " / " + coinsOnScene.ToString();
        }
        
    }

    public void SetStatus(GameStatus parStatus)
    {
        status = parStatus;
    }

    public void PauseGameOn()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void PauseGameOff()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void RestartButton()
    { 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ContinueButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        if (levelPassed < 4) {
            levelPassed++;
            SaveGame();
        }
        
    }

    private void SaveGame()
    {
        PlayerPrefs.SetInt("levelPassed", levelPassed);
        PlayerPrefs.Save();
        Debug.Log("Game Saved");
    }

    
}
