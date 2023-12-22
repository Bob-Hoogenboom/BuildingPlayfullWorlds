using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class EnemyManager : MonoBehaviour
{
    public UnityEvent OnAllEnemiesDefeated;
    [SerializeField] private int defeatedEnemies;
    [SerializeField] private int requiredEnemies = 5;

    public void AddDefeatedEnemy()
    {
        defeatedEnemies++;

        if (defeatedEnemies == requiredEnemies)
        {
            StartCoroutine(Victory());
        }
    }

    IEnumerator Victory()
    {
        OnAllEnemiesDefeated.Invoke();
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("MainMenu");
    }


    public void Defeated()
    {
        StartCoroutine(Defeat());
    }

    IEnumerator Defeat()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("MainMenu");
    }
}
