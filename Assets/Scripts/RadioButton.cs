using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioButton : MonoBehaviour
{
    public Texture2D selectedTexture;
    public Texture2D deselectedTexture;
    public RadioButton[] otherRadioButtons;

    Material material;

    public void Awake()
    {
        material = GetComponent<Renderer>().material;
    }
    
    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Select();
        }
    }

    public void Select()
    {
        // Deselect all other buttons
        foreach(var button in otherRadioButtons)
        {
            button.Deselect();
        }

        // Swap the texture
        material.SetTexture("_MainTex", selectedTexture);

        Execute();
    }

    public void Deselect()
    {
        // Swap the texture
        material.SetTexture("_MainTex", deselectedTexture);
    }

    public virtual void Execute()
    {

    }
}
