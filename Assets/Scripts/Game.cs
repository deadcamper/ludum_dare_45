using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public GameFeatureController featureController;

    public MessageBoard messageBoard;

    public Image coverImage;

    int score = 0;

    float timeLeft = 0f;

    private Color finalColor;

    private bool isWinState = false;

    public void HandleGameWin()
    {
        if (isWinState)
        {
            Debug.Log("You already won!!! Go away!");
        }
        else
        {
            Debug.Log("You win!!!");

            isWinState = true;

            StartCoroutine(ImaWinner());
        }
    }

    public void HandleGameLoss()
    {
        if (isWinState)
        {
            return;
        }

        Debug.Log("You lose!!!");

        StartCoroutine(ImaLoser());
    }

    IEnumerator ImaWinner()
    {
        finalColor = new Color(1, 1, 0.9f, 1);

        yield return CoverOver();

        finalColor = Color.black;

        yield return CoverOver();

        SceneManager.LoadScene("EndScene");
    }

    IEnumerator ImaLoser()
    {
        featureController.gameObject.SetActive(false);

        messageBoard.SetMainText("Ooops...\nThe player isn't supposed to leave the screen...");

        finalColor = Color.black;

        yield return CoverOver();

        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene("GameScene");
    }

    IEnumerator CoverOver()
    {
        float timer = 1f;
        float totalTimer = timer;

        Color startColor = coverImage.color;
        while (timer > 0)
        {
            coverImage.color = Color.Lerp(finalColor, startColor, timer / totalTimer);
            timer -= Time.deltaTime;

            yield return null;
        }
    }
}
