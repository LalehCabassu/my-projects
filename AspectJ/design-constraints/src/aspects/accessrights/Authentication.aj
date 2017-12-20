package aspects.accessrights;

import personalHealthInfo.*;

public aspect Authentication {
	private static UserSession curSession;
	
	pointcut loginPC(): execution(UserSession UserSession.Login(..));
	pointcut logoutPC(): execution(UserSession UserSession.LogOut(..));
	
	Authentication()
	{
		curSession = null;
	}
	
	after() returning(UserSession session): loginPC()
	{
		curSession = session;
		System.out.println("> User '" + curSession + "' has logged in.");
	}
	
	after() returning(UserSession session): logoutPC()
	{
		System.out.println("> User '" + curSession + "' has logged out.\n\n");
		curSession = session;	
	}
	
	public static UserSession getCurrentSesstion()
	{
		return curSession;
	}
}
