using UnityEngine;
using UnityEngine.UI;

public class MenuHandling : MonoBehaviour
{
    //UI elements
    [SerializeField]
    private GameObject MenuPanel;
    [SerializeField]
    private GameObject ControlsPanel;
    [SerializeField]
    private GameObject PauseBtn;

    //Opens pause menu
    public void Pause()
    {
        PauseBtn.SetActive(false);
        MenuPanel.SetActive(true);
        Time.timeScale = 0.0f;
    }

    //Closes pause menu
    public void UnPause()
    {
        PauseBtn.SetActive(true);
        MenuPanel.SetActive(false);
        ControlsPanel.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void Controls()
    {
        ControlsPanel.SetActive(true);
    }

    public void ExitControls()
    {
        ControlsPanel.SetActive(false);
    }

    //Closes the application
    public void Quit()
    {
        Application.Quit();
    }
}
