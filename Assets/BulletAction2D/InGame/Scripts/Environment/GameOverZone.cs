using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverZone : MonoBehaviour
{
    [SerializeField]
    GameObject gameOverMessage;

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>()?.RestertPlayer();
        }
    }
}
