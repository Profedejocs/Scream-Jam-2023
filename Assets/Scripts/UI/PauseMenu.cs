using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PauseMenu : MonoBehaviour
{
    GameObject pauseUI;
    GameObject background;
    GameObject settingUI;

    public bool isPaused = false;
    
    void Start()
    {
        pauseUI = gameObject.transform.Find("Top").gameObject;
        pauseUI.SetActive(false);
        settingUI = gameObject.transform.Find("Settings").gameObject;
        settingUI.SetActive(false);
        background = gameObject.transform.Find("Background").gameObject;
        background.SetActive(false);
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(!isPaused)
            {
                pause();
            }
            else
            {
                resume();
            }
        }
    }

    public void pause()
    {
        Time.timeScale = 0;
        pauseUI.SetActive(true);
        settingUI.SetActive(false);
        background.SetActive(true);
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void resume()
    {
        Time.timeScale = 1;
        pauseUI.SetActive(false);
        settingUI.SetActive(false);
        background.SetActive(false);
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void settings()
    {
        pauseUI.SetActive(false);
        settingUI.SetActive(true);
    }

    public void back()
    {
        pauseUI.SetActive(true);
        settingUI.SetActive(false);
    }

    public void mainMenu()
    {
        Time.timeScale = 1;
        isPaused = false;
        SceneManager.LoadScene(0);
    }

    public void quit()
    {
        Application.Quit();

        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #endif
    }
}
