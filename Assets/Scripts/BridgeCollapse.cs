using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeCollapse : MonoBehaviour
{
    public GameObject player;
    private Animator collapseAnimator;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.Equals(player))
        {
            collapseAnimator.enabled = true;
            collapseAnimator.Play("BridgeCollapse");
            StartCoroutine(waiter());
        }
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(0.6f);

        Destroy(GetComponent<BoxCollider2D>());
    }

    // Start is called before the first frame update
    void Start()
    {
        collapseAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
