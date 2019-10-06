using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroSceneCoroutine : MonoBehaviour
{
    public Image irritation;

    public MessageBoard messages;

    public int frustrationKeyCount = 20;

    private int keyCount = 0;

    public string NextScene = "GameScene";

    public Color irritationColor;

    private bool isStoryTimeCutOff = true;

    // Start is called before the first frame update
    void Start()
    {
        irritationColor = irritation.color;
        irritation.color = new Color(0,0,0,0);
        StartCoroutine("StoryTime");
    }

    IEnumerator StoryTime()
    {
        yield return new WaitForSeconds(2f);

        isStoryTimeCutOff = false;

        messages.SetMainText("Well...");

        yield return new WaitForSeconds(0.75f);

        messages.AppendMainText(" this is awkward...");

        yield return new WaitForSeconds(2f);

        messages.ClearMainText();

        yield return new WaitForSeconds(1f);

        messages.SetMainText("I seem to have forgotten to put a \"game\" here...");

        yield return new WaitForSeconds(3f);

        messages.AppendMainText("\n\nThat's unfortunate...");

        yield return new WaitForSeconds(3f);

        messages.ClearMainText();

        yield return new WaitForSeconds(0.25f);

        messages.SetMainText("Maybe you can help build it?");

        yield return new WaitForSeconds(3f);

        messages.ClearMainText();

        yield return new WaitForSeconds(0.5f);

        MoveToNextScene();
    }

    IEnumerator CutOffStoryTime()
    {
        irritation.color = irritationColor;

        messages.SetMainText("Alright, calm down!\nJeeze...\n");

        yield return new WaitForSeconds(3f);

        messages.ClearMainText();

        yield return new WaitForSeconds(0.5f);

        MoveToNextScene();
    }

    IEnumerator Irritated()
    {
        Color blank = new Color(0,0,0,0);

        float lerp = Mathf.Clamp(keyCount * 0.125f, 0.25f, 1);
        float timeLeft = lerp;

        irritation.color = Color.Lerp(blank, irritationColor, lerp);

        while(timeLeft > 0f)
        {
            timeLeft -= Time.deltaTime;
            irritation.color = Color.Lerp(blank, irritationColor, timeLeft);

            yield return null;
        }
        irritation.color = blank;
    }

    public void Update()
    {
        if (!isStoryTimeCutOff && Input.anyKeyDown)
        {
            keyCount++;
            StopCoroutine("Irritated");

            if (keyCount >= frustrationKeyCount)
            {
                isStoryTimeCutOff = true;
                StopCoroutine("StoryTime");
                StartCoroutine("CutOffStoryTime");
            }
            else
            {
                StartCoroutine("Irritated");
            }
        }
    }

    private void MoveToNextScene()
    {
        SceneManager.LoadScene(NextScene);
    }
}
