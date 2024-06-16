using UnityEngine;

[CreateAssetMenu(fileName = "Plant", menuName = "Plant", order = 1)]
public class Plant : ScriptableObject
{
    public GameObject plantPrefab;
    public RuntimeAnimatorController animatorController;
}
