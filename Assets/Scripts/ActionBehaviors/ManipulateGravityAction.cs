using UnityEngine;

public class ManipulateGravityAction : ActionBehavior
{
    public override string InstructionText
    {
        get { return ""; } //Auto-applies on click
    }

    public float gravityConstant = 1f;

    protected override void OnActiveUpdate()
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
