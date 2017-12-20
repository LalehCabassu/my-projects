package com.example.wordrcognition;

import java.io.File;
import java.io.FileWriter;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Collections;
import java.util.Enumeration;

import android.os.Bundle;
import android.os.Environment;
import android.app.Activity;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.util.Log;
import android.view.Menu;

public class WordRecongnitionAct extends Activity {

	final String LOGCAT = WordRecongnitionAct.class.getSimpleName() + "LOGCAT";
	private static String InputAddress = Environment.getExternalStorageDirectory().getPath() + "/input_images/";
	private static String OutputAddress = Environment.getExternalStorageDirectory().getPath() + "/recognized_words/";
	
	protected static BitmapFactory.Options BITMAP_OPTIONS = new BitmapFactory.Options();
	protected static Bitmap.Config BITMAP_CONFIG = Bitmap.Config.ARGB_8888;
	static {
		 BITMAP_OPTIONS.inPreferredConfig = BITMAP_CONFIG;
	}
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_word_recongnition);
		
		//FindWordBlobs wr = new FindWordBlobs();
		//wr.findBlobs();
		
		/*
		try {
			TemplateLibrary.createTextFile();
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		*/
		TemplateLibrary.createLibrary();
		
		WordRecognition wr = new WordRecognition();
		
		File inputFolder = new File(InputAddress);
        inputFolder.mkdirs();
        String results = "";
        
        for(File imageFile: ImageReader.readImages(InputAddress)) {
        	Bitmap NTF_line = ImageReader.convertImageToBitmap(imageFile, BITMAP_OPTIONS);
        	String file_name = imageFile.getName();
			file_name = file_name.substring(0, file_name.indexOf("."));
			results += wr.recognizeWord(NTF_line, file_name);
		}
        
        try {
			writeWords(results);
			
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
	}
	
	private void writeWords(String text) throws IOException {
	    
		File inputFolder = new File(OutputAddress);
        inputFolder.mkdirs();
        
	    try {
	    	FileWriter file = new FileWriter(OutputAddress + "results.txt");
	    	Log.d(this.LOGCAT, "* " + text);
	    	file.write(text);
	        file.flush();
	        file.close();

	    } catch (Exception e) {
	    	Log.d(this.LOGCAT, "exception: " + e);
	           e.printStackTrace();
	    }
	}	

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.word_recongnition, menu);
		return true;
	}

}
