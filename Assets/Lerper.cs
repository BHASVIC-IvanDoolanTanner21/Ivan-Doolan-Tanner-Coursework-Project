using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lerper : MonoBehaviour
{
    public GameObject speedController;
    public float topY, bottomY;

    // Update is called once per frame
    void Update()
    {
        if (speedController.GetComponent<SpeedController>().cameraMoveUp)
        {
            speedController.GetComponent<SpeedController>().cameraMoveUp = false;
            StartCoroutine(ChangeObjectYPos(this.transform, topY, 2));
        }
        if (speedController.GetComponent<SpeedController>().cameraMoveDown)
        {
            speedController.GetComponent<SpeedController>().cameraMoveDown = false;
            StartCoroutine(ChangeObjectYPos(this.transform, bottomY, 2));
        }
    }


    // v NOT MY CODE v  -  This is an ease in out script that allows for smooth movement, it was taken from https://www.febucci.com/2018/08/easing-functions/
    public IEnumerator ChangeObjectYPos(Transform transform, float y_target, float duration)
    {
        float elapsed_time = 0; //Elapsed time

        Vector3 pos = transform.position; //Start object's position

        float y_start = pos.y; //Start "y" value

        while (elapsed_time <= duration) //Inside the loop until the time expires
        {
            pos.y = Mathf.Lerp(y_start, y_target, EaseInOut(elapsed_time / duration)); //Changes and interpolates the position's "y" value

            transform.position = pos;//Changes the object's position

            yield return null; //Waits/skips one frame

            elapsed_time += Time.unscaledDeltaTime; //Adds to the elapsed time the amount of time needed to skip/wait one frame
        }
    }
    public static float EaseIn(float t)
    {
        return t * t;
    }
    static float Flip(float x)
    {
        return 1 - x;
    }
    public static float EaseOut(float t)
    {
        return Flip(EaseIn(Flip(t)));
    }
    public static float EaseInOut(float t)
    {
        return Mathf.Lerp(EaseIn(t), EaseOut(t), t);
    }
}
