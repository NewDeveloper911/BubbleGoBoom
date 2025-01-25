using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossibleExitDoors : MonoBehaviour
{
    [SerializeField] Transform room;
    
    GameObject[] taggedChildArray;
    // Call this method with the parent GameObject and the tag you're looking for
    /* public List<Transform> GetTaggedChildrenWithTag(Transform parent, string tag)
    {
        List<Transform> taggedChildren = new List<Transform>();
        GetTaggedChildrenRecursive(parent, tag, taggedChildren);
        return taggedChildren;
    }

    private void GetTaggedChildrenRecursive(Transform parent, string tag, List<Transform> taggedChildren)
    {
        foreach (Transform child in parent)
        {
            if (child.CompareTag(tag))  // Check if the child has the specified tag
            {
                taggedChildren.Add(child);
            }

            // Recursively check the child objects
            GetTaggedChildrenRecursive(child, tag, taggedChildren);
        }
    } */

    // Example usage
    void Start()
    {
        // Specify the tag you're interested in
        string tagToFind = "Finish";  // Replace with your tag
        taggedChildArray = GameObject.FindGameObjectsWithTag(tagToFind);

        // Print out all tagged children
        foreach (GameObject child in taggedChildArray)
        {
            Debug.Log(child.name);
        }
    }
}
