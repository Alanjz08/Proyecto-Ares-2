using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public Animator settingsanim;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void StartGame()
    {
       
        SceneManager.LoadScene(1);
    }

    public void ShowSettings()
    {
        settingsanim.SetBool("ShowOpciones",true);
    }

    public void HideSettings()
    {
        settingsanim.SetBool("ShowOpciones", false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
