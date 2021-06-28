using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class TextSpinner : MonoBehaviour
{
    private Vector3 newRotation = Vector3.zero;

    private void Update()
    {
        TextNode3DSpin();
    }

    private void TextNode3DSpin()
    {
        transform.rotation = Quaternion.Euler(newRotation);
        newRotation.x += 2 * Time.deltaTime;
        newRotation.y += 6 * Time.deltaTime;
        newRotation.z += 4 * Time.deltaTime;
        return;
    }
}