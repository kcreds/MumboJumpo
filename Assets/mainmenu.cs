using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainmenu : MonoBehaviour
{
    public void play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ABOUT()
    {
        SceneManager.LoadScene("menua");
    }
    public void back()
    {
        SceneManager.LoadScene("menu");
    }
    public void quit()
    {
        Application.Quit();
    }
}
