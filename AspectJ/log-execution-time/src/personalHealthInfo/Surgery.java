package personalHealthInfo;
import java.util.Date;


public class Surgery {

	private Date date;
	private String condition;
	private String notes;
	
	public Surgery(Date date, String condition, String notes)
	{
		this.date = date;
		this.condition = condition;
		this.notes = notes;
	}
	
	public Surgery(Date date, String condition)
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
		String result = "Surgery   ";
		
		result += "Date: " + this.date + "\n";
		result += "Note: " + this.notes +"\n";
		result += "Condition: " + this.condition;
		
		return result;
	}

}
