using UnityEngine;

public interface ControllableObject
{
    bool isControlled { get; set; }

    void TakeCommand(GameObject target);
    void TakeCommand(Vector3 position);
}
