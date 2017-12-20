package personalHealthInfo;

import aspects.protect.Protected;

public class UserAccount {

	private static int sequenceKeyId = 1;
	
	private int id;
	private String username;
	private String password;
	private String firstName;
	private String lastName;
	
	public boolean canViewBirthDate;
	public boolean canModifyBirthDate;
	public boolean canViewNames;
	public boolean canModifyNames;
	
	// general access rights
	public boolean canView;
	public boolean canModify;
	
	public UserAccount(String username, String password, String firstName, String lastName,
			boolean canViewBirthDate, boolean canModifyBirthDate,
			boolean canViewNames, boolean canModifyNames,
			boolean canView, boolean canModify)
	{
		this.id = this.sequenceKeyId++;
		this.username = username;
		this.password = password;
		this.firstName = firstName;
		this.lastName = lastName;
		
		this.canViewBirthDate = canViewBirthDate;
		this.canModifyBirthDate = canModifyBirthDate;
		this.canViewNames = canViewNames;
		this.canModifyNames = canModifyNames;
		
		this.canView = canView;
		this.canModify = canModify;
	}
	
	public UserAccount(String username, String password, String firstName, String lastName,
						boolean canViewBirthDate, boolean canModifyBirthDate,
						boolean canViewNames, boolean canModifyNames)
	{
		this.id = this.sequenceKeyId++;
		this.username = username;
		this.password = password;
		this.firstName = firstName;
		this.lastName = lastName;
		
		this.canViewBirthDate = canViewBirthDate;
		this.canModifyBirthDate = canModifyBirthDate;
		this.canViewNames = canViewNames;
		this.canModifyNames = canModifyNames;
	}
	
	public UserAccount(String username, String password, String firstName, String lastName)
	{
		this.id = this.sequenceKeyId++;
		this.username = username;
		this.password = password;
		this.firstName = firstName;
		this.lastName = lastName;
	}

	public UserAccount(String username, String password)
	{
		this.id = this.sequenceKeyId++;
		this.username = username;
		this.password = password;
	}
	
	public int getId() 
	{
		return this.id;
	}
	
	public String getUsername()
	{
		return this.username;
	}

	public String getPassword()
	{
		return this.password;
	}

	public String getFirstName()
	{
		return this.firstName;
	}
	
	public String getLastName()
	{
		return this.lastName;
	}
	
	public void setPassword(String password)
	{
		if(password.isEmpty())
			this.password = password;
	}
	
	public void setFirstName(String firstName)
	{
		this.firstName = firstName;
	}
	
	public void setLastName(String lastName)
	{
		this.lastName = lastName;
	}
}