using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameFeatureController : MonoBehaviour
{

    public Transform level;

    private int maxGridSpace = 1;

    public int numberOfFeaturesUsed = 0;

    public GridLayoutGroup buttonGrid;

    public Transform buttonPoolArea;

    public TMPro.TextMeshProUGUI messageText;

    public Transform actionArea;

    public ParticleSystem spawnParticle;

    private ActionButton selectedButton = null;

    public List<Transform> buttonTiers;
    public List<int> buttonTierUnlocks; // How many spawned pieces to the next template

    private bool isButtonGridLocked = false;

    private int currentTier = -1;

    private void Start()
    {
        if (buttonTiers.Count != buttonTierUnlocks.Count)
            throw new System.Exception("Tier Counts don't match!!");

        TierCheck();
        StartCoroutine(PerpetualButtons());
    }

    private void TierCheck()
    {
        if (currentTier + 1 >= buttonTierUnlocks.Count)
            return; // No more unlocks

        if (numberOfFeaturesUsed >= buttonTierUnlocks[currentTier + 1])
        {
            currentTier++;

            ActionButton[] buttonTemplates = buttonTiers[currentTier].gameObject.GetComponentsInChildren<ActionButton>();

            foreach (ActionButton buttonTemplate in buttonTemplates)
            {
                ActionButton newButton = Instantiate(buttonTemplate, buttonPoolArea);
            }

            // Special case logic
            switch(currentTier)
            {
                case 0:
                    buttonGrid.constraintCount = 2;
                    maxGridSpace = 4;
                    break;
                case 2:
                    buttonGrid.constraintCount = 3;
                    maxGridSpace = 6;
                    break;
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
        isButtonGridLocked = !canInteract;
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
            if (numOfButtons < maxGridSpace)
            {
                int poolCount = buttonPoolArea.transform.childCount;
                if (poolCount > 0)
                {
                    ActionButton button = buttonPoolArea.GetChild(Random.Range(0, poolCount)).GetComponent<ActionButton>();
                    button.transform.SetParent(buttonGrid.transform);
                    button.button.interactable = !isButtonGridLocked;
                }
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
