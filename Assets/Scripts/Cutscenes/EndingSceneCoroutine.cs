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

        yield return new WaitForSeconds(1f);

        messages.AppendMainText(" you did it.");

        yield return new WaitForSeconds(3f);

        messages.SetMainText("You made a game for yourself...");

        yield return new WaitForSeconds(4f);

        int seconds = Mathf.RoundToInt(Time.time);

        int minutes = seconds / 60;
        int exSeconds = seconds % 60;

        string pluralMinute = minutes == 1 ? "minute" : "minutes";
        string pluralSeconds = seconds == 1 ? "second" : "seconds";

        messages.SetMainText(string.Format("And it only took you {0} {2} and {1} {3}\nto finish it.", minutes, exSeconds, pluralMinute, pluralSeconds));

        yield return new WaitForSeconds(5f);

        messages.SetMainText(string.Format("Would you like to play again?"));

        yield return new WaitForSeconds(3f);

        retryCanvas.gameObject.SetActive(true);
    }


}
