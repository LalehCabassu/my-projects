package com.example.linedetection_houghtransforms;

import android.os.Bundle;
import android.app.Activity;
import android.content.Intent;
import android.content.res.Resources;
import android.util.Log;
import android.view.Menu;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.Toast;

public class AcrivitySelector extends Activity implements OnClickListener {
	
	final String LOGCAT = AcrivitySelector.class.getSimpleName() + "LOGCAT";
	
	final static int EDGE_DETECTOR_REQUEST_CODE  = 1;
	final static int HOUGH_TRANSFORM_REQUEST_CODE = 2;
	
	protected Resources mRes = null;
	protected Button mEdgeDetector = null;
	protected Button mHoughTransform = null;
	protected Activity mActivity = null;
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_acrivity_selector);
		
		Log.d(this.LOGCAT, "onCreate()");
		
		mRes = getResources();
		mEdgeDetector = (Button) this.findViewById(R.id.edgeDetectorButton);
		mHoughTransform = (Button) this.findViewById(R.id.houghTransformButton);
		mActivity = this;
		
		// Set intents
		mEdgeDetector.setOnClickListener(this);
		mHoughTransform.setOnClickListener(this);
						
		Log.d(this.LOGCAT, "onCreate() done");
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.acrivity_selector, menu);
		return true;
	}

	@Override
	public void onClick(View v) {
		
		Button clickedButton = (Button) v;
		switch ( clickedButton.getId() ) {
		case R.id.edgeDetectorButton:
			try { 
				Intent edgeDetectIntent = new Intent(this, EdgeDetectorAct.class);
				startActivityForResult(edgeDetectIntent, AcrivitySelector.EDGE_DETECTOR_REQUEST_CODE); 
				return;
			} catch ( Exception ex ) {
				Log.d(this.LOGCAT, "Unable to start EdgeDetectorAct: " + ex);
			}
			return;
 		case R.id.houghTransformButton:
			try { 
				Intent houghTransformsIntent = new Intent(this, HoughTransformsAct.class);
				startActivityForResult(houghTransformsIntent, AcrivitySelector.HOUGH_TRANSFORM_REQUEST_CODE); 
				return;
			} catch ( Exception ex ) {
				Log.d(this.LOGCAT, "Unable to start HoughTransformsAct: " + ex);
			}
			return;
		}
	}
	
	@Override
	public void onActivityResult(int requestCode, int resultCode, Intent returnedData) {
		super.onActivityResult(requestCode, resultCode, returnedData);
		
		if ( resultCode == Activity.RESULT_OK ) {
			if ( returnedData.hasExtra(mRes.getString(R.string.activity_result_key)) ) {
				String InputAddress = returnedData.getStringExtra(mRes.getString(R.string.activity_result_key));
				
				Intent imageDisplayerIntent = new Intent(this, ImageDisplayer.class);
				imageDisplayerIntent
							.putExtra(mRes.getString(R.string.activity_result_key), InputAddress);
				startActivity(imageDisplayerIntent); 		
			}
			else {
				switch ( requestCode ) {
				case AcrivitySelector.EDGE_DETECTOR_REQUEST_CODE:
					Toast.makeText(this, "Edge Detecor Failure", Toast.LENGTH_LONG).show();
					return;
				case AcrivitySelector.HOUGH_TRANSFORM_REQUEST_CODE:
					Toast.makeText(this, "Hough Transforms Failure", Toast.LENGTH_LONG).show();
					return;
				}
			}
		}
		else {
			switch ( requestCode ) {
			case AcrivitySelector.EDGE_DETECTOR_REQUEST_CODE:
				Toast.makeText(this, "Edge Detecor Failure", Toast.LENGTH_LONG).show();
				return;
			case AcrivitySelector.HOUGH_TRANSFORM_REQUEST_CODE:
				Toast.makeText(this, "Hough Transforms Failure", Toast.LENGTH_LONG).show();
				return;
			}
		}
	}
}
