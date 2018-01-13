using UnityEngine;

public class InputPatchTest : MonoBehaviour
{
    private void Awake()
    {
        InputPatcher.Patch();
    }

    private void Update()
    {
        if (Input.GetKeyDown("Fire1"))
        {
            Debug.Log("down");
        }
    }
}