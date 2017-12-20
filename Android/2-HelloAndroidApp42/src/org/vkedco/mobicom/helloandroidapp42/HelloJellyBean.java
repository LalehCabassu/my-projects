package org.vkedco.mobicom.helloandroidapp42;

import android.os.Bundle;
import android.app.Activity;
import android.view.Menu;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.Toast;

public class HelloJellyBean extends Activity {
	
	final String LOG_TAG = HelloJellyBean.class.getSimpleName() + "LOG_TAG";
	Button mFirstJellyBeanButton = null;
	

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_hello_android_app42__main);
        
        mFirstJellyBeanButton = (Button) this.findViewById(R.id.btnJellyBeanButton);
        
        mFirstJellyBeanButton.setOnClickListener(
        			new OnClickListener() {

						@Override
						public void onClick(View v) {
							Toast.makeText(getApplicationContext(), R.string.jelly_bean_toast_text, 
									Toast.LENGTH_LONG).show();
							
						}
        			}
        		);
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.activity_hello_android_app42__main, menu);
        return true;
    }
    
}
