using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour
{
    [SerializeField] float duration = .5f;
    [SerializeField] float power = 5f;
    public IEnumerator Shake()
    {
        Vector3 originalpos = transform.localPosition;
        float timer = 0f;
        while (timer < duration)
        {
            float x = UnityEngine.Random.Range(-.2f, .2f) * power;
            float y = UnityEngine.Random.Range(-.2f, .2f) * power;
            transform.localPosition = new Vector3(x, y, originalpos.z);
            timer += Time.deltaTime;
            yield return null; //vänta till nästa frame
        }
        transform.localPosition = originalpos;
    }

    internal void StartCoruotine(object p)
    {
        throw new NotImplementedException();
    }
}
