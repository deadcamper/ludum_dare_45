using System;
using UnityEngine;

public abstract class ActionBehavior : MonoBehaviour
{
    public event Action<UnityEngine.Object> OnFinished = delegate { };
    public event Action OnCancelled = delegate { };

    public GameFeatureController controller;
    public Transform level;

    public static float GRAVITY_CONSTANT = 0f;

    public void SetUp(GameFeatureController controller, Transform level)
    {
        this.controller = controller;
        this.level = level;

        OnFinished += controller.OnActionFinished;
        OnCancelled += controller.OnActionCanceled;
    }

    public void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace))
        {
            Cancelled();
        }
    }

    protected void Finished(UnityEngine.Object obj)
    {
        OnFinished(obj);
        Destroy(gameObject);
    }

    protected void Cancelled()
    {
        CleanUpOnCancel();
        Destroy(gameObject);

        OnCancelled();
    }

    public static Rigidbody2D EnsureRigidbody2DComponent(GameObject gameObject)
    {
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.drag = 5;
            rb.gravityScale = GRAVITY_CONSTANT;
            rb.useAutoMass = true;
        }

        return rb;
    }

    public static Rigidbody2D EnsureCorrectedRigidbody2D(GameObject gameObject)
    {
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        if (rb)
        {
            rb.gravityScale = GRAVITY_CONSTANT;
            rb.useAutoMass = true;
        }
        return rb;
    }

    protected abstract void CleanUpOnCancel();
}
