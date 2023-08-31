using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject: MonoBehaviour {

    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    private IKitchenObjectParent parent;

    public KitchenObjectSO GetKitchenObjectSO() {
        return kitchenObjectSO;
    }

    public void setParent(IKitchenObjectParent parent) {
        if (this.parent != null) {
            this.parent.ClearKitchenObject();
        }

        this.parent = parent;

        if (this.parent.HasKitchenObject()) {
            Debug.LogError("This Parent already has a KitchenObject");
        }

        parent.SetKitchenObject(this);

        transform.parent = parent.GetKitchenObjectFollowtransform();
        transform.localPosition = Vector3.zero;
    }

    public IKitchenObjectParent getParent() {
        return parent;
    }
}
