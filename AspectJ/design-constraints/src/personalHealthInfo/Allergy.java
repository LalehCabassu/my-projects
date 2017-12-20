package personalHealthInfo;

public class Allergy {
	private String allergen;
	private int severity;
	
	public Allergy(String allergen, int severity)
	{
		this.allergen = allergen;
		this.severity = severity;
	}
	
	public String GetAllergen()
	{
		return this.allergen;
	}
	
	public int GetSeverity()
	{
		return this.severity;
	}
	
	public void SetAllergen(String allergen)
	{
		this.allergen = allergen;
	}
	
	public void SetSeverity(int severity)
	{
		this.severity = severity;
	}
	
	public @Override String toString()
	{
		String result = "Allergy\n";
		
		result += "Allergen: " + this.allergen + "\n";
		result += "Severity: " + this.severity + "\n";
		
		return result;
	}
	
}

