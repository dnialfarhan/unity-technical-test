using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the player's hand of cards, including drawing, clearing, and card selection logic.
/// </summary>
public class Hand : MonoBehaviour
{
    [Header("Settings")]
    public int cardsToDraw = 5;                     // Number of cards to draw at start
    public Transform handTransform;                 // Parent transform to hold card UIs
    public GameObject cardUIPrefab;                 // Prefab with CardDisplayUI.cs attached

    [Header("Card Pool")]
    public List<CardSO> allAvailableCards;          // Master list of all possible cards

    [Header("UI Buttons")]
    public Button executeButton;                    // Button to execute card ability
    public Button viewDetailsButton;                // Button to view card details

    private CardDisplayUI selectedCard;             // Currently selected card

    void Start()
    {
        DrawCards(cardsToDraw);
    }

    /// <summary>
    /// Removes all cards from the hand and disables action buttons.
    /// </summary>
    public void ClearHand()
    {
        foreach (Transform child in handTransform)
        {
            Destroy(child.gameObject);
        }
        selectedCard = null;
        executeButton.gameObject.SetActive(false);
        viewDetailsButton.gameObject.SetActive(false);
    }

    /// <summary>
    /// Draws a specified number of cards from the pool into the hand.
    /// </summary>
    /// <param name="amount">Number of cards to draw.</param>
    public void DrawCards(int amount)
    {
        ClearHand();

        if (allAvailableCards == null || allAvailableCards.Count == 0)
        {
            Debug.LogWarning("No cards available in the card pool.");
            return;
        }

        for (int i = 0; i < amount; i++)
        {
            CardSO randomCard = allAvailableCards[Random.Range(0, allAvailableCards.Count)];

            GameObject cardGO = Instantiate(cardUIPrefab, handTransform);
            CardDisplayUI display = cardGO.GetComponent<CardDisplayUI>();

            if (display != null)
            {
                display.Setup(randomCard);
            }
            else
            {
                Debug.LogError("Card UI prefab is missing CardDisplayUI script.");
            }
        }
    }

    /// <summary>
    /// Sets the currently selected card and wires up the action buttons.
    /// </summary>
    /// <param name="card">The selected CardDisplayUI.</param>
    public void SetSelectedCard(CardDisplayUI card)
    {
        selectedCard = card;

        // Enable buttons when a card is selected
        executeButton.gameObject.SetActive(true);
        viewDetailsButton.gameObject.SetActive(true);

        // Remove old listeners to prevent stacking
        executeButton.onClick.RemoveAllListeners();
        viewDetailsButton.onClick.RemoveAllListeners();

        // Add new listeners for the selected card
        executeButton.onClick.AddListener(card.ExecuteCardAction);
        viewDetailsButton.onClick.AddListener(card.ViewCardDetails);
    }

    /// <summary>
    /// Deselects the current card and disables action buttons.
    /// </summary>
    public void DeselectCard()
    {
        selectedCard = null;
        executeButton.gameObject.SetActive(false);
        viewDetailsButton.gameObject.SetActive(false);
    }
}
