using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameFeatureController : MonoBehaviour
{

    public Transform level;

    public int maxGridSpace = 6;

    public int numberOfFeaturesUsed = 0;

    public GridLayoutGroup buttonGrid;

    public TMPro.TextMeshProUGUI messageText;

    public Transform actionArea;

    public ParticleSystem spawnParticle;

    private ActionButton selectedButton = null;

    public List<Transform> buttonTemplateTiers;

    private List<ActionButton> buttonTemplates = new List<ActionButton>();

    private void Start()
    {
        TierCheck();
        StartCoroutine(PerpetualButtons());
    }

    private void TierCheck()
    {
        if (numberOfFeaturesUsed % 6 == 0)
        {
            int nextTier = numberOfFeaturesUsed / 6;
            if (buttonTemplateTiers.Count > nextTier)
            {
                ActionButton[] buttons = buttonTemplateTiers[nextTier].gameObject.GetComponentsInChildren<ActionButton>();
                buttonTemplates.AddRange(buttons);
            }
        }
    }

    private void SetInteractableButtons(bool canInteract)
    {
        Button[] buttons = buttonGrid.GetComponentsInChildren<Button>();
        foreach (Button butt in buttons)
        {
            butt.interactable = canInteract;
        }
    }

    public void OnActionButtonStart(ActionButton button)
    {
        SetInteractableButtons(false);
        selectedButton = button;
    }

    public void OnActionCanceled()
    {
        SetInteractableButtons(true);
    }

    public void OnActionFinished(Object obj)
    {
        if (selectedButton)
        {
            Destroy(selectedButton.gameObject);
        }

        SetInteractableButtons(true);

        ParticleSystem clone=null;
        if (obj is GameObject gameObj)
        {
            clone = Instantiate(spawnParticle, gameObj.transform);
        }
        else if (obj is MonoBehaviour monoObj)
        {
            clone = Instantiate(spawnParticle, monoObj.transform);
        }

        if (clone)
        {
            clone.transform.localPosition = Vector3.zero;
            clone.transform.rotation = Quaternion.Euler(Vector3.forward);
            clone.gameObject.SetActive(true);
        }

        numberOfFeaturesUsed++;
        TierCheck();
    }

    public IEnumerator PerpetualButtons()
    {
        yield return null;

        while (true)
        {
            int numOfButtons = buttonGrid.transform.childCount;
            while (numOfButtons < maxGridSpace)
            {
                yield return new WaitForSeconds(1.5f);
                ActionButton buttonTemplate = buttonTemplates[Random.Range(0, buttonTemplates.Count)];
                Instantiate(buttonTemplate, buttonGrid.transform);
                numOfButtons = buttonGrid.transform.childCount;
            }
            yield return null;
        }
    }
}
