using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Unity.VisualScripting;


public class MainMenu : MonoBehaviour
{
    [Header("Event System")]
    [SerializeField] private EventSystem eventSystem;

    [Header("Main Menu")]
    [SerializeField] private GameObject mainMenuBG;

    [Header("Map Selection")]
    [SerializeField] private GameObject mapSelectionBG;


    private void Awake()
    {
       Cursor.lockState = CursorLockMode.Locked;
       Cursor.visible = false;
    }

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
