using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[RequireComponent(typeof(ActionButtonHookup))]
public class ActionButton : MonoBehaviour
{
    public Button button;

    public GameFeatureController controller;

    [FormerlySerializedAs("actionToRunPrefab")]
    public ActionBehavior actionToRun;

    [HideInInspector]
    public ActionBehavior actionRunning;

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(Activate);
    }

    private void Activate()
    {
        if (actionToRun.transform != transform)
        {
            Debug.Log("Action To Run is not on the button...");
            actionRunning = Instantiate(actionToRun, controller.transform);
        }
        else
        {
            actionRunning = actionToRun;
        }

        actionRunning.SetUp(controller, controller.level);
        controller.OnActionButtonStart(this);

        ClickSound();
    }

    private void ClickSound()
    {
        SoundBoard.Instance?.buttonClick?.Play();
    }
}
