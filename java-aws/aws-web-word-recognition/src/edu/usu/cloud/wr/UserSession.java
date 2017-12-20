package edu.usu.cloud.wr;

import java.util.Stack;

public class UserSession {
	
	private static UserSession instance = null;
	private Stack returnURLStack;
	private String returnURL;
	
	private ImageHandler imageHandler;
	
	public static UserSession getUserSession()
	{
		if(instance == null)
			instance = new UserSession();
		return instance;
	}
	
	private UserSession()
	{
		returnURLStack = new Stack();
		imageHandler = new ImageHandler();
	}
	
	public ImageHandler getImageHandler() {
		return imageHandler;
	}

	public String getReturnURL()
    {
	    String result = "";
        if (returnURLStack.size() > 0)
        {
            result = (String) returnURLStack.pop();
        }
        return result;
    }
	
	public void setReturnURL(String value)
	{
         returnURLStack.push(value);
    }

    public void clearReturnURLStack()
    {
        returnURLStack.clear();
    }
}
