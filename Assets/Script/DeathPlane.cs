using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathPlane : MonoBehaviour
{
    //public GameObject Player;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log(" You touched the deathplane ");
            GameManager.Score = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }

    }

}
