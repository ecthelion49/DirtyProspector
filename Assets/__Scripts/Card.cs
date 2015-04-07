using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Card : MonoBehaviour {
	public string			suit;					// Suit of the Card (C, D, H or S)
	public int				rank;					// Rabk of the card, 1-14
	public Color			color = Color.black;	// Color to tint pips
	public string			colS = "Black";			// Or red. Name of the color

	// This list holds all of the Decorator GameObjects
	public List<GameObject> decoGOs = new List<GameObject> ();

	// This list golds all of the Pip GameObjects
	public List<GameObject> pipGOs = new List<GameObject> ();

	public GameObject		back;	// The GameObject of the back of the card

	public CardDefinition	def;	// Parsed for DeckXML.xml

	// List of the SpriteRenderer Components of this GameObhect and its children
	public SpriteRenderer[]	spriteRenderers;

	void Start() {
		SetSortOrder (0);	// Ensures that the card starts properly depth sorted
	}

	public bool faceUp {
		get {
			return(!back.activeSelf);
		}
		set {
			back.SetActive (!value);
		}
	}

	// If spriteRenderers is not yet defined, this function defines it
	public void PopulateSpriteRenderers() {
		// If spriteRenderers is null or empty
		if (spriteRenderers == null || spriteRenderers.Length == 0) {
			// Get SpriteRenderer Components of this GameObject and its children
			spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
		}
	}

	// Sets the sortingLayerName on all SPriteRenderer Components
	public void SetSortingLayerName(string tSLN) {
		PopulateSpriteRenderers ();

		foreach (SpriteRenderer tSR in spriteRenderers) {
			tSR.sortingLayerName = tSLN;
		}
	}

	// Sets the sortingOrder of all SpriteRenderer Components
	public void SetSortOrder(int sOrd) {
		PopulateSpriteRenderers ();

		// The white backfround of the card is on bottom (sOrd)
		// On top of that are all the pips, decorators, face etc (sOrd + 1)
		// The back is on top so that when visible, it covers the rest (sOrd + 2)

		// Iterate through all the spriteRenderers as tSR
		foreach (SpriteRenderer tSR in spriteRenderers) {
			if (tSR.gameObject == this.gameObject) {
				// If the gameObject is this.gameObject, its the background
				tSR.sortingOrder = sOrd;	// Set its order to sOrd
				continue;	// And continue to the next iteration of the loop
			}

			// Each of the children of this GameObject are names
			// switch nased on the names
			switch (tSR.gameObject.name) {
			case "back":		// If the name is "back"
				tSR.sortingOrder = sOrd + 2;
				// ^ Set it to the highest layer to cover everything else
				break;
			case "face":		// If the name is "face"
			default:			// Or if it is anyhting else
				tSR.sortingOrder = sOrd + 1;
				// ^ Set it to the middle layer to be above the background
				break;
			}
		}
	}

	// Virtual methods can be overridden by cubclass methods with the same name
	virtual public void OnMouseUpAsButton() {
		print (name); // When clicked iths outputs the card name
	}
}

[System.Serializable]
public class Decorator {
	// This class stores indormation about each decorator or pip from DeckXML
	public string		type;				// For card pips, type = pip
	public Vector3		loc;				// The location of the spreite on the card
	public bool			flip = false;		// Wheith the Sprite flips vertically
	public float		scale = 1f;			// The scale of the Sprite
}

[System.Serializable]
public class CardDefinition {
	// This class stores information for each rank of card
	public string			face; 							// Sprite to use for each face card
	public int				rank;							// The rank (1-13) of this card
	public List<Decorator>	pips = new List<Decorator>();	// Pips used

	// Because decorators (from the XML) are used th same way on every card in the deck 
	// pips only stores information about the pips on numbered cards
}

