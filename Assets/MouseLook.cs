using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private Transform playerBody;

    private float xRotation = 0f;

    private void Start()
    {
        // Прячем курсор и фиксируем его в центре экрана
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Mouse.current == null) return;

        // Читаем движение мыши и умножаем на чувствительность
        Vector2 mouseDelta = Mouse.current.delta.ReadValue() * mouseSensitivity * Time.deltaTime;

        // Вертикальный поворот — только камера (вверх-вниз)
        xRotation -= mouseDelta.y;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // ограничение, чтобы не переворачивалась
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Горизонтальный поворот — всё тело игрока (влево-вправо)
        playerBody.Rotate(Vector3.up * mouseDelta.x);
    }

    private void OnDisable()
    {
        // Возвращаем курсор, если скрипт отключили
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}