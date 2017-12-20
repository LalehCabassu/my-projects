package personalHealthInfo;

public class EmergencyContact extends Person {

	private String relationship;
	
	public EmergencyContact(Person person, String relationship)
	{
		super(person);
		SetRelationship(relationship);
	}
	
	public String GetRelationship()
	{
		return this.relationship;
	}
	
	public void SetRelationship(String relationship)
	{
		this.relationship = relationship;
	}
	
	public @Override String toString()
	{
		String result = "Emergency Contact: ";
		
		result += this.relationship + "\n";
		result += super.toString();
		
		return result;
	}
}
