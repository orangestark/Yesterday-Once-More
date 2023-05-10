using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPostEffect : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    [SerializeField] private Material _off;

    [SerializeField] private Material _on;
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.material = _off;
    }

    public void MaterialOn()
    {
        _spriteRenderer.material = _on;
    }
    
    public void MaterialOff()
    {
        _spriteRenderer.material = _off;
    }
}
