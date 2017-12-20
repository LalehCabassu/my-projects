package org.vkedco.mobappdev.helloandroidapp;

/*
 * ************************************************
 * HelloAndroidAct.java is the Activity of an Android
 * App that displays a Toast in response to a Button
 * click.
 * 
 * Bugs to vladimir dot kulyukin at gmail dot com
 * ************************************************
 */

import android.app.Activity;
import android.os.Bundle;
import android.util.Log;
import android.view.Menu;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.Toast;

public class HelloGingerbreadAct extends Activity {
	
	final String LOGTAG = HelloGingerbreadAct.class.getSimpleName() + "LOGTAG";
	Button mFirstButton = null;
	

    @Override
    public void onCreate(Bundle savedInstanceState) {
    	Log.d(this.LOGTAG, "onCreate()");
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_hello_android);
        
        mFirstButton = (Button) this.findViewById(R.id.my_first_button);
        
        mFirstButton.setOnClickListener(
        		new OnClickListener() {

					@Override
					public void onClick(View v) {
						Toast
							.makeText(getApplicationContext(), 
									R.string.my_first_toast_text, 
									Toast.LENGTH_LONG)
							.show();
						
					}
        			
        		}
        		
        );
        
        Log.d(this.LOGTAG, "onCreate() done");
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        getMenuInflater().inflate(R.menu.activity_hello_android, menu);
        return true;
    }
}
