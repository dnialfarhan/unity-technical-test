using UnityEngine;
using TMPro;

/// <summary>
/// Handles card logic and ability execution.
/// </summary>
public class Card : MonoBehaviour
{
    /// <summary>
    /// Executes the card's ability and updates the provided description text.
    /// </summary>
    /// <param name="card">The card data (CardSO).</param>
    /// <param name="target">The target GameObject (not used in this implementation).</param>
    /// <param name="Description">The TextMeshProUGUI to display the result.</param>
    public void UseCard(CardSO card, GameObject target, TextMeshProUGUI Description)
    {
        string extraLine = "";

        // Determine extra line based on card type
        switch (card.cardType)
        {
            case CardType.None:
                Description.text =
                    $"{card.Name} was executed.\n" +
                    $"Deal {card.Attack} damage to enemy units.";
                return;
            case CardType.Spawn:
                extraLine = $"Spawn {card.spawnAmount} {(card.spawnTargetCard != null ? card.spawnTargetCard.Name : "unit")} unit(s).";
                break;
            case CardType.Heal:
                extraLine = $"Heal ally units by {card.healAmount}.";
                break;
            case CardType.Stun:
                extraLine = $"Stun enemy units for {card.stunTurns} turn(s).";
                break;
            default:
                extraLine = "";
                break;
        }

        Description.text = BuildDescription(card, extraLine);
    }

    /// <summary>
    /// Builds the description string for the card's effect.
    /// </summary>
    /// <param name="card">The card data.</param>
    /// <param name="extraLine">The extra effect line.</param>
    /// <returns>Formatted description string.</returns>
    private string BuildDescription(CardSO card, string extraLine)
    {
        string desc =
            $"{card.Name} was executed.\n" +
            $"Deal {card.Attack} damage to enemy units.";
        if (!string.IsNullOrEmpty(extraLine))
            desc += $"\n{extraLine}";
        return desc;
    }
}
