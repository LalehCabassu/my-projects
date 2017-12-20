package aspects.accessrights;

import personalHealthInfo.UserAccount;
import personalHealthInfo.UserAccountList;

public class UserSession {

	private UserAccount currentUser = null;
	
	public static UserSession Login(String username, String password)
	{
		UserSession result = null;
		
		UserAccount temp = UserAccountList.find(username, password);
		
		if(temp != null)
		{
			result = new UserSession();
			result.currentUser = temp;
		}
		return result;
	}
	
	public static UserSession LogOut(UserSession session)
	{
		return (session = null);
	}
	
	public String getUsername()
	{
		return this.currentUser.getUsername();
	}
	
	public boolean canViewBirthDate()
	{
		return this.currentUser.canViewBirthDate;
	}
	
	public boolean canModifyBirthDate()
	{
		return this.currentUser.canModifyBirthDate;
	}
	
	public boolean canViewNames()
	{
		return this.currentUser.canViewNames;
	}
	
	public boolean canModifyNames()
	{
		return this.currentUser.canModifyNames;
	}
	
	public boolean canView()
	{
		return this.currentUser.canView;
	}
	
	public boolean canModify()
	{
		return this.currentUser.canModify;
	}
	
	@Override
	public String toString()
	{
		return (this.currentUser != null) ? this.currentUser.getUsername() : "";	
	}
}