using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventController : MonoBehaviour
{
    public GameObject pauseMenu;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (pauseMenu.activeSelf)
            {
                pauseMenu.SetActive(false);
                Time.timeScale = 1f;
            }
            else
            {
                pauseMenu.SetActive(true);
                Time.timeScale = 0f;
            }     
        }
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
   public  void Menu()
    {
        SceneManager.LoadScene(0);

    }
   public  void Restart()
    {
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeSceneIndex);
        // SceneManager.LoadScene(1);
    }
    public void Play()
    {
        anim.SetTrigger("goo");
        Invoke("LoadScene", 1.1f);
        
    }
    void LoadScene()
    {
        SceneManager.LoadScene(1);
    }
    
    public void quit()
    {
        
        Application.Quit();
    }
}
