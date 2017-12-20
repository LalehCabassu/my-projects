package com.example.alignednutritiontablelocalization;

import android.os.Bundle;
import android.app.Activity;
import android.content.Intent;
import android.content.res.Resources;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.util.Log;
import android.view.Menu;
import android.widget.ImageView;

public class ImageDisplayer extends Activity {

	final String LOGCAT = ImageDisplayer.class.getSimpleName() + "LOGCAT";
	static Resources mRes = null;
	static ImageView mImageView = null;
	
	static BitmapFactory.Options BITMAP_OPTIONS = new BitmapFactory.Options();
	static Bitmap.Config BITMAP_CONFIG = Bitmap.Config.ARGB_8888;

	static int NIBLACK_BLOCK_SIZE = 50;
	static double NIBLACK_STD_THRESHOLD = 12.7;
	static double NIBLACK_CONSTANT = -0.25; 
	 
	static {
		 BITMAP_OPTIONS.inPreferredConfig = BITMAP_CONFIG;
	}
	
	static int LINE_DETECTION_BLACK_THRESHOLD = 15;
	static int LINE_DETECTION_LENGTH_THRESHOLD = 4;
	

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		Log.d(this.LOGCAT, "onCreate()");
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_image_displayer);
		
		mRes = getResources();
		mImageView = (ImageView) this.findViewById(R.id.mImageView);
		mImageView.setImageDrawable(null);
		
		Bitmap bm = null;
		Pixels bmPixIn = null;
		Pixels bmPixOut = null; 
		
		Intent i = this.getIntent();
		
		Log.d(this.LOGCAT, "Got the intent");
		
		if ( i.hasExtra(mRes.getString(R.string.imageKey)) ) {
			
			int ik = i.getIntExtra(mRes.getString(R.string.imageKey), 0);
			
			Log.d(this.LOGCAT, "Got the image key: " + ik);
			// 
			switch (ik) {
			case 0: 
				bm = BitmapFactory.decodeResource(getResources(),R.drawable.image01);
				bmPixIn = new Pixels("image01", bm);
				bmPixOut = new Pixels("image01", bm);
				break;
			case 1:
				bm = BitmapFactory.decodeResource(getResources(),R.drawable.image02);
				bmPixIn = new Pixels("image02", bm);
				bmPixOut = new Pixels("image02", bm);
				break;
			case 2:
				bm = BitmapFactory.decodeResource(getResources(),R.drawable.image03);
				bmPixIn = new Pixels("image03", bm);
				bmPixOut = new Pixels("image03", bm);
				break;
			case 3:
				bm = BitmapFactory.decodeResource(getResources(),R.drawable.image04);
				bmPixIn = new Pixels("image04", bm);
				bmPixOut = new Pixels("image04", bm);
				break;
			case 4:
				bm = BitmapFactory.decodeResource(getResources(),R.drawable.image05);
				bmPixIn = new Pixels("image05", bm);
				bmPixOut = new Pixels("image05", bm);
				break;
			}
			
			Log.d(this.LOGCAT, "Decode to Bitmap");
									
			LineDetection.magnitudeThreshold = 3;
			LineDetection.thetaThreshold = 15;
			LineDetection.detectHorizontalLines(bmPixIn, bmPixOut);
			Log.d(this.LOGCAT, "Detect horizontal lines");
			
			Projection myProjection = new Projection(bmPixOut);
			myProjection.findVerticalBoundries();
			int xstart = myProjection.getXstart();
			int xend = myProjection.getXend();
			Log.d(this.LOGCAT, "Vertical Boundaries");
			
			LineDetection.magnitudeThreshold = 15;
			LineDetection.thetaThreshold = 15;
			LineDetection.detectVerticalLines(bmPixIn, bmPixOut);
			Log.d(this.LOGCAT, "Detect vertical lines");
			
			myProjection.findHorizontalBoundries();
			int ystart = myProjection.getYstart();
			int yend = myProjection.getYend();
			Log.d(this.LOGCAT, "Horizontal Boundaries");
			
			Bitmap cropped_bm = null;

			cropped_bm = Bitmap.createBitmap(bm, xstart, ystart, (xend - xstart), (yend - ystart));
			mImageView.setImageBitmap(cropped_bm);
		}
		Log.d(this.LOGCAT, "onCreate() done");
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.activity_image_displayer, menu);
		return true;
	}
}
