using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Utilities : MonoBehaviour
{
    public void StartGame()
    {
       SceneManager.LoadScene(1);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void HouseSound()
    {
        //start playing house sound
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
