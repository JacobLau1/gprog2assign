using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrapBehaviour : MonoBehaviour
{

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("Player"))
        {
            // Reload the Level 1 scene.
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            GameManager.Score = 0;
            Debug.Log("Player touched the trap!");
        }
    }
}
