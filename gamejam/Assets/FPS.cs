using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPS : MonoBehaviour
{
    public float timer, refresh, avgFrameRate;
    public string display = "FPS: {0}";
    public Text m_Text;

    private void Update()
    {
        float timelapse = Time.smoothDeltaTime;
        timer = timer <= 0 ? refresh : timer -= timelapse;

        if (timer <= 0) avgFrameRate = (int)(1f / timelapse);
        m_Text.text = string.Format(display, avgFrameRate.ToString());
    }
}
