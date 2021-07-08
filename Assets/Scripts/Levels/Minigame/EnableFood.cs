using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableFood : MonoBehaviour
{
    private void OnEnable()
    {
        for(int i=0; i< transform.childCount; i++)
        {
            transform.GetChild(i).transform.gameObject.SetActive(true);
        }
    }
}
