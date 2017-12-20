package edu.usu.cloud.wr;

import java.awt.image.BufferedImage;
import java.io.IOException;
import java.util.List;

import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import edu.usu.cloud.wr.imageprocessing.TemplateLibrary;
import edu.usu.cloud.wr.imageprocessing.WordRecognition;
import edu.usu.cloud.wr.model.Image;

/**
 * Servlet implementation class RecogniseWordServlet
 */
@WebServlet(urlPatterns="/rw")
public class RecognizeWordServlet extends BaseServlet {
	private static final long serialVersionUID = 1L;
       
    /**
     * @see HttpServlet#HttpServlet()
     */
    public RecognizeWordServlet() {
        super();
    }

	/**
	 * @see HttpServlet#doPost(HttpServletRequest request, HttpServletResponse response)
	 */
	protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		String indexStr = request.getParameter("index");
		
		if(!indexStr.isEmpty())
		{
			int imageIndex = Integer.parseInt(indexStr); 
			
			ImageHandler imageHandler = userSession.getImageHandler();
			List<String> library = imageHandler.downloadTemplateLibrary();
			TemplateLibrary.loadLibrary(library);
			Image image = imageHandler.getImage(imageIndex);
			BufferedImage bufferedImage = imageHandler.downloadImage(imageIndex);
			List<String> recognisedWords = WordRecognition.recognizeWord(bufferedImage, image.getName());
			image.setRecognizedWords(recognisedWords);
		}
		
		userSession.setReturnURL("home.jsp");
		saveSessionAndForward(request, response);		
	}

}
