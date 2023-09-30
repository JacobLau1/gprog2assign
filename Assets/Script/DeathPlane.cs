using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    public GameObject Player;
    Vector3 OriginalPlayerPosition;
    // Start is called before the first frame update
    void Start()
    {
        OriginalPlayerPosition = Player.transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            RestartGame();
        }
    }

    public void RestartGame()
    {
        Player.transform.position = OriginalPlayerPosition;
    }
}
