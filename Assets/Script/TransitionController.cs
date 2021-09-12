using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionController : MonoBehaviour
{
    public Animator fade;
    public float fadeTime = 1f;

    public IEnumerator TriggerStart()
    {
        fade.SetTrigger("Start");

        yield return new WaitForSeconds(fadeTime);
    }
}
