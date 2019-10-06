using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameFeatureController : MonoBehaviour
{
    public Transform level;

    public GridLayoutGroup buttonGrid;

    public Transform buttonPoolArea;

    public ParticleSystem spawnParticle;

    private ActionButton selectedButton = null;

    public MessageBoard messageBoard;

    public List<Transform> buttonTiers;
    public List<int> buttonTierUnlocks; // How many spawned pieces to the next template

    public Transform dangerTier;

    private bool isButtonGridLocked = false;

    private int maxGridSpace = 1;

    public int numberOfFeaturesUsed = 0;

    private int currentTier = -1;

    private void Start()
    {
        if (buttonTiers.Count != buttonTierUnlocks.Count)
            throw new System.Exception("Tier Counts don't match!!");

        if (buttonTiers.Count != 0)
        {
            CheckButtons();
        }

        ActionBehavior.SetUpClass();
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
                case 1:
                    maxGridSpace = 2;
                    break;
                case 2:
                    maxGridSpace = 3;
                    break;
                case 3:
                    maxGridSpace = 4;
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

        messageBoard.SetInstructionText(button.actionRunning.InstructionText, "You can cancel out at any time with 'Escape'");
    }

    public void OnActionCanceled()
    {
        SetInteractableButtons(true);

        messageBoard.ClearInstructionText();
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

        messageBoard.ClearInstructionText();

        numberOfFeaturesUsed++;
        CheckButtons();
    }

    private void CheckButtons()
    {
        StopCoroutine("ShiftButtons");
        TierCheck();
        StartCoroutine("ShiftButtons");
    }

    public IEnumerator ShiftButtons()
    {
        yield return new WaitForSeconds(0.5f);

        while (buttonPoolArea && maxGridSpace > buttonGrid.transform.childCount)
        {
            yield return new WaitForSeconds(0.25f);

            // Desperate times call for desperate measures
            int poolCount = buttonPoolArea.transform.childCount;
            if (poolCount == 0 && buttonGrid.transform.childCount == 0)
            {
                if (dangerTier)
                {
                    foreach (ActionButton buttonTemplate in dangerTier.GetComponentsInChildren<ActionButton>())
                    {
                        ActionButton newButton = Instantiate(buttonTemplate, buttonPoolArea);
                    }
                    dangerTier = null;
                }
            }

            if (poolCount > 0)
            {
                ActionButton button = buttonPoolArea.GetChild(Random.Range(0, poolCount)).GetComponent<ActionButton>();
                button.transform.SetParent(buttonGrid.transform);
                button.button.interactable = !isButtonGridLocked;
            }
            else
            {
                break;
            }
        }
    }
}
