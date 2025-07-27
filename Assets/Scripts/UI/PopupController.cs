using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupController : MonoBehaviour {
    public Dictionary<string, GameObject> popupsByName = new Dictionary<string, GameObject>();
    public Stack<GameObject> openedPopups = new Stack<GameObject>();

    private void Awake() {
        foreach (Transform child in transform) {
            AddPopup(child.gameObject.name, child.gameObject);
            child.gameObject.SetActive(false);
        }

        InputProvider.OnEscPressed += OnEscPressed;
    }

    private void OnEscPressed() {
        CloseCurrentPopup();
    }

    private void AddPopup(string popupName, GameObject popup) {
        if (!popupsByName.ContainsKey(popupName)) {
            popupsByName.Add(popupName, popup);
        } else {
            popupsByName[popupName] = popup;
        }
    }

    public GameObject GetPopup(string popupName) {
        if (!popupsByName.ContainsKey(popupName)) {
            return null;
        }
        return popupsByName[popupName];
    }

    public bool IsPopupOpen(string popupName) {
        return openedPopups.Contains(GetPopup(popupName));
    }

    public void OpenPopup(string popupName) {
        if (IsPopupOpen(popupName)) {
            // TODO: bring to front
            return;
        }
        var thePopup = GetPopup(popupName);
        thePopup.SetActive(true);
        openedPopups.Push(thePopup);
    }

    public void CloseAllThePopups() {
        foreach (var popup in openedPopups) {
            popup.SetActive(false);
        }
        openedPopups.Clear();
    }

    public void CloseCurrentPopup() {
        if (openedPopups.Count == 0) {
            return;
        }
        var popup = openedPopups.Pop();
        popup.SetActive(false);
    }
}
