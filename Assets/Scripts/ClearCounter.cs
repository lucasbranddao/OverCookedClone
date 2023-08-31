using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ClearCounter: BaseCounter {

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
            kitchenObjectTransform.GetComponent<KitchenObject>().setParent(this);
        } else {
            GetKitchenObject().setParent(player);
        }
    }
}
