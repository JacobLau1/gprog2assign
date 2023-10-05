using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel1 : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player finished level ");

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }

    }
}
