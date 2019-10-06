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

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(Activate);
    }

    private void Activate()
    {
        ActionBehavior action;
        if (actionToRun.transform != transform)
        {
            action = Instantiate(actionToRun, controller.transform);
        }
        else
        {
            action = actionToRun;
        }

        action.SetUp(controller, controller.level);
        controller.OnActionButtonStart(this);
    }
}
