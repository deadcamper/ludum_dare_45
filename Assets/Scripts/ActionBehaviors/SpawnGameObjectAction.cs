using UnityEngine;

public class SpawnGameObjectAction : ActionBehavior
{
    public GameObject ghostPrefab;
    public GameObject gameObjectPrefab;

    private GameObject ghostObject;
    private GameObject newObject;

    public bool isBigObject;

    private bool unlockSpawn;

    public override string InstructionText
    {
        get { return "Hold Left Mouse Button to rotate, then release button to place."; }
    }

    protected override void OnSetUp()
    {
        unlockSpawn = false;
    }

    protected override void OnActiveUpdate()
    {
        if (Input.GetMouseButtonUp(0) && unlockSpawn)
        {
            Vector3 position = ghostObject ? ghostObject.transform.position : Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
            Quaternion rotation = ghostObject ? ghostObject.transform.rotation : Quaternion.identity;

            newObject = Instantiate(gameObjectPrefab, position, rotation, level);
            EnsureCorrectedRigidbody2D(newObject);

            if (ghostObject)
                Destroy(ghostObject);

            if (isBigObject)
            {
                SoundBoard.Instance?.spawnObjectLarge?.Play();
            }
            else
            {
                SoundBoard.Instance?.spawnObjectSmall?.Play();
            }

            Finished(newObject);
        }
        else if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0))
        {
            if (Input.GetMouseButtonDown(0))
            {
                unlockSpawn = true;
            }

            if (!ghostObject)
            {
                return;
            }

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));

            Vector3 difference = worldPosition - ghostObject.transform.position;

            if (difference.sqrMagnitude > 0.01f)
            {
                float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                ghostObject.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ - 90);
            }

        }
        else if (ghostObject)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));

            ghostObject.transform.position = worldPosition;
        }
        else
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));

            ghostObject = Instantiate(ghostPrefab, worldPosition, Quaternion.identity);
        }
    }

    protected override void CleanUpOnCancel()
    {
        if (ghostObject)
        {
            Destroy(ghostObject);
        }

        if (newObject)
        {
            Destroy(newObject);
        }
    }
}

