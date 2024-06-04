using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    [Header("OnGameEnd")]
    public UnityEvent onEnemiesDefeated;
    private bool _gameEnded = false;
    
    [SerializeField]private float musicTimer = 3f;
    [SerializeField] private float transitionTimer = 1f;

    [SerializeField] private Animator animator;
    private int startTrigger = Animator.StringToHash("startFade");


    [Header("Detection")]
    [SerializeField] private List<ActorZombie> enemylist;


    void Update()
    {
        foreach (var enemy in enemylist)
        {
            if (enemy.isDead)
            {
                enemylist.Remove(enemy);    
            }
        }

        if (enemylist.Count <= 0 && !_gameEnded)
        {
            _gameEnded = true; //mnakes sure this only plays once

            onEnemiesDefeated.Invoke();
            _ = StartCoroutine(GameWon()); // '_' to discard any value given to the compiler as the coroutine return null we wont be using
        }
    }

    public void GameEnd()
    {
        _ = StartCoroutine(GameWon());
    }

    IEnumerator GameWon()
    {
        //wait for the sound to end
        yield return new WaitForSeconds(musicTimer);

        //Scene transition
        animator.SetTrigger(startTrigger);
        yield return new WaitForSeconds(transitionTimer);

        //# scene switch, whats the best way?
        SceneManager.LoadScene(0); //Back to MainMenu scene
        yield return null;
    }
}
