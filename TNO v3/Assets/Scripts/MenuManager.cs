using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    //Start button
    public void StartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //Credits button
    public void CreditButton()
    {
        
        SceneManager.LoadScene(7);
    }

    //Back to menu
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
