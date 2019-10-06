using System.Collections;
using UnityEngine;

public class EndingSceneCoroutine : MonoBehaviour
{
    public Canvas retryCanvas;

    public MessageBoard messages;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("StoryTime");
    }

    IEnumerator StoryTime()
    {
        yield return new WaitForSeconds(2f);

        messages.SetMainText("Well...");

        yield return new WaitForSeconds(0.75f);

        messages.AppendMainText(" you did it.");

        yield return new WaitForSeconds(3f);

        messages.SetMainText("You made a game for yourself...");

        yield return new WaitForSeconds(4f);

        int seconds = Mathf.RoundToInt(Time.time);

        int minutes = seconds / 60;
        int exSeconds = seconds % 60;

        messages.SetMainText(string.Format("And it only took you {0} minutes and {1} seconds\nto finish.", minutes, exSeconds));

        yield return new WaitForSeconds(5f);

        messages.SetMainText(string.Format("Would you like to try it again?"));

        yield return new WaitForSeconds(3f);

        retryCanvas.gameObject.SetActive(true);
    }


}
