using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryViewScript : MonoBehaviour
{
    [SerializeField] private GameObject imageParent;
    [SerializeField] private float scrollTime = 0.5f;
    [SerializeField] private InventoryScript inventoryScript;

    private float mouseScroll;
    private bool scrolling = false;
    private int currentItemIndex = 0;
    private int maxItemIndex;


    // Start is called before the first frame update
    void Start()
    {
        maxItemIndex = inventoryScript.items.Count - 1;
        UpdateInvetoryView();

        imageParent.transform.GetChild(currentItemIndex).localScale = new Vector3(1.1f, 1.1f, 1.1f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 vec = Mouse.current.scroll.ReadValue();
        mouseScroll = vec.y;

        if (mouseScroll > 0)
        {
            if (!scrolling && currentItemIndex < maxItemIndex)
            {
                StartCoroutine(ScrollImages(1));
            }
        }
        if (mouseScroll < 0)
        {
            if (!scrolling && currentItemIndex > 0)
            {
                StartCoroutine(ScrollImages(-1));
            }
        }
    }

    public ItemSO GetCurrentItem()
    {
        return inventoryScript.items[currentItemIndex];
    }

    private void UpdateInvetoryView()
    {
        int itemCount = inventoryScript.items.Count;
        for (int i = 0; i < imageParent.transform.childCount; i++)
        {
            if (i < itemCount)
                imageParent.transform.GetChild(i).gameObject.SetActive(true);
            else
                imageParent.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    private IEnumerator ScrollImages(int dir)
    {
        scrolling = true;
        float timer = 0;
        float t = 0;

        while (timer < scrollTime)
        {
            // deltaTime * time spent for scroll * direction * amount to scroll
            float delta = Time.deltaTime * (1f / scrollTime) * dir * 120f;
            imageParent.transform.Translate(Vector3.up * delta, Space.Self);

            t += Time.deltaTime * 1f / scrollTime;
            float lerp = Mathf.Lerp(1.1f, 1.0f, t);
            Transform transform = imageParent.transform.GetChild(currentItemIndex);
            transform.localScale = new Vector3(lerp, lerp, lerp);

            lerp = Mathf.Lerp(1.0f, 1.1f, t);
            transform = imageParent.transform.GetChild(currentItemIndex + dir);
            transform.localScale = new Vector3(lerp, lerp, lerp);

            timer += Time.deltaTime;
            yield return null;
        }
        currentItemIndex += dir;
        scrolling = false;
    }
}
