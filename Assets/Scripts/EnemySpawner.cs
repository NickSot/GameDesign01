using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpawner : MonoBehaviour
{
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;

    void SpawnEnemy(GameObject enemy) {
        enemy.transform.position = transform.position;
        enemy.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemy(enemy1);
    }

    // Update is called once per frame
    void Update()
    {
        if (!enemy1.activeSelf && !enemy2.activeSelf && !enemy3.activeSelf)
        {
            SpawnEnemy(enemy2);
        }
    }
}
