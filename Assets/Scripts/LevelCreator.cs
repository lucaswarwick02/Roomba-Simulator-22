using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    private void Awake() {
        Transform[] children = new Transform[transform.childCount];

        // Unpack the child objects into the scene
        for (int i = 0; i < children.Length; i++) {
            children[i] = transform.GetChild(i);
        }

        foreach (Transform child in children) {
            child.SetParent(null);
        }

        // Delete this object
        Destroy(gameObject);
    }
}
