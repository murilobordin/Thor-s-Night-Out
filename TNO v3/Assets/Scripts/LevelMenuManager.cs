using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelMenuManager : MonoBehaviour
{
    [SerializeField]
    private int levelPass;
    private GameObject btLevel2, btLevel3, btLevel4, btLevel5;

    // Start is called before the first frame update
    void Start()
    {
        levelPass = PlayerPrefs.GetInt("levelPassed");
        Debug.Log(levelPass);

        btLevel2 = GameObject.FindWithTag("Button2");
        btLevel3 = GameObject.FindWithTag("Button3");
        btLevel4 = GameObject.FindWithTag("Button4");
        btLevel5 = GameObject.FindWithTag("Button5");

        btLevel2.transform.GetChild(1).gameObject.SetActive(false);
        btLevel3.transform.GetChild(1).gameObject.SetActive(false);
        btLevel4.transform.GetChild(1).gameObject.SetActive(false);
        btLevel5.transform.GetChild(1).gameObject.SetActive(false);

        switch (levelPass)
        {
            case 1:
                btLevel2.transform.GetChild(1).gameObject.SetActive(true);
                break;

            case 2:
                btLevel2.transform.GetChild(1).gameObject.SetActive(true);
                btLevel3.transform.GetChild(1).gameObject.SetActive(true);
                break;

            case 3:
                btLevel2.transform.GetChild(1).gameObject.SetActive(true);
                btLevel3.transform.GetChild(1).gameObject.SetActive(true);
                btLevel4.transform.GetChild(1).gameObject.SetActive(true);
                break;

            case 4:
                btLevel2.transform.GetChild(1).gameObject.SetActive(true);
                btLevel3.transform.GetChild(1).gameObject.SetActive(true);
                btLevel4.transform.GetChild(1).gameObject.SetActive(true);
                btLevel5.transform.GetChild(1).gameObject.SetActive(true);
                break;
        }
    }

    public void LoadLevelOne()
    {
        SceneManager.LoadScene("Level1");
    }

    public void LoadLevelTwo()
    {
        SceneManager.LoadScene("Level2");
    }

    public void LoadLevelThree()
    {
        SceneManager.LoadScene("Level3");
    }

    public void LoadLevelFour()
    {
        SceneManager.LoadScene("Level4");
    }

    public void LoadLevelFive()
    {
        SceneManager.LoadScene("Level5");
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
