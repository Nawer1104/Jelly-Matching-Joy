using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private bool _dragging = true;

    private Vector2 _offset;

    public static bool mouseButtonReleased;

    public GameObject vfxSuccess;

    private Animator anim;

    public bool canCollide = true;

    private void Awake()
    {
        anim = GetComponent<Animator>();

    }

    private void Start()
    {
        anim.SetTrigger("Intro");
    }

    private void OnMouseDown()
    {
        _offset = GetMousePos() - (Vector2)transform.position;
    }

    private void OnMouseDrag()
    {
        if (!_dragging) return;

        var mousePosition = GetMousePos();

        transform.position = mousePosition - _offset;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canCollide && !collision.GetComponent<DragAndDrop>().canCollide) return;

        if (collision != null && collision.tag == gameObject.tag)
        {
            _dragging = false;
            GameObject vfx = Instantiate(vfxSuccess, transform.position, Quaternion.identity);
            Destroy(vfx, 1f);
            GameManager.Instance.levels[GameManager.Instance.GetCurrentIndex()].gameObjects.Remove(gameObject);
            gameObject.SetActive(false);
        }
    }


    private void OnMouseUp()
    {
        mouseButtonReleased = true;
    }

    private Vector2 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}