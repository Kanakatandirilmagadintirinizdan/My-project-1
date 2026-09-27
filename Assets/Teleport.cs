using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Transform teleportPoint;
    [SerializeField] private string playerTag = "Player";

    private bool isTeleporting;

    private void OnTriggerEnter(Collider other)
    {
        if (isTeleporting) return;
        if (!other.CompareTag(playerTag)) return;
        if (teleportPoint == null)
        {
            Debug.LogWarning("[Teleport] Не указана точка телепорта!");
            return;
        }

        isTeleporting = true;

        CharacterController controller = other.GetComponent<CharacterController>();

        // Отключаем CharacterController перед сменой позиции
        if (controller != null) controller.enabled = false;

        other.transform.position = teleportPoint.position;
        other.transform.rotation = teleportPoint.rotation;

        if (controller != null) controller.enabled = true;

        // Сбрасываем флаг через 0.5 секунды, чтобы не зациклить телепорт
        Invoke(nameof(ResetTeleport), 0.5f);
    }

    private void ResetTeleport() => isTeleporting = false;
}