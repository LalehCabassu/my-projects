package aspects.pii;

import java.beans.PropertyChangeListener;
import java.beans.PropertyChangeSupport;

public aspect SubjectObserverProtocol {

	private PropertyChangeSupport PIIObserver.propertyChangeSupport =
			new PropertyChangeSupport(this);
	
	public void PIIObserver.addPropertyChangeListener(PropertyChangeListener listener)
	{
		this.propertyChangeSupport.addPropertyChangeListener(listener);
	}
	
	public void PIIObserver.addPropertyChangeListener(String subjectName, PropertyChangeListener listener)
	{
		this.propertyChangeSupport.addPropertyChangeListener(subjectName, listener);
	}
	
	public void PIIObserver.removePropertyChangeListener(PropertyChangeListener listener)
	{
		this.propertyChangeSupport.addPropertyChangeListener(listener);
	}
	
	public void PIIObserver.removePropertyChangeListener(String subjectName, PropertyChangeListener listener)
	{
		this.propertyChangeSupport.removePropertyChangeListener(subjectName, listener);
	}
	
	public void PIIObserver.hasListeners(String subjectName)
	{
		this.propertyChangeSupport.hasListeners(subjectName);
	}
	
	public void firePropertyChange(PIIObserver observer, String subject, String oldValue, String newValue)
	{
		observer.propertyChangeSupport.firePropertyChange(subject, oldValue, newValue);
	}
}
