using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenuBtnController : MonoBehaviour
{
    public Button btnStart;

    // Start is called before the first frame update
    void Start()
    {
        this.btnStart.onClick.AddListener(() => {
            SceneManager.LoadScene("Level_1");
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
