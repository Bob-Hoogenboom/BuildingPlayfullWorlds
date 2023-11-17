using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


/// <summary>
/// tutorial source: https://www.youtube.com/watch?v=TVJB_1d3xTE
/// </summary>
public class PlantHandler : MonoBehaviour, PlayerInputActions.IPlayerActions
{
    [Tooltip("This Transform is needed to know where the plant you currently want to have equipped will appear at.")]
    [SerializeField] private Transform plantSocket;
    [SerializeField] private Animator anim;

    //Trivia: 'equippable' is not really a word but is accepted by the Gaming-Community to use for something that can be 'equipped'
    [Tooltip("List of plants that can be used.")]
    public List<Plant> equippablePlants;
    
    private Plant _currentPlant;
    private GameObject _currentPlantGameObject;
    private IPlantObject _currentPlantInterface;
    private int _currentPlantIndex;
    
    private void Awake()
    {
        SwitchHandler(equippablePlants[0]);
    }

    public Animator GetAnimator() { return anim; }
    
    public void SwitchHandler(Plant plant)
    {
        if (_currentPlant == plant) return;
        
        Destroy(_currentPlantGameObject);

        _currentPlant = plant;
        _currentPlantGameObject = Instantiate(_currentPlant.plantPrefab, plantSocket, true);
        _currentPlantGameObject.transform.localPosition = Vector3.zero;
        _currentPlantGameObject.transform.localRotation = Quaternion.identity;

        _currentPlantInterface = _currentPlantGameObject.GetComponentInChildren<IPlantObject>();

        if (_currentPlantInterface != null)
        {
            _currentPlantInterface.OnAttachedCarrier(this);
            _currentPlantInterface.OnEquip();

            anim.runtimeAnimatorController = plant.animatorController;
        }
        else
        {
            DestroyImmediate(_currentPlantGameObject);
            _currentPlant = null;
            _currentPlantInterface = null;
            _currentPlantGameObject = null;
        }
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        //if _currentPlantInterface != null parse (context) to the _currentPlantInterface
        _currentPlantInterface?.OnFire(context);
    }
    
    public void OnSwapPlant(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _currentPlantIndex += 1 * (int) Mathf.Sign(context.ReadValue<float>());
            _currentPlantIndex = Mathf.Clamp(_currentPlantIndex, 0, equippablePlants.Count - 1);
            
            SwitchHandler(equippablePlants[_currentPlantIndex]);
        }
    }
    
    public void OnMovement(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }
}
