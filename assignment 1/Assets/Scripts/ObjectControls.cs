using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectControls : MonoBehaviour
{
    public GameObject peter;
    public GameObject cube;
    public GameObject capsule;
    public TextMesh selectedLabel;

    private GameObject selected;
    private int selectedNumber = 1;
    private Vector2 movement;
    private float rotation;
    private float scale;

    void Start()
    {
        selected = peter;
    }

    void Update()
    {
        Vector3 direction = new Vector3(movement.x, movement.y, 0);
        selected.transform.Translate(direction * 2f * Time.deltaTime, Space.World);
        selected.transform.Rotate(0, 0, rotation * 90f * Time.deltaTime, Space.World);

        float size = selected.transform.localScale.x + scale * Time.deltaTime;
        size = Mathf.Clamp(size, 0.35f, 2f);
        selected.transform.localScale = new Vector3(size, size, size);

        selectedLabel.text = "Selected: " + selectedNumber + " - " + selected.name;
        if (selected.activeSelf)
        {
            selectedLabel.text += "  /  ON";
        }
        else
        {
            selectedLabel.text += "  /  OFF";
        }
    }

    public void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();
    }

    public void OnRotate(InputValue value)
    {
        rotation = value.Get<float>();
    }

    public void OnScale(InputValue value)
    {
        scale = value.Get<float>();
    }

    public void OnSelectPeter(InputValue value)
    {
        if (value.isPressed)
        {
            selected = peter;
            selectedNumber = 1;
        }
    }

    public void OnSelectCube(InputValue value)
    {
        if (value.isPressed)
        {
            selected = cube;
            selectedNumber = 2;
        }
    }

    public void OnSelectCapsule(InputValue value)
    {
        if (value.isPressed)
        {
            selected = capsule;
            selectedNumber = 3;
        }
    }

    public void OnToggle(InputValue value)
    {
        if (value.isPressed)
        {
            if (selected.activeSelf)
            {
                selected.SetActive(false);
            }
            else
            {
                selected.SetActive(true);
            }
        }
    }
}
