using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureText : MonoBehaviour
{
    [SerializeField] private Texture2D _texture;
    [Range(2, 512)] [SerializeField] private int _resolution;
}
