package com.example.wordrcognition;

import java.io.File;
import java.io.FileWriter;
import java.io.IOException;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.Environment;
import android.util.Log;

public class WordRecognition {
	
	final String LOGCAT = FindWordBlobs.class.getSimpleName() + "LOGCAT";
	
	private static String OutputAddress = Environment.getExternalStorageDirectory().getPath() + "/output_word_blobs/";
	
	protected static BitmapFactory.Options BITMAP_OPTIONS = new BitmapFactory.Options();
	protected static Bitmap.Config BITMAP_CONFIG = Bitmap.Config.ARGB_8888;
	static {
		 BITMAP_OPTIONS.inPreferredConfig = BITMAP_CONFIG;
	}
	
	public String recognizeWord(Bitmap image, String name){
		
		// Find all word blobs in the image and store them on the SD card in a folder with its name
		FindWordBlobs.findBlobs(image, name);
		
		// Read all blobs
		File inputFolder = new File(OutputAddress + name + "/");
        inputFolder.mkdirs();
        
        String words = name + ":\t"; 
        for(File imageFile: ImageReader.readImages(OutputAddress + name + "/")) {
        	Bitmap blob = ImageReader.convertImageToBitmap(imageFile, BITMAP_OPTIONS);
		    
		    // Down scale the image
		    Bitmap resizedImage = TemplateLibrary.scaleDown(blob);
		    
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
	private void writeWords(String text, String name) throws IOException {
	    
	    try {
	    	FileWriter file = new FileWriter(OutputAddress + name + "/" + name + "_words.txt");
	    	//Log.d(this.LOGCAT, "* " + text);
	    	file.write(text);
	        file.flush();
	        file.close();

	    } catch (Exception e) {
	    	Log.d(this.LOGCAT, "exception: " + e);
	           e.printStackTrace();
	    }
	}
}
