package personalHealthInfo;
import java.util.Date;

public class HealthIssue {
	private Date beganOn;
	private Date endedOn;
	private String symptomOrObservation;
	
	public HealthIssue(Date beganOn, Date endedOn, String symptomOrObservation)
	{
		this.beganOn = beganOn;
		this.endedOn = endedOn;
		this.symptomOrObservation = symptomOrObservation;
	}
	
	public Date GetBeganOn()
	{
		return this.beganOn;
	}
	
	public Date GetEndedOn()
	{
		return this.endedOn;
	}
	
	public String GetSymptomOrObservation()
	{
		return this.symptomOrObservation;
	}
	
	public void SetBeganOn(Date beganOn)
	{
		this.beganOn = beganOn;
	}
	
	public void SetEndOn(Date endedOn)
	{
		this.endedOn = endedOn;
	}
	
	public void SetSymptomOrObservation(String symptomOrObservation)
	{
		this.symptomOrObservation = symptomOrObservation;
	}
	
	public @Override String toString()
	{
		String result = "Health Issue\n";
		
		result += "Began on: " + this.beganOn.toString();
		result += "Ended on: " + this.endedOn.toString();
		result += "Symptom or observation: " + this.toString();
		
		return result;
	}
	
}
