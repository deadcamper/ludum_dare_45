using UnityEngine;

public class AttributeWASDMovementAction : ActionBehavior
{
    GameObject hoverObject;

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (hoverObject != null)
            {
                Rigidbody2D rb = EnsureRigidbody2DComponent(hoverObject);

                WASDMovement move = hoverObject.AddComponent<WASDMovement>();

                Finished(move);
            }
        }

        {
            RaycastHit2D ray = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (ray.collider != null)
            {
                hoverObject = ray.collider.gameObject;
            }
        }


    }

    protected override void CleanUpOnCancel()
    {
    }
}
