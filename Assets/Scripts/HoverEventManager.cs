using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverEventManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Animator animator;
    public GameObject glowWithSpriteRendererGameObject;
    private SpriteRenderer glowRenderer;

    // Start is called before the first frame update
    void Start()
    {
        animator= GetComponent<Animator>();
        glowRenderer=glowWithSpriteRendererGameObject.GetComponent<SpriteRenderer>();
        glowRenderer.enabled = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.SetBool("Hover", true);
        glowRenderer.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.SetBool("Hover", false);
        glowRenderer.enabled = false;
    }
}
