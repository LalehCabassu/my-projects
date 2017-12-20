package com.example.prebuilttesseract_test;

import com.googlecode.tesseract.android.TessBaseAPI;

import android.os.Bundle;
import android.os.Environment;
import android.app.Activity;
import android.graphics.Bitmap;
import android.graphics.Bitmap.Config;
import android.graphics.BitmapFactory;
import android.graphics.Canvas;
import android.graphics.Paint;
import android.util.Log;
import android.view.Menu;
import android.widget.Toast;

public class PrebuiltTesseract_Test extends Activity {
	
	final String LOGCAT = PrebuiltTesseract_Test.class.getSimpleName() + "LOGCAT";
	
	protected String tessartDirectory = Environment.getExternalStorageDirectory().getPath() + "/tessaract_languages/";
	protected TessBaseAPI baseApi;
	protected BitmapFactory.Options opts;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_prebuilt_tesseract__test);
		
		// steps to invoke tesseract from android application 
        baseApi = new TessBaseAPI();
        // change the first argument to the place of the tesseract directory on your SDCARD
        baseApi.init(tessartDirectory, "eng");
        
        // Image has to be ARGB_8888 or the next api call will fail
        opts = new BitmapFactory.Options();
        opts.inPreferredConfig = Bitmap.Config.ARGB_8888;
        
        for(int i = 0; i < 3; i++){
        	switch(i){
        	case 0:
        		callOCR(R.drawable.ear);
        		break;
        	case 1:
        		callOCR(R.drawable.shifting);
        		break;
        	case 2:
        		callOCR(R.drawable.image23);
        		break;
        	}
        	Toast.makeText(this, baseApi.getUTF8Text(), Toast.LENGTH_LONG).show();
        }
        
        Log.d(this.LOGCAT, "onCreate() done");
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.prebuilt_tesseract__test, menu);
		return true;
	}
	
	protected void callOCR(int imageId){
		
        // Image has to be ARGB_8888 or the next api call will fail
        Bitmap bitmapImage = BitmapFactory.decodeResource(this.getResources(), imageId);
        Log.d(this.LOGCAT, "1. Image config: " + bitmapImage.getConfig());
        
        if(bitmapImage.getConfig() != Bitmap.Config.ARGB_8888){
        	Bitmap maskBitmap = Bitmap.createBitmap(bitmapImage.getWidth(), bitmapImage.getHeight(), Bitmap.Config.ARGB_8888);
        	Canvas c = new Canvas();
        	c.setBitmap(maskBitmap);
        	Paint p = new Paint();
        	p.setFilterBitmap(true); 
        	c.drawBitmap(bitmapImage,0,0,p);
        	bitmapImage.recycle();
        	bitmapImage = maskBitmap;
        	Log.d(this.LOGCAT, "2. Image config: " + bitmapImage.getConfig());
        }
        
        baseApi.setImage(bitmapImage);
        Log.d(this.LOGCAT, "Text: " + baseApi.getUTF8Text());
	}
	

}
