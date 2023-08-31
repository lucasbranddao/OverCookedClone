using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter: MonoBehaviour, IKitchenObjectParent {

    [SerializeField] private Transform counterTopPoint;

    private KitchenObject kitchenObject;
   
   public virtual void Interact(Player player) {
       Debug.Log("BaseCounter.Interact()");
   }

    public KitchenObject GetKitchenObject() {
        return kitchenObject;
    }

    public Transform GetKitchenObjectFollowtransform() {
        return counterTopPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;
    }

    public bool HasKitchenObject() {
        return kitchenObject != null;
    }

    public void ClearKitchenObject() {
        this.kitchenObject = null;
    }
}
