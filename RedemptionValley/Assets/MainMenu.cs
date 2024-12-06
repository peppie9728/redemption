using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    [Header("Event System")]
    [SerializeField] private EventSystem eventSystem;

    [Header("Main Menu")]
    [SerializeField] private GameObject mainMenuBG;

    [Header("Map Selection")]
    [SerializeField] private GameObject mapSelectionBG;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene(int i)
    {
        SceneManager.LoadScene(i);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
