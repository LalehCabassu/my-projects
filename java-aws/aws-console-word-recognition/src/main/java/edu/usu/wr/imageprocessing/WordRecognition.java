package edu.usu.wr.imageprocessing;

import java.awt.image.BufferedImage;
import java.io.FileWriter;
import java.io.IOException;
import java.util.List;

import edu.usu.wr.handlers.BlobStoreHandler;

public class WordRecognition {
	
//	final String LOGCAT = FindWordBlobs.class.getSimpleName() + "LOGCAT";
//	
//	private static String OutputAddress = Environment.getExternalStorageDirectory().getPath() + "/output_word_blobs/";
//	
//	protected static BitmapFactory.Options BITMAP_OPTIONS = new BitmapFactory.Options();
//	protected static Bitmap.Config BITMAP_CONFIG = Bitmap.Config.ARGB_8888;
//	static {
//		 BITMAP_OPTIONS.inPreferredConfig = BITMAP_CONFIG;
//	}
	
	public String recognizeWord(BufferedImage image, String name){
		
		// Find all word blobs in the image and store them on the blobStore in a folder with its name
		FindWordBlobs.findBlobs(image, name);
		
//		File inputFolder = new File(path + name + "/");
//        inputFolder.mkdirs();
//        
         
//        for(File imageFile: ImageReaderWriter.readImageFiles(path + name + "/")) {
//        	BufferedImage blob = Converter.imageFileToBufferedImage(imageFile);
		
		// Download all blobs of the image
		String words = "";
		BlobStoreHandler handler = new BlobStoreHandler();
		List<BufferedImage> blobs = handler.downloadWordBlobs(name);
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
		    	words += w + "\t";
        }
       // if( words != "" )
        	words += "\n";
        return words;
        //writeWords(words, name);
	}
	
	// Write a text file to SD card
	public void writeWords(String path, String text, String name) throws IOException {
	    
	    try {
	    	FileWriter file = new FileWriter(path + name + "/" + name + "_words.txt");
	    	//Log.d(this.LOGCAT, "* " + text);
	    	file.write(text);
	        file.flush();
	        file.close();

	    } catch (Exception e) {
//	    	Log.d(this.LOGCAT, "exception: " + e);
	           e.printStackTrace();
	    }
	}
}
