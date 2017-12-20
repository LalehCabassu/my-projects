package aspects.pii;

import java.beans.PropertyChangeEvent;
import java.beans.PropertyChangeListener;

public class PIIObserver implements PropertyChangeListener {

	@Override
	public void propertyChange(PropertyChangeEvent e) {

		System.out.println("Property '" + e.getPropertyName() +
							"'\nCHANGED from " + e.getOldValue() + "to" + e.getNewValue());
	}

	
}
