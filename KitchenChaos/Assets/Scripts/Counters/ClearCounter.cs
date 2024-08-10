using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{

    [SerializeField] private KitchenObjectSO kitchenObjectSO;


    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            // empty
            if (player.HasKitchenObject()) {
                // player already has item in hands
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
        } else {
            // there is something on the counter
            if (!player.HasKitchenObject()) {
                // player isnt carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            } else {
                // player is carrying object

                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                    // player is holding a plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
                        GetKitchenObject().DestroySelf();
                    } 
                } else {
                    // player is carrying something else, does the counter have a plate?
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject)) {
                        // the counter has a plate
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO())) {
                            // player is carrying an ingredient that can be added to the plate
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
        }
    }

}
