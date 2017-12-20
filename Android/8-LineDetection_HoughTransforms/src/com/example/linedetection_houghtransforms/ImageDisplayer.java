package com.example.linedetection_houghtransforms;

import java.io.File;
import android.os.Bundle;
import android.os.Environment;
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
	
	static BitmapFactory.Options BITMAP_OPTIONS = new BitmapFactory.Options();
	static Bitmap.Config BITMAP_CONFIG = Bitmap.Config.ARGB_8888;

	static {
		 BITMAP_OPTIONS.inPreferredConfig = BITMAP_CONFIG;
	}

	protected Resources mRes = null;
	protected ImageView mImageView = null;
	protected Activity mActivity = null;
	static String InputAddress =  Environment.getExternalStorageDirectory().getPath();
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_image_displayer);
		
		Log.d(this.LOGCAT, "onCreate()");
		
		mRes = getResources();
		mImageView = (ImageView) this.findViewById(R.id.mImageView);
		mImageView.setImageDrawable(null);
		mActivity = this;
		
		// Display the selected image
		Intent i = this.getIntent();
		Log.d(this.LOGCAT, "Got an intent");

		if ( i.hasExtra(mRes.getString(R.string.activity_result_key)) ) {
			String filename = i.getStringExtra(mRes.getString(R.string.activity_result_key));
			Log.d(this.LOGCAT, "Got a filename");
			
			File imageFile = new File(Environment.getExternalStorageDirectory().getPath(), filename);
        	Bitmap bitmap = BitmapFactory.decodeFile(imageFile.getAbsolutePath());
        	
        	if(bitmap != null && imageFile != null){
				mImageView.setImageBitmap(bitmap);
				Log.d(this.LOGCAT, "Image displayed");
        	}
        	else
            	Log.d(this.LOGCAT, "Image displayer failed");
		}
		Log.d(this.LOGCAT, "onCreate() done");
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.image_displayer, menu);
		return true;
	}

}
