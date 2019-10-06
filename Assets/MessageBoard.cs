using System.Collections;
using UnityEngine;

public class MessageBoard : MonoBehaviour
{
    // Game texts
    public TMPro.TextMeshProUGUI scoreText;
    public TMPro.TextMeshProUGUI timeLimitText;

    // Editor texts
    public TMPro.TextMeshProUGUI editorTextTemplate;

    public TMPro.TextMeshProUGUI editorInstructionText1;
    public TMPro.TextMeshProUGUI editorInstructionText2;

    // Central texts
    public TMPro.TextMeshProUGUI mainCenterText;

    public int framesBetweenTyping = 3;
    public float secondsBetweenTyping = 1 / 40f;

    public string mainText;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EndlessTypeWriter());
        StartCoroutine(InstructionTextPulse());
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
                for (int n = 0; n < framesBetweenTyping; n++)
                {
                    yield return null;
                }
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

    public void SetInstructionText(string text1, string text2)
    {
        editorInstructionText1.text = text1;
        editorInstructionText2.text = text2;

        editorInstructionText1.gameObject.SetActive(true);
        editorInstructionText2.gameObject.SetActive(true);
    }

    public void ClearInstructionText()
    {
        editorInstructionText1.gameObject.SetActive(false);
        editorInstructionText2.gameObject.SetActive(false);
    }

    private IEnumerator InstructionTextPulse()
    {
        Color color1 = editorInstructionText1.color;
        Color color2 = editorInstructionText2.color;
        Color dark = Color.black;

        while (true)
        {
            float bottom = 3 / 5f;

            int ctr = 60;
            for (int n = 1; n <= ctr; n++)
            {
                float lerp = Mathf.Lerp(1, bottom, (float)n / ctr);

                editorInstructionText1.color = Color.Lerp(dark, color1, lerp);
                editorInstructionText2.color = Color.Lerp(dark, color2, lerp);
                yield return null;
            }
            for (int n = 1; n <= ctr; n++)
            {
                float lerp = Mathf.Lerp(bottom, 1, (float)n / ctr);

                editorInstructionText1.color = Color.Lerp(dark, color1, lerp);
                editorInstructionText2.color = Color.Lerp(dark, color2, lerp);
                yield return null;
            }
        }
    }
}