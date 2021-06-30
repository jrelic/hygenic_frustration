using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAnimator : MonoBehaviour
{
    public Image image;

    public Sprite[] sprites;

    public float FrameSwap;

    private int CurrentFrame;

    public void OnEnable()
    {
        CurrentFrame = 0;
        StartCoroutine(IterateAnimations());
    }

    private IEnumerator IterateAnimations()
    {
        while(true)
        {
            yield return new WaitForSeconds(FrameSwap);
            image.sprite = sprites[CurrentFrame];
            CurrentFrame++;
            CurrentFrame %= sprites.Length;
        }
    }
}
