package edu.usu.cloud.wr;

import java.io.File;
import java.io.IOException;

import javax.servlet.ServletContext;
import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

/**
 * Servlet implementation class InitializeBlobStoreServlet
 */
@WebServlet(urlPatterns="/images")
public class InitializeBlobStoreServlet extends BaseServlet {
	private static final long serialVersionUID = 1L;
       
    /**
     * @see HttpServlet#HttpServlet()
     */
    public InitializeBlobStoreServlet() {
        super();
        userSession.setReturnURL("home.jsp");
    }

	/**
	 * @see HttpServlet#doGet(HttpServletRequest request, HttpServletResponse response)
	 */
	protected void doGet(HttpServletRequest request, HttpServletResponse response) 
								throws ServletException, IOException {
		
		ServletContext servletContext = request.getServletContext();
		String servletPath = servletContext.getRealPath(File.separator);
		String resourcePath = servletPath + "/WEB-INF/resources";
		
		userSession.getImageHandler().initialImages(resourcePath);
		
		saveSessionAndForward(request, response);
	}
}
