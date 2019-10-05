﻿using UnityEngine;

public class AttributeJumpMovementAction : ActionBehavior
{
    GameObject hoverObject;

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (hoverObject != null)
            {
                EnsureRigidbody2DComponent(hoverObject);

                JumpMovement jump = hoverObject.AddComponent<JumpMovement>();
                jump.speed = 10;

                Finished(jump);
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
