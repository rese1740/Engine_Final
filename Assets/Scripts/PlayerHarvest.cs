using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHarvest : MonoBehaviour
{
    public float rayDistance = 5f;
    public LayerMask hitMask = ~0;
    public int toolDamage = 1;
    public float hitCooldown = 0.15f;
    private float _nextHitTime;
    private Camera _cam;
    public Inventory inventory;

    Transform playerTransform;
    public float interactionRange = 2.0f;
    public LayerMask interactionMask = 1;
    private void Awake()
    {
        playerTransform = transform;
        _cam = Camera.main;
        if (inventory == null) inventory = gameObject.AddComponent<Inventory>();
    }

    private void Update()
    {
        CheckForInteractables();
        if (Input.GetMouseButton(0) && Time.time >= _nextHitTime)
        {
            _nextHitTime = Time.time + hitCooldown;

            Ray ray = _cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            if (Physics.Raycast(ray, out var hit, rayDistance, hitMask))
            {
                var block = hit.collider.GetComponent<Block>();
                if (block != null)
                    block.Hit(toolDamage, inventory);
            }
        }
    }

    void CheckForInteractables()
    {
        // 플레이어 중심에서 구형 범위로 탐색
        Collider[] hitColliders = Physics.OverlapSphere(playerTransform.position, interactionRange, interactionMask);

        Block closestInteractable = null;
        float closestDistance = float.MaxValue;

        foreach (Collider collider in hitColliders)
        {
            Block interactable = collider.GetComponent<Block>();
            if (interactable != null)
            {
                float distance = Vector3.Distance(playerTransform.position, collider.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestInteractable = interactable;
                }
            }
        }

        if (closestInteractable != null)
        {
            closestInteractable.GetItem();
        }
    }
}
