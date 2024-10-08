using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter {
    public static event EventHandler OnAnyTrashedItem;

    new public static void ResetStaticData() {
        OnAnyTrashedItem = null;
    }

    public override void Interact(Player player) {
        if (player.HasKitchenObject()) {
            player.GetKitchenObject().DestroySelf();
            OnAnyTrashedItem?.Invoke(this, EventArgs.Empty);
        }
    }
}
