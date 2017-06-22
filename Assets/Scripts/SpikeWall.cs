using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeWall : MonoBehaviour
{ 
	void Start () {
        Renderer rend = GetComponent<Renderer>();
        var newMat = new Material(rend.material.shader);
        newMat.mainTexture = rend.material.mainTexture;
        newMat.SetTextureScale("_MainTex", new Vector2((int)transform.localScale.x/2, 1f));
        rend.material = newMat;
    }
}
