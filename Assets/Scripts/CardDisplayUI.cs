using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections;

    /// <summary>
    /// Handles the display and interaction logic for a card UI element.
    /// </summary>
public class CardDisplayUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("UI References")]
    public Image cardSprite;                        // Card image
    public TextMeshProUGUI cardName;                // Card name text
    public TextMeshProUGUI cardDescription;         // Card description text
    public TextMeshProUGUI cardHealth;              // Card health value text
    public TextMeshProUGUI cardAttack;              // Card attack value text
    public Image originIcon;                        // Card origin icon
    public Image rarityIcon;                        // Card rarity icon
    public Image evolutionIcon;                     // Card evolution icon

    [Header("Origin Icon References")]
    public Sprite waterIcon;
    public Sprite fireIcon;
    public Sprite airIcon;
    public Sprite earthIcon;

    [Header("Rarity Icon References")]
    public Sprite commonIcon;
    public Sprite uncommonIcon;
    public Sprite rareIcon;
    public Sprite epicIcon;
    public Sprite legendaryIcon;
    public Sprite mythicalIcon;

    [Header("Evolution Icon References")]
    public Sprite evolutionIIcon;
    public Sprite evolutionIIIcon;
    public Sprite evolutionIIIIcon;

    [Header("Card Hover References")]
    private Vector3 originalScale;
    [SerializeField] private float popScale = 1.1f;         // Scale factor when hovered/selected
    [SerializeField] private float popDuration = 0.15f;     // Animation duration

    [Header("Card Isolation References")]
    [SerializeField] private IsolationCardDisplayUI isolationCardDisplayUI; // Reference to isolation UI

    [Header("Execution References")]
    [SerializeField] private TextMeshProUGUI executeDescription;            // Text for execution feedback

    [Header("Misc References")]
    private CardSO cardData;                                // Card data reference
    private static CardDisplayUI currentlySelectedCard;     // Tracks the currently selected card
    public Hand hand;                                       // Reference to the hand manager

    private void Awake()
    {
        // Cache original scale for pop animation
        originalScale = transform.localScale;

        // Find references if not set in Inspector
        if (isolationCardDisplayUI == null)
            isolationCardDisplayUI = GameObject.Find("Manager").GetComponent<IsolationCardDisplayUI>();
        if (hand == null)
            hand = GameObject.Find("Manager").GetComponent<Hand>();
        if (executeDescription == null)
            executeDescription = GameObject.Find("Execute Description (Text)").GetComponent<TextMeshProUGUI>();
    }

    /// <summary>
    /// Sets up the card UI with the given card data.
    /// </summary>
    public void Setup(CardSO card)
    {
        cardData = card;

        cardName.text = card.Name;
        cardDescription.text = card.cardDescription;
        cardSprite.sprite = card.cardSprite;
        cardHealth.text = card.Health.ToString();
        cardAttack.text = card.Attack.ToString();

        originIcon.sprite = GetOriginIcon(card.origin);
        rarityIcon.sprite = GetRarityIcon(card.rarity);
        evolutionIcon.sprite = GetEvolutionIcon(card.evolution);
    }

    /// <summary>
    /// Handles card selection logic and pop effect.
    /// </summary>
    public void OnPointerClick(PointerEventData eventData)
    {
        // Reset previous selection
        if (currentlySelectedCard != null && currentlySelectedCard != this)
            currentlySelectedCard.ResetScale();

        currentlySelectedCard = this;
        transform.localScale = originalScale * popScale;

        // Notify hand manager of selection
        if (hand != null)
            hand.SetSelectedCard(this);
    }

    /// <summary>
    /// Handles pop effect on hover.
    /// </summary>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (currentlySelectedCard != this)
            StartCoroutine(ScaleTo(originalScale * popScale, popDuration));
    }

    /// <summary>
    /// Resets pop effect when pointer exits, unless selected.
    /// </summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        if (currentlySelectedCard != this)
            StartCoroutine(ScaleTo(originalScale, popDuration));
    }

    /// <summary>
    /// Smoothly animates the card scale.
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
    /// Executes the card's ability and shows feedback.
    /// </summary>
    public void ExecuteCardAction()
    {
        Card cardComponent = GetComponent<Card>();
        if (cardComponent != null)
        {
            cardComponent.UseCard(cardData, gameObject, executeDescription);
        }
        else
        {
            executeDescription.text = "No Card component found!";
        }
    }

    /// <summary>
    /// Shows this card in the isolation view.
    /// </summary>
    public void ViewCardDetails()
    {
        if (isolationCardDisplayUI != null)
        {
            isolationCardDisplayUI.ShowCard(cardData, this);
        }
    }

    /// <summary>
    /// Instantly resets the card's scale to original.
    /// </summary>
    private void ResetScale()
    {
        StopAllCoroutines();
        StartCoroutine(ScaleTo(originalScale, popDuration));
    }

    /// <summary>
    /// Gets the correct origin icon for the given origin.
    /// </summary>
    public Sprite GetOriginIcon(CardOrigin origin)
    {
        return origin switch
        {
            CardOrigin.Fire => fireIcon,
            CardOrigin.Water => waterIcon,
            CardOrigin.Air => airIcon,
            CardOrigin.Earth => earthIcon,
            _ => null
        };
    }

    /// <summary>
    /// Gets the correct rarity icon for the given rarity.
    /// </summary>
    public Sprite GetRarityIcon(CardRarity rarity)
    {
        return rarity switch
        {
            CardRarity.Common => commonIcon,
            CardRarity.Uncommon => uncommonIcon,
            CardRarity.Rare => rareIcon,
            CardRarity.Epic => epicIcon,
            CardRarity.Legendary => legendaryIcon,
            CardRarity.Mythical => mythicalIcon,
            _ => null
        };
    }

    /// <summary>
    /// Gets the correct evolution icon for the given evolution.
    /// </summary>
    public Sprite GetEvolutionIcon(CardEvolution evolution)
    {
        return evolution switch
        {
            CardEvolution.EvolutionI => evolutionIIcon,
            CardEvolution.EvolutionII => evolutionIIIcon,
            CardEvolution.EvolutionIII => evolutionIIIIcon,
            _ => null
        };
    }
}
