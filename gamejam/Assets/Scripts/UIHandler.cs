using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;
public class UIHandler : MonoBehaviour {
    public Canvas canvas;

    private KeyCode inventoryKey = KeyCode.E;

    private Transform currentItem;
    public Transform inventory;
    public GameObject inventorySlots;

    private bool itemCanMove = false;
    private bool slotTaken = false;

    public Transform[] items;
    private Transform takeSlot;
    private Transform placeSlot;
    private Transform replacingItem;

    private PlayerMover playerMover;

    [Header("Attack GameObject Text")]
    [SerializeField] private TextMeshProUGUI meleeText = null;
    [SerializeField] private TextMeshProUGUI weaponText = null;
    [SerializeField] private TextMeshProUGUI buildText = null;
    [SerializeField] private AttackMode defaultAttack = AttackMode.Melee;
    private TextMeshProUGUI activeObject = null;

    private void Start() {
        inventory.gameObject.SetActive(false);
        LinkPlayer();
        InitAttackUI();
    }

    private void Update() {
        MoveItem();
        if (Input.GetKeyDown(inventoryKey)) {
            inventory.gameObject.SetActive(!inventory.gameObject.activeSelf);
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            inventory.gameObject.SetActive(false);
        }
        CheckClick();
    }

    private void CheckClick() {
        if (Input.GetMouseButtonDown(0)) {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Construct a ray from the current mouse coordinates
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) {
                if (hit.collider != null) {
                    if (hit.collider.transform.tag == "Slot") {
                        print("Hit");
                        ProcessGrab(hit.collider.transform);
                    }
                    else {
                        if (currentItem != null) { currentItem = null; }
                    }
                }
                else {
                    if (currentItem != null) { currentItem = null; }
                }
            }
        }
        else if (Input.GetMouseButtonUp(0)) {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Construct a ray from the current mouse coordinates
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                if (hit.collider != null) {
                    if (hit.collider.transform.tag == "Slot") {
                        ProcessPlace(hit.collider.transform);
                    }
                    else {
                        if (currentItem != null) {
                            itemCanMove = false;
                            currentItem.localPosition = Vector3.zero;
                            takeSlot = null;
                            placeSlot = null;
                        }
                    }
                }
            }
        else {
                //when you let go of the mouse and its not over another slot: just returns the item to the slot you got it from
                if (currentItem != null) {
                    itemCanMove = false;
                    currentItem.localPosition = Vector3.zero;
                    takeSlot = null;
                    placeSlot = null;
                }
            }
        }
    }

    private void ProcessGrab(Transform slot) {
        try {
            currentItem = slot.GetChild(0).GetComponent<Transform>();
            takeSlot = slot;
            itemCanMove = true;
        }
        catch {
            currentItem = null;
        }
    }

    private void MoveItem() {
        if (itemCanMove) {
            //Vector3 mousePos = Input.mousePosition;
            //currentItem.localPosition = new Vector3(mousePos.x, mousePos.y, 0);
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out pos);
            currentItem.position = canvas.transform.TransformPoint(pos);
        }
    }

    private void ProcessPlace(Transform slot) {
        foreach (Transform item in items) {
            if (item.position == slot.position) {
                replacingItem = item;
                slotTaken = true;
            }
        }
        if (!slotTaken && currentItem != null) {
            itemCanMove = false;
            currentItem.SetParent(slot);
            currentItem.localPosition = Vector3.zero;
            placeSlot = slot;
            takeSlot = null;
            placeSlot = null;

        }
        else if (currentItem != null){
            slotTaken = false;
            itemCanMove = false;
            currentItem.SetParent(slot);
            placeSlot = slot;
            currentItem.localPosition = Vector3.zero;
            replacingItem.SetParent(takeSlot);
            replacingItem.localPosition = Vector3.zero;
            takeSlot = null;
            placeSlot = null;
        }
    }

    private bool LinkPlayer()
    {
        playerMover = PlayerManager.instance.player.GetComponent<PlayerMover>();
        if (playerMover != null)
        {
            playerMover.onAttackChanged += SwitchAttack;
            return true;
        }
        return false;
    }

    private void InitAttackUI()
    {
        Color textColor = meleeText.color;
        textColor.a = 0.5f;
        meleeText.color = textColor;
        weaponText.color = textColor;
        buildText.color = textColor;

        SwitchAttack(defaultAttack);
    }

    private void SwitchAttack(AttackMode mode)
    {
        Color currentColor;

        if (activeObject != null)
        {
            currentColor = activeObject.color;
            currentColor.a = 0.5f;
            activeObject.color = currentColor;
        }

        switch (mode)
        {
            case AttackMode.Melee:
                activeObject = meleeText;
                break;
            case AttackMode.Weapon:
                activeObject = weaponText;
                break;
            case AttackMode.Build:
                activeObject = buildText;
                break;
        }

        currentColor = activeObject.color;
        currentColor.a = 1.0f;
        activeObject.color = currentColor;

    }
}
