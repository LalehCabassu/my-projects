package personalHealthInfo;

import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;

public class UserAccountList {

	private static List<UserAccount> userAccountList = new ArrayList<UserAccount>();
	
//	public UserAccountList()
//	{
//		this.userAccountList = new ArrayList<UserAccount>();		
//	}
	
	public static void add(UserAccount newUser)
	{
		userAccountList.add(newUser);
	}
	
	public static boolean remove(UserAccount user)
	{
		return userAccountList.remove(user);
	}
	
	public static int size()
	{
		return userAccountList.size();
	}
	
	public static UserAccount find(String username, String password)
	{
		UserAccount result = null;
		
		for(Iterator<UserAccount> l = userAccountList.iterator(); l.hasNext();)
		{
			UserAccount user = l.next();
			
			if(user.getUsername().equals(username) && user.getPassword().equals(password))
			{
				result = user;
				break;
			}
		}
		return result;
	}
	
	public static Iterator<UserAccount> getIterator()
	{
		return userAccountList.iterator();
	}
}
