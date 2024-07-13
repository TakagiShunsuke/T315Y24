using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.TimeZoneInfo;

public class InkTransition : MonoBehaviour
{
    public Material transitionMaterial;
    public float transitionDuration = 2.0f;
    private float transitionProgress = 0.0f;
    private bool isTransitioning = false;

    void Update()
    {
        transitionMaterial.SetFloat("_TransitionProgress", 0.0f);
        transitionMaterial.SetFloat("_alpha", 0.0f);
        if (isTransitioning)
        {
            transitionProgress += Time.deltaTime * 0.075f;
            transitionMaterial.SetFloat("_TransitionProgress", Mathf.Clamp01(transitionProgress / transitionDuration));
            transitionMaterial.SetFloat("_alpha", 1.0f);

            if (transitionProgress >= transitionDuration)
            {
                transitionProgress = 0.0f;
                isTransitioning = false;
                transitionMaterial.SetFloat("_TransitionProgress", 0.0f);
                transitionMaterial.SetFloat("_alpha", 0.0f);

            }
        }
    }

    public void StartTransition()
    {
        isTransitioning = true;
    }
}
