using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class MainMenuBtnController : MonoBehaviour
{
    public Button btnStart;
    public Button btnQuit;
    
    public void QuitGame() {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Story");
    }

    // Start is called before the first frame update
    void Start()
    {
        var bText = btnStart.GetComponentInChildren<TextMeshProUGUI>();
        bText.text = "Play";

        bText = btnQuit.GetComponentInChildren<TextMeshProUGUI>();
        bText.text = "Quit";

        this.btnStart.onClick.AddListener(() => StartGame());
        this.btnQuit.onClick.AddListener(() => QuitGame());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
