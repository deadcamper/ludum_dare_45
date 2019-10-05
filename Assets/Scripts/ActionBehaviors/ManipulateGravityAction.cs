using UnityEngine;

public class ManipulateGravityAction : ActionBehavior
{

    public float gravityConstant = 1f;

    public void Update()
    {
        Rigidbody2D[] bodies = level.GetComponentsInChildren<Rigidbody2D>();

        foreach(Rigidbody2D body in bodies)
        {
            body.gravityScale = gravityConstant;
        }

        ActionBehavior.GRAVITY_CONSTANT = gravityConstant;

        Finished(level);

    }

    protected override void CleanUpOnCancel()
    {
        //No Cleanup
    }
}
