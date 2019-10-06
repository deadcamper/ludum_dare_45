using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryScript : MonoBehaviour
{

    private Quaternion startingRotation;

    private void Start()
    {
        startingRotation = transform.rotation;
    }

    private void OnMouseUpAsButton()
    {
        SceneManager.LoadScene("GameScene");
    }

    private void OnMouseEnter()
    {
        StartCoroutine("Spin");
    }

    private void OnMouseExit()
    {
        StopCoroutine("Spin");
        transform.rotation = startingRotation;
    }

    IEnumerator Spin()
    {
        while (true)
        {
            transform.rotation = transform.rotation * Quaternion.Euler(new Vector3(0, 0, -Time.deltaTime * 400));
            yield return null;
        }
    }

}
