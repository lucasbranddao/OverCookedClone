using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKitchenObjectParent {
    public KitchenObject GetKitchenObject();
    public Transform GetKitchenObjectFollowtransform();
    public void SetKitchenObject(KitchenObject kitchenObject);
    public bool HasKitchenObject();
    public void ClearKitchenObject();
}