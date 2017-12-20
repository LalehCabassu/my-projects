package edu.usu.cloud.wr.imageprocessing;

import java.awt.image.BufferedImage;
import java.util.ArrayList;
import java.util.List;

public class WordRecognition {
	
	public static List<String> recognizeWord(BufferedImage image, String name){
		
		// Find all word blobs in the image and store them on the blobStore in a folder with its name
		ArrayList<BufferedImage> blobs = FindWordBlobs.findBlobs(image, name);
				
		List<String> words = new ArrayList<String>();
		for(BufferedImage blob: blobs)
		{
		    // Down scale the image
        	BufferedImage resizedImage = TemplateLibrary.scaleDown(blob);
		    
		    // Do projection
		    Pixels curPix = new Pixels("blob", resizedImage);
		    int [] projection = Projection.verticalProjection(curPix);
		    
		    double ratio = resizedImage.getHeight() / (double) resizedImage.getWidth();
		    double roundedRatio = (double) Math.round(ratio * 10000) / 10000;
		   
		    TemplateLibrary tl = new TemplateLibrary();
		    
		    String w = tl.findWord(roundedRatio, projection);
		    if(w != "")
		    	words.add(w);
        }
        return words;
	}
}
