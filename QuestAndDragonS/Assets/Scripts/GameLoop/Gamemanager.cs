using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
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

    [SerializeField] private TMP_Text targetScoreTMP;
    [SerializeField] private TMP_Text currentScoreTMP;

    private int _currentScore;

    private void Start()
    {
        _currentScore = 0;
        targetScoreTMP.text = enemylist.Count.ToString();
        currentScoreTMP.text = _currentScore.ToString();
    }


    void Update()
    {
        foreach (var enemy in enemylist.ToList())
        {
            if (enemy.isDead)
            {
                enemylist.Remove(enemy);

                _currentScore++; // update score index

                currentScoreTMP.text = _currentScore.ToString();
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
