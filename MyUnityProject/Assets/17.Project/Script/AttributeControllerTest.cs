
using UnityEngine;
using UnityEngine.UI;

public class AttributeControllerTest : MonoBehaviour
{
    [Color(0,1,0,1)]
    public Renderer rend;

    [SerializeField, Color(r:1, b:0.5f)]
    private Graphic graphic;

    [Color]
    public float notRendererOrGraphic;
}
