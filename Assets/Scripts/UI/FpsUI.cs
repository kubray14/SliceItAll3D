using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Profiling;

public class FpsUI : MonoBehaviour
{
    public TMP_Text fpsText;

    private void Start()
    {
        StartCoroutine(ShowFps());
       // StartCoroutine(Stopp());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Profiler.BeginSample("FPS UI FOR LOOP START");
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    print("a");
                }
            }
            Profiler.EndSample();
        }
    }

    IEnumerator ShowFps()
    {
        while (true)
        {
            //yield return new WaitForSecondsRealtime(0.5f);
            yield return null;
            int fps = (int)(1 / Time.unscaledDeltaTime);
            fpsText.text = " Fps : " + fps.ToString();
        }
    }

    IEnumerator Stopp()
    {
        yield return new WaitForSecondsRealtime(5f);
        while (true)
        {
            //yield return new WaitForSecondsRealtime(0.5f);
            yield return null;
            int fps = (int)(1 / Time.unscaledDeltaTime);
            fpsText.text = " Fps : " + fps.ToString();
            if (fps <= 40)
            {
                UnityEditor.EditorApplication.isPaused = true;
            }
        }
    }


}
