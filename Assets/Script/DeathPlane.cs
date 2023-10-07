using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathPlane : MonoBehaviour
{

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // Reload the current scene.
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Debug.Log("Player touched the death plane!");

            GameManager gm = new GameManager();
            GameManager2 gm2 = new GameManager2();
            GameManager3 gm3 = new GameManager3();

            switch (SceneManager.GetActiveScene().buildIndex)
            {
                case 1:
                    gm.SetScore(0.0f);
                    break;
                case 2:
                    gm2.SetScore(gm.GetScore());
                    break;
                case 3:
                    gm3.SetScore(gm2.GetScore());
                    break;
            }
        }

    }

}
