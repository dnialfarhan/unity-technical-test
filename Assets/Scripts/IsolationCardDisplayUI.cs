using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections;

    /// <summary>
    /// Handles the display and interaction logic for the isolation card UI (detailed card view).
    /// Includes pop (scale) effect on hover.
    /// </summary>
public class IsolationCardDisplayUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Card Isolation References")]
    [SerializeField] private GameObject isolationView;           // The isolation card view panel
    [SerializeField] private GameObject handView;                // The hand view panel to hide when isolating
    [SerializeField] private Image isolationCardSprite;          // Main card image
    [SerializeField] private TextMeshProUGUI isolationCardName;  // Card name
    [SerializeField] private TextMeshProUGUI isolationCardDescription; // Card description
    [SerializeField] private TextMeshProUGUI isolationCardHealth;     // Card health
    [SerializeField] private TextMeshProUGUI isolationCardAttack;     // Card attack
    [SerializeField] private Image isolationOriginIcon;          // Card origin icon
    [SerializeField] private Image isolationRarityIcon;          // Card rarity icon
    [SerializeField] private Image isolationEvolutionIcon;       // Card evolution icon

    [Header("Card Isolation Description References")]
    [SerializeField] private TextMeshProUGUI cardName;       // Card name (details panel)
    [SerializeField] private TextMeshProUGUI cardOrigin;     // Card origin (details panel)
    [SerializeField] private TextMeshProUGUI cardRarity;     // Card rarity (details panel)
    [SerializeField] private TextMeshProUGUI cardEvolution;  // Card evolution (details panel)
    [SerializeField] private TextMeshProUGUI cardHealth;     // Card health (details panel)
    [SerializeField] private TextMeshProUGUI cardAttack;     // Card attack (details panel)
    [SerializeField] private TextMeshProUGUI cardType;       // Card type and extra info

    [Header("Hover/Scale Settings")]
    [SerializeField] private float popScale = 1.1f;          // Scale factor for pop effect
    [SerializeField] private float popDuration = 0.15f;       // Duration for pop animation
    private Vector3 originalScale;                            // Original scale for reset
    private Coroutine scaleCoroutine;                         // Reference to running scale coroutine

    private void Awake()
    {
        originalScale = transform.localScale;
    }

    /// <summary>
    /// Populates the isolation card UI with the given card data and shows the isolation view.
    /// </summary>
    public void ShowCard(CardSO cardData, CardDisplayUI displayUI)
    {
        // Main card visuals
        isolationCardName.text = cardData.Name;
        isolationCardDescription.text = cardData.cardDescription;
        isolationCardSprite.sprite = cardData.cardSprite;
        isolationCardHealth.text = cardData.Health.ToString();
        isolationCardAttack.text = cardData.Attack.ToString();
        isolationOriginIcon.sprite = displayUI.GetOriginIcon(cardData.origin);
        isolationRarityIcon.sprite = displayUI.GetRarityIcon(cardData.rarity);
        isolationEvolutionIcon.sprite = displayUI.GetEvolutionIcon(cardData.evolution);

        // Details panel
        cardName.text = cardData.Name;
        cardOrigin.text = $"Card Origin: {cardData.origin}";
        cardRarity.text = $"Card Rarity: {cardData.rarity}";
        cardEvolution.text = $"Card Evolution: {cardData.evolution}";
        cardHealth.text = $"Health: {cardData.Health}";
        cardAttack.text = $"Attack: {cardData.Attack}";

        // Card type details
        switch (cardData.cardType)
        {
            case CardType.Spawn:
                cardType.text = $"Card Type: Spawn | Target: {(cardData.spawnTargetCard != null ? cardData.spawnTargetCard.Name : "None")}";
                break;
            case CardType.Stun:
                cardType.text = $"Card Type: Stun | Turns: {cardData.stunTurns}";
                break;
            case CardType.Heal:
                cardType.text = $"Card Type: Heal | Amount: {cardData.healAmount}";
                break;
            default:
                cardType.text = "Card Type: None";
                break;
        }

        isolationView.SetActive(true);
        handView.SetActive(false);

        // Reset scale when showing
        ResetScale();
    }

    /// <summary>
    /// Handles pop (scale up) effect when pointer enters the card UI.
    /// </summary>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (scaleCoroutine != null) StopCoroutine(scaleCoroutine);
        scaleCoroutine = StartCoroutine(ScaleTo(originalScale * popScale, popDuration));
    }

    /// <summary>
    /// Handles scale reset when pointer exits the card UI.
    /// </summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        if (scaleCoroutine != null) StopCoroutine(scaleCoroutine);
        scaleCoroutine = StartCoroutine(ScaleTo(originalScale, popDuration));
    }

    /// <summary>
    /// Smoothly animates the card scale to the target value over the given duration.
    /// </summary>
    private IEnumerator ScaleTo(Vector3 target, float duration)
    {
        Vector3 start = transform.localScale;
        float time = 0f;
        while (time < duration)
        {
            transform.localScale = Vector3.Lerp(start, target, time / duration);
            time += Time.unscaledDeltaTime;
            yield return null;
        }
        transform.localScale = target;
    }

    /// <summary>
    /// Instantly resets the card scale to its original value.
    /// </summary>
    private void ResetScale()
    {
        if (scaleCoroutine != null) StopCoroutine(scaleCoroutine);
        transform.localScale = originalScale;
    }
}