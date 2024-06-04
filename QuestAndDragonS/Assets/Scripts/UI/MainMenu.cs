using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] float changeDelay = 1f;

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(SceneChange(sceneName));
    }

    IEnumerator SceneChange(string sceneName)
    {
        //wait for the sound to end
 

        //Scene transition

        yield return new WaitForSeconds(changeDelay);

        //# scene switch, whats the best way?
        SceneManager.LoadScene(sceneName); //Back to MainMenu scene
        yield return null;
    }

}
