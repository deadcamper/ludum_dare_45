using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ActionButtonHookup : MonoBehaviour
{

    private void Update()
    {
        if (!Application.isPlaying)
        {
            if (gameObject.scene.name != null)
            {
                ActionButton action = GetComponent<ActionButton>();

                if (!action.controller)
                {
                    GameFeatureController controller = FindObjectOfType<GameFeatureController>();

                    if (controller && gameObject.scene.name == controller.gameObject.scene.name)
                    {
                        action.controller = FindObjectOfType<GameFeatureController>();
                    }
                }

                if (!action.button)
                {
                    action.button = GetComponent<Button>();
                }
            }
        }
    }
}
