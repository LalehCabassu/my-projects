package personalHealthInfo;
import java.util.Date;

public class Diagnosis {

	private Date date;
	private String condition;
	private String notes;
	
	public Diagnosis(Date date, String condition, String notes)
	{
		this.date = date;
		this.condition = condition;
		this.notes = notes;
	}
	
	public Diagnosis(Date date, String condition)
	{
		this.date = date;
		this.condition = condition;
		this.notes = "";
	}

	public Date GetDate()
	{
		return this.date;
	}
	
	public String GetCondition()
	{
		return this.condition;
	}
	
	public String GetNotes()
	{
		return this.notes;
	}
	
	public void SetDate(Date date)
	{
		this.date = date;
	}
	
	public void SetCondition(String condition)
	{
		this.condition = condition;
	}
	
	public void SetNotes(String notes)
	{
		this.notes = notes;
	}
	
	public @Override String toString()
	{
		String result = "Diagnosis\n";
		
		result += "Date: " + date.toString() + "\n";
		result += "Condotion: " + this.condition + "\n";
		result += "Notes: " + this.notes + "\n";
		
		return result;
	}
	
	public boolean MatchCondition(String condition)
	{
		boolean result = false;
		
		if(this.condition.equals(condition))
			result = true;
		return result;
	}
	
}
