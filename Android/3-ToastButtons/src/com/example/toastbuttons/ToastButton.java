
// Laleh Rostami Hosoori  A01772483

package com.example.toastbuttons;

import com.example.toastbuttons.R;

import android.os.Bundle;
import android.app.Activity;
import android.util.Log;
import android.view.Menu;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

public class ToastButton extends Activity {
	
	final String LOGCAT = ToastButton.class.getSimpleName() + "LOGCAT";
	Button myLeftButton = null;
	Button myRightButton = null;
	EditText myEditTextBox = null; 
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		Log.d(this.LOGCAT, "onCreate()");
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_toast_button);
		
		myLeftButton = (Button) this.findViewById(R.id.myLeftButton);
		myRightButton = (Button) this.findViewById(R.id.myRightButton);
		
		myLeftButton.setOnClickListener(
	        		new OnClickListener() 
	        		{
						@Override
						public void onClick(View v) {
							Toast
								.makeText(getApplicationContext(), 
										R.string.myLeftToastingString, 
										Toast.LENGTH_LONG)
								.show();
							myEditTextBox.setText(R.string.myLeftEditTextString);
						}

	        		}
				);
	        		
		myRightButton.setOnClickListener(
        		new OnClickListener() {

					@Override
					public void onClick(View v) {
						Toast
							.makeText(getApplicationContext(), 
									R.string.myRightToastingString, 
									Toast.LENGTH_LONG)
							.show();
						myEditTextBox.setText(R.string.myRightEditTextString);
					}
        		}
			 );
		
		myEditTextBox = (EditText)findViewById(R.id.myEditText);
		
		Log.d(this.LOGCAT, "onCreate() done");
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.activity_toast_button, menu);
		return true;
	}
}
