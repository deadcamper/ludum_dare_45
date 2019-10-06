using System;
using UnityEngine;

public abstract class ActionBehavior : MonoBehaviour
{
    public bool isActiveAction = false;

    public event Action<UnityEngine.Object> OnFinished = delegate { };
    public event Action OnCancelled = delegate { };

    public GameFeatureController controller;
    public Transform level;

    public static float GRAVITY_CONSTANT = 0f;

    public static void SetUpClass()
    {
        GRAVITY_CONSTANT = 0f;
    }

    public void SetUp(GameFeatureController controller, Transform level)
    {
        this.controller = controller;
        this.level = level;

        OnFinished += controller.OnActionFinished;
        OnCancelled += controller.OnActionCanceled;

        isActiveAction = true;

        OnSetUp();
    }

    protected virtual void OnSetUp()
    {

    }

    private void Update()
    {
        if (isActiveAction)
        {
            OnActiveUpdate();
        }
    }

    protected abstract void OnActiveUpdate();

    public void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace))
        {
            Cancelled();
        }
    }

    protected void Finished(UnityEngine.Object obj)
    {
        isActiveAction = false;

        OnFinished(obj);
    }

    protected void Cancelled()
    {
        isActiveAction = false;

        CleanUpOnCancel();

        OnCancelled();
    }

    public static Rigidbody2D EnsureRigidbody2DComponent(GameObject gameObject)
    {
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.drag = 10;
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
            rb.drag = 10;
            rb.gravityScale = GRAVITY_CONSTANT;
            rb.useAutoMass = true;
        }
        return rb;
    }

    protected abstract void CleanUpOnCancel();
}
