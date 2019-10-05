using UnityEngine;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour
{
    public Button button;

    public GameFeatureController controller;

    public Transform actionArea;

    public ActionBehavior actionToRunPrefab;

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(Activate);
    }

    private void Activate()
    {
        ActionBehavior action = Instantiate(actionToRunPrefab, actionArea);
        action.SetUp(controller, controller.level);

        controller.OnActionButtonStart(this);
    }
}
