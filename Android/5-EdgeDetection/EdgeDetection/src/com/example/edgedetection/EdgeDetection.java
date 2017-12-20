package com.example.edgedetection;

import java.io.File;

import com.example.edgedetection.ImageReader;
import com.example.edgedetection.Pixels;
//import com.example.edgedetection.NiblackBinarizer;

import android.os.Bundle;
import android.os.Environment;
import android.app.Activity;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.view.Menu;

public class EdgeDetection extends Activity {

	static String InputAddressGray  = Environment.getExternalStorageDirectory().getPath() + "/input_gray_images/";
	static String InputAddressColor  = Environment.getExternalStorageDirectory().getPath() + "/input_color_images/";
	static String OutputAddress = Environment.getExternalStorageDirectory().getPath() + "/output_images/";
	
	//static int NIBLACK_BLOCK_SIZE = 50;
	//static double NIBLACK_STD_THRESHOLD = 12.7;
	//static double NIBLACK_CONSTANT = -0.25; 
	
	static BitmapFactory.Options BITMAP_OPTIONS = new BitmapFactory.Options();
	static Bitmap.Config BITMAP_CONFIG = Bitmap.Config.ARGB_8888;

	static {
		 BITMAP_OPTIONS.inPreferredConfig = BITMAP_CONFIG;
	}
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_edge_detection);
		
		File inputFolderGray = new File(InputAddressGray);
        inputFolderGray.mkdirs();
        File inputFolderColor = new File(InputAddressColor);
        inputFolderColor.mkdirs();
        File outputFolder = new File(OutputAddress);
        outputFolder.mkdirs();
		
		Bitmap bm = null;
		Pixels bmPixIn = null;
		Pixels bmPixOut = null;

		for(File imageFile: ImageReader.readImages(InputAddressGray)) {
		    bm = ImageReader.convertImageToBitmap(imageFile, BITMAP_OPTIONS);
			bmPixIn = new Pixels(imageFile.toString(), bm);
			bmPixOut = new Pixels(imageFile.toString(), bm);
			
			DetectRGBEdges.magnitudeThreshold = 5;
			DetectRGBEdges.thetaThreshold = 180;
			DetectRGBEdges.detect(bmPixIn, bmPixOut);
			
			String file_name = imageFile.getName();
			file_name = file_name.substring(0, file_name.indexOf("."));
			bmPixOut.writePixelsToFile(OutputAddress, file_name + "EdgeDetected");
			
			bm.recycle();
			bm = null;
			bmPixIn = null;
			bmPixOut = null;
		}	

		for(File imageFile: ImageReader.readImages(InputAddressColor)) {
		    bm = ImageReader.convertImageToBitmap(imageFile, BITMAP_OPTIONS);
			bmPixIn = new Pixels(imageFile.toString(), bm);
			bmPixOut = new Pixels(imageFile.toString(), bm);
			
			//NiblackBinarizer.binarizeImage(bmPixIn, NIBLACK_BLOCK_SIZE, NIBLACK_STD_THRESHOLD, NIBLACK_CONSTANT);
			DetectRGBEdges.magnitudeThreshold = 3;
			DetectRGBEdges.thetaThreshold = 180;
			DetectRGBEdges.detect(bmPixIn, bmPixOut);
			
			String file_name = imageFile.getName();
			file_name = file_name.substring(0, file_name.indexOf("."));
			bmPixOut.writePixelsToFile(OutputAddress, file_name + "EdgeDetected");
			
			bm.recycle();
			bm = null;
			bmPixIn = null;
			bmPixOut = null;
		}	

	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.activity_edge_detection, menu);
		return true;
	}

}
