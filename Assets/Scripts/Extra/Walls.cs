using UnityEngine;

public class Walls : MonoBehaviour
{
    [SerializeField] private GameObject left;
    [SerializeField] private GameObject right;
    [SerializeField] private GameObject up;
    [SerializeField] private GameObject down;

    [SerializeField] private BoxCollider2D area;

    private float leftS, rightS, upS, downS;
    private Vector3 screenWorld;
    private void Start()
    {
        DynamicWalls();
        Area();     
    }

    private void DynamicWalls()
    {
        screenWorld = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));

        leftS = Mathf.Round(-(screenWorld.x) + (left.GetComponent<SpriteRenderer>().bounds.size.x / 2));
        left.transform.position = new Vector3(leftS, 0);

        rightS = Mathf.Round((screenWorld.x) - (right.GetComponent<SpriteRenderer>().bounds.size.x / 2));
        right.transform.position = new Vector3(rightS, 0);

        upS = Mathf.Round((screenWorld.y) - (up.GetComponent<SpriteRenderer>().bounds.size.y / 2));
        up.transform.position = new Vector3(0, upS);

        downS = Mathf.Round(-(screenWorld.y) + (down.GetComponent<SpriteRenderer>().bounds.size.y / 2));
        down.transform.position = new Vector3(0, downS);
    }

    private void Area()
    {
        area.size = new Vector2((rightS) * 2,(upS ) * 2);
    }
}
