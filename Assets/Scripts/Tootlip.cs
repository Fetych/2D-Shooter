using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using TMPro;

public class Tootlip : MonoBehaviour
{
    public static string Text;
    public static bool IsUI;
    public Color BGColor;// = Color.white;
    public Color TextColor;// = Color.black;
    public int MaxWidth; // максимальная ширина Tooltip
    public int Border; // ширина обводки
    public RectTransform Box;
    //public RectTransform arrow;
    public TextMeshProUGUI BoxText;
    public float Speed = 10; // скорость плавного затухания и проявления
    public Camera Camera;
    bool Show = false;

    private void Awake()
    {
        IsUI = false;
        Camera = GetComponent<Camera>();
    }
    //private void OnMouseDown()
    //{
    //    Cursor.SetCursor(cursorDown, Vector2.zero, CursorMode.Auto);
    //}

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.transform != null)
        {
            if (hit.transform.TryGetComponent(out AmountMoney amountMoney))
            {
                Text = amountMoney.AmountOfMoney.ToString();
                Show = true;
            }
        }
        BoxText.text = Text;
        float width = MaxWidth;
    }
}
