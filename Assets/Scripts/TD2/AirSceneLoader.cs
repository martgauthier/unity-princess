using UnityEngine;
using UnityEngine.SceneManagement;
public class AirSceneLoader : MonoBehaviour
{
    public void PlayGame()
    {
        Debug.Log("Should play game !");
        SceneManager.LoadScene(1);
    }
    public void LoadMenu()
    {
        Debug.Log("Should load menu !");
        SceneManager.LoadScene(0);
    }
}
