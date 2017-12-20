package com.example.imagebinarization;

import java.io.File;

import android.os.Bundle;
import android.os.Environment;
import android.app.Activity;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.view.Menu;

public class ImageBinarization extends Activity {

	static String InputAddress  = Environment.getExternalStorageDirectory().getPath() + "/ImageBinarization/Input/";
	static String OutputAddress = Environment.getExternalStorageDirectory().getPath() + "/ImageBinarization/Output/";
	
	static BitmapFactory.Options BITMAP_OPTIONS = new BitmapFactory.Options();
	static Bitmap.Config BITMAP_CONFIG = Bitmap.Config.ARGB_8888;

	static int NIBLACK_BLOCK_SIZE = 50;
	static double NIBLACK_STD_THRESHOLD = 12.7;
	static double NIBLACK_CONSTANT = -0.25; 
	 
	static {
		 BITMAP_OPTIONS.inPreferredConfig = BITMAP_CONFIG;
	}
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_image_binarization);
		
		File inputFolder = new File(InputAddress);
        inputFolder.mkdirs();
        File outputFolder = new File(OutputAddress);
        outputFolder.mkdirs();
		
		Bitmap bm = null;
		Pixels bmPix = null;
		
		for(File imageFile: ImageReader.readImages(InputAddress)) {
		    bm = ImageReader.convertImageToBitmap(imageFile, BITMAP_OPTIONS);
			bmPix = new Pixels(imageFile.toString(), bm);
			
			NiblackBinarizer.binarizeImage(bmPix, NIBLACK_BLOCK_SIZE, NIBLACK_STD_THRESHOLD, NIBLACK_CONSTANT);
			String file_name = imageFile.getName();
			file_name = file_name.substring(0, file_name.indexOf("."));
			bmPix.writePixelsToFile(OutputAddress, file_name + "Binarized");
			
			bm.recycle();
			bm = null;
			bmPix = null;
		}	
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.activity_image_binarization, menu);
		return true;
	}
}
