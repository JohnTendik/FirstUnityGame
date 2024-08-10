using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    
    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate;

    private void Start() {
        DeliveryManager.Instance.OnRecipeSpawned += DeliveryManager_OnRecipeSpawned;
        DeliveryManager.Instance.OnRecipeDelivered += DeliveryManager_OnRecipeDelivered;
        
        UpdateVisual();
    }

    private void DeliveryManager_OnRecipeDelivered(object sender, EventArgs e) {
        UpdateVisual();
    }

    private void DeliveryManager_OnRecipeSpawned(object sender, EventArgs e) {
        UpdateVisual();
    }

    private void Awake() {
        recipeTemplate.gameObject.SetActive(false);
    }

    private void UpdateVisual() {
        foreach (Transform child in container) {
            if (child == recipeTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (RecipeSO recipeSO in DeliveryManager.Instance.GetWaitingRecipeSOList()) {
            Transform recipeTransform = Instantiate(recipeTemplate, container);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<DeliveryManagerSingleRecipeUI>().SetRecipeSO(recipeSO);
        }
    }
}
