using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Image))]
public class StoryManager : MonoBehaviour
{
    public GameObject panel;
    public Sprite [] sprites = new Sprite[7];

    private Image image;
    private int imageIndex = 0;

    // Start is called before the first frame update

    void Start()
    {
        image = panel.GetComponent<Image>();
        image.sprite = sprites[imageIndex];
        imageIndex++;
    }

    // Update is called once per frame
    void Update()
    {
        if (imageIndex >= sprites.Length)
        {
            SceneManager.LoadScene("Level_01");
            return;
        }

        if (Input.GetKeyDown("space") && imageIndex < sprites.Length) {
            image.sprite = sprites[imageIndex];

            imageIndex++;
        }
    }
}
