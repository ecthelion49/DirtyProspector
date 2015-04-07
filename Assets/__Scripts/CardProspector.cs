using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// This ia an enum, which defines a type of variable that only has a few
// possible name values. The CardState variable type has one of gour values
// drawoukem tableau, target and discard
public enum CardState {
	drawpile,
	tableau,
	target,
	discard
}

public class CardProspector : Card {	// Make sure CardProspector extends card
	// This is how you use the enum CardState
	public CardState			state = CardState.drawpile;

	// The hiddenBy list stores which other cards will keep this one face down
	public List<CardProspector> hiddenBy = new List<CardProspector>();

	// LayoutID matches this card to a Layout XML id if its a tableau card
	public int					layoutID;

	// The slotdef class stores information pulled in from the LayoutXML <slot>
	public SlotDef				slotDef;

	// This allows the card to react to being clicked
	override public void OnMouseUpAsButton () {
		// Call the CardCLicked method on the Prospector sinfleton
		Prospector.S.CardClicked (this);
		// Also call the base calss (card.cs) cersion of this method
		base.OnMouseUpAsButton ();
	}



}
