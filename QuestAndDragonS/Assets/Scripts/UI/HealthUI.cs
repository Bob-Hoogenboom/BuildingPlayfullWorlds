using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private TMP_Text healthText;

    private IDamagable idamagable;


    private void Start()
    {
        idamagable = player.GetComponent<IDamagable>();
        if (idamagable == null)
        {
            this.enabled = false;
            return;
        }
    }

    private void LateUpdate()
    {
        healthText.text = idamagable.HitPoints.ToString();
    }
}
