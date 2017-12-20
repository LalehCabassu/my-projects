package edu.usu.cloud.wr;

import java.io.IOException;

import javax.servlet.ServletConfig;
import javax.servlet.ServletContext;
import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;

/**
 * Servlet implementation class BaseServlet
 */
public class BaseServlet extends HttpServlet {
	private static final long serialVersionUID = 1L;
	private HttpSession session;
	protected UserSession userSession;    
	
	
    /**
     * @see HttpServlet#HttpServlet()
     */
    public BaseServlet() {
        super();
        userSession = UserSession.getUserSession();
        
    }
    
    /**
	 * @see HttpServlet#doGet(HttpServletRequest request, HttpServletResponse response)
	 */
	protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		getSession(request);
	}
	
	/**
	 * @see HttpServlet#doPost(HttpServletRequest request, HttpServletResponse response)
	 */
	protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		getSession(request);
	}
	
	protected void getSession(HttpServletRequest request)
    {
		session = request.getSession(true);
		userSession = (UserSession) session.getAttribute("UserSession");
    }

    protected void saveSession(HttpServletRequest request)
    {
    	session = request.getSession(true);
		session.setAttribute("UserSession", userSession);
    }

    protected void saveSessionAndForward(HttpServletRequest request, HttpServletResponse response)
    {
        saveSession(request);
    	try {
//			request.getRequestDispatcher(userSession.getReturnURL()).forward(request, response);
			response.sendRedirect(userSession.getReturnURL());
//		} catch (ServletException e) {
//			// TODO Auto-generated catch block
//			e.printStackTrace();
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
    }

    protected void pushUrlAndRedirect(HttpServletResponse response, String returnUrl, String newUrl)
    {
        try {
        	userSession.setReturnURL(returnUrl);
            response.sendRedirect(newUrl);
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
    }
	
}
