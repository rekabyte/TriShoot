using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Menu : MonoBehaviour
{

    public string sceneToLoad;
    public Health playerHealth;

    public void ChangeScene()
    {
        playerHealth.healthValue = playerHealth.defaultHealthValue;
        SceneManager.LoadScene(sceneToLoad);
    }
}
