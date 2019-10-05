using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ActionButtonHookup : MonoBehaviour
{

    private void Update()
    {
        if (!Application.isPlaying)
        {
            if (transform.parent != null)
            {
                ActionButton action = GetComponent<ActionButton>();

                if (!action.controller)
                {
                    action.controller = FindObjectOfType<GameFeatureController>();
                }

                if (!action.button)
                {
                    action.button = GetComponent<Button>();
                }
            }
        }
    }
}
