using UnityEngine;

/// <summary>
/// ScriptableObject representing a card's data and stats.
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObjects/Card")]
public class CardSO : ScriptableObject
{
    [Header("Basic Info")]
    public string Name;                         // Name of the card
    public Sprite cardSprite;                   // Sprite for the card
    [TextArea(3, 15)]
    public string cardDescription;              // Description of the card

    [Header("Card Stats")]
    public CardOrigin origin;                   // Origin of the card
    public CardRarity rarity;                   // Rarity of the card
    public CardEvolution evolution;             // Evolution stage of the card
    public int Health;                          // Health value
    public int Attack;                          // Attack value

    [Header("Card Abilities")]
    public CardType cardType;                   // Type of the card (Spawn, Stun, Heal, etc.)

    [Header("Spawn Ability")]
    public CardSO spawnTargetCard;              // Reference to another CardSO to spawn
    public int spawnAmount;                     // Number of units to spawn

    [Header("Stun Ability")]
    public int stunTurns;                       // Stun duration in turns

    [Header("Heal Ability")]
    public float healAmount;                    // Amount to heal
}

/// <summary>
/// Rarity levels for cards.
/// </summary>
public enum CardRarity
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary,
    Mythical
}

/// <summary>
/// Origin types for cards.
/// </summary>
public enum CardOrigin
{
    Water,
    Fire,
    Air,
    Earth
}

/// <summary>
/// Card ability types.
/// </summary>
public enum CardType
{
    None,
    Spawn,
    Stun,
    Heal,
}

/// <summary>
/// Evolution stages for cards.
/// </summary>
public enum CardEvolution
{
    EvolutionI,
    EvolutionII,
    EvolutionIII,
}