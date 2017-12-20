package com.example.datatransferintents;


import android.os.Bundle;
import android.app.Activity;
import android.content.Intent;
import android.content.res.Resources;
import android.view.Menu;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

public class DataTransferIntents extends Activity {
	
	protected TextView myDesc = null;
	protected EditText myImageNum = null;
	protected Button myBinarizer = null;
	protected Button myEdgeDetector = null;
	protected Activity myActivity= null;
	protected Resources myResources = null;
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_data_transfer_intents);
	
		myDesc = (TextView) this.findViewById(R.id.myDescTextView);
		myImageNum = (EditText) this.findViewById(R.id.myImageNumEditText);
		myBinarizer = (Button) this.findViewById(R.id.myBinarizeButton);
		myEdgeDetector = (Button) this.findViewById(R.id.myDetectEdgeButton);
		myActivity = this;
		myResources = getResources();
		
		myDesc.setText(R.string.Description);
		
		myBinarizer.setOnClickListener(
        		new OnClickListener() {

					@Override
					public void onClick(View v) {
						Intent myIntent = new Intent(myActivity, ImageProcessor.class);
						int imageNum = Integer.parseInt(myImageNum.getText().toString()) ;
						myIntent.putExtra(myResources.getString(R.string.image_id), imageNum);
						myIntent.putExtra(myResources.getString(R.string.action_id), myResources.getString(R.string.action_binarize));
						startActivity(myIntent);
					}	
        		}
        );
		
		myEdgeDetector.setOnClickListener(
        		new OnClickListener() {

					@Override
					public void onClick(View v) {
						Intent myIntent = new Intent(myActivity, ImageProcessor.class);
						int imageNum = Integer.parseInt(myImageNum.getText().toString()) ;
						myIntent.putExtra(myResources.getString(R.string.image_id), imageNum);
						myIntent.putExtra(myResources.getString(R.string.action_id), myResources.getString(R.string.action_detect_edges));
						startActivity(myIntent);
					}	
        		}
        );

	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.activity_data_transfer_intents, menu);
		return true;
	}
}
