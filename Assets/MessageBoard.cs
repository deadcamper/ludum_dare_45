using System.Collections;
using UnityEngine;

public class MessageBoard : MonoBehaviour
{
    // Game texts
    public TMPro.TextMeshProUGUI scoreText;
    public TMPro.TextMeshProUGUI timeLimitText;

    // Editor texts
    public TMPro.TextMeshProUGUI editorTextTemplate;

    // Central texts
    public TMPro.TextMeshProUGUI mainCenterText;

    public float secondsBetweenTyping = 1 / 40f;

    public string mainText;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EndlessTypeWriter());
    }

    private IEnumerator EndlessTypeWriter()
    {
        while (true)
        {
            string currentText = mainCenterText.text;
            if (mainText != currentText)
            {
                if (!mainText.StartsWith(currentText, System.StringComparison.Ordinal))
                {
                    currentText = "";
                }
                else
                {
                    int length = currentText.Length;
                    currentText = mainText.Substring(0, length + 1);
                }
                mainCenterText.text = currentText;
                yield return new WaitForSeconds(secondsBetweenTyping);
            }
            else
            {
                yield return null;
            }
            
        }
    }

    public void ClearMainText()
    {
        mainText = "";
        mainCenterText.text = "";
    }

    public void SetMainText(string text)
    {
        mainText = text;
        mainCenterText.text = "";
    }

    public void AppendMainText(string text)
    {
        mainText += text;
    }
}
