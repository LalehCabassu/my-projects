package com.example.datatransferintents;


import android.os.Bundle;
import android.app.Activity;
import android.content.Intent;
import android.content.res.Resources;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.view.Menu;
import android.widget.ImageView;
import android.widget.TextView;

public class ImageProcessor extends Activity {
	
	protected ImageView myImageView = null;
	protected Resources myResources = null;
	protected TextView myTextView = null;
	
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
		setContentView(R.layout.activity_image_processor);
		
		myImageView = (ImageView) this.findViewById(R.id.myImageView);
		myImageView.setImageDrawable(null);
		myResources = getResources();
		
		Intent i = this.getIntent();
		int imageNum = i.getIntExtra(myResources.getString(R.string.image_id), 0);
		
		if (imageNum > 0) {
			boolean action = false;       //binarize: false     detect edges: true
			String actionName = i.getStringExtra(myResources.getString(R.string.action_id)); 
			if(actionName.equals(R.string.action_binarize))
				action = false;
			else if(actionName.equals(R.string.action_detect_edges))
				action = true;
			
			Bitmap bm = null;
			Pixels bmPix = null;
			
			switch (imageNum) {
			case 1:
				bm = BitmapFactory.decodeResource(getResources(),R.drawable.image01);
				bmPix = new Pixels("image01", bm);
				break;
			case 2:
				bm = BitmapFactory.decodeResource(getResources(),R.drawable.image02);
				bmPix = new Pixels("image02", bm);
				break;
			case 3:
				bm = BitmapFactory.decodeResource(getResources(),R.drawable.image03);
				bmPix = new Pixels("image03", bm);
				break;
			case 4:
				bm = BitmapFactory.decodeResource(getResources(),R.drawable.image04);
				bmPix = new Pixels("image04", bm);
				break;
			case 5:
				bm = BitmapFactory.decodeResource(getResources(),R.drawable.image05);
				bmPix = new Pixels("image05", bm);
				break;
			case 6:
				bm = BitmapFactory.decodeResource(getResources(),R.drawable.image06);
				bmPix = new Pixels("image06", bm);
				break;
			case 7:
				bm = BitmapFactory.decodeResource(getResources(),R.drawable.image07);
				bmPix = new Pixels("image07", bm);
				break;
			case 8:
				bm = BitmapFactory.decodeResource(getResources(),R.drawable.image08);
				bmPix = new Pixels("image08", bm);
				break;
			case 9:
				bm = BitmapFactory.decodeResource(getResources(),R.drawable.image09);
				bmPix = new Pixels("image09", bm);
				break;
			case 10:
				bm = BitmapFactory.decodeResource(getResources(),R.drawable.image10);
				bmPix = new Pixels("image10", bm);
				break;
			case 11:
				bm = BitmapFactory.decodeResource(getResources(),R.drawable.image11);
				bmPix = new Pixels("image11", bm);
				break;
			case 12:
				bm = BitmapFactory.decodeResource(getResources(),R.drawable.image12);
				bmPix = new Pixels("image12", bm);
				break;
			}
			
			if(action)
				NiblackBinarizer.binarizeImage(bmPix, NIBLACK_BLOCK_SIZE, NIBLACK_STD_THRESHOLD, NIBLACK_CONSTANT);
			else
			{
				Pixels bmPixIn = new Pixels("image", bm);
				DetectRGBEdges.magnitudeThreshold = 3;
				DetectRGBEdges.thetaThreshold = 180;
				DetectRGBEdges.detect(bmPixIn, bmPix);
			}
			
			bm = bmPix.convertToBitmap();
			myImageView.setImageBitmap(bm);			
		}
	}

	@Override
	
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.activity_image_processor, menu);
		return true;
	}
	
}