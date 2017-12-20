package personalHealthInfo;
import java.util.Date;


public class Perscription {

	private Date startDate;
	private Date endDate;
	private String medication;
	private String dosage;
	private String frequency;
	
	public Perscription(Date startDate, Date endDate, 
			String medication, String dosage, String frequency)
	{
		this.startDate = startDate;
		this.endDate = endDate;
		this.medication = medication;
		this.dosage = dosage;
		this.frequency = frequency;
	}
	
	public Date GetStartDate()
	{
		return this.startDate;
	}
	
	public Date GetEndDate()
	{
		return this.endDate;
	}
	
	public String GetMedication()
	{
		return this.medication;
	}
	
	public String GetDosage()
	{
		return this.dosage;
	}
	
	public String GetFrequency()
	{
		return this.frequency;
	}
	
	public void SetStartDate(Date startDate)
	{
		this.startDate = startDate;
	}
	
	public void SetEndDate(Date endDate)
	{
		this.endDate = endDate;
	}
	
	public void SetMedicatoin(String medication)
	{
		this.medication = medication;
	}
	
	public void SetDosage(String dosage)
	{
		this.dosage = dosage;
	}
	
	public void SetFrequency(String frequency)
	{
		this.frequency = frequency;
	}
	
	public @Override String toString()
	{
		String result = "Perscription\n";
		
		result += "Start date: " + this.startDate.toString() + "   ";
		result += "End date: " + this.endDate.toString() + "\n";
		result += "Medication: " + this.medication + "\n";
		result += "Dosage: " + this.dosage + "   ";
		result += "Frequently: " + this.frequency;
		
		return result;
	}
	
}
