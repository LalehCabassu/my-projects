package com.example.alignednutritiontablelocalization;

import android.os.Bundle;
import android.app.ListActivity;
import android.content.Intent;
import android.content.res.Resources;
import android.graphics.Color;
import android.util.Log;
import android.view.Menu;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.ListView;
import android.widget.AdapterView.OnItemClickListener;

public class AlignedNutritionTableLocalization extends ListActivity
			implements OnItemClickListener {

	Resources mRes = null;
	String[] mImageNumbers = null;
	final String LOGCAT = AlignedNutritionTableLocalization.class.getSimpleName() + "LOGCAT";
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		Log.d(this.LOGCAT, "onCreate()");
		super.onCreate(savedInstanceState);
		//setContentView(R.layout.activity_aligned_nutrition_table_localization);
		
		mRes = getResources();
        mImageNumbers = mRes.getStringArray(R.array.imageNumbers);
        
        Log.d(this.LOGCAT, "ListView: Try to create");
        ListView lv = this.getListView();
        lv.setBackgroundColor(Color.WHITE);
        
        Log.d(this.LOGCAT, "ListView: Created");
		
        ArrayAdapter<String> mAdaptor = 
        	new ArrayAdapter<String>(this, 
    			android.R.layout.simple_list_item_1, 
    			this.mImageNumbers);
        
        Log.d(this.LOGCAT, "ArrayAdapter: Created");
        
        lv.setAdapter(mAdaptor);
        
        Log.d(this.LOGCAT, "ArrayAdater: Bound to ListView");
		
        lv.setTextFilterEnabled(true);
        lv.setOnItemClickListener(this);	
        
        Log.d(this.LOGCAT, "onCreate() done");
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(
				R.menu.activity_aligned_nutrition_table_localization, menu);
		return true;
	}
	
	public void onItemClick(AdapterView<?> parent, View view, int pos, long id) {
		
		Log.d(this.LOGCAT, "onItemClick()");
		
		Intent i = new Intent(this, ImageDisplayer.class);
		i.putExtra(mRes.getString(R.string.imageKey), pos);
		startActivity(i);
		
		Log.d(this.LOGCAT, "onItemClick() done");
	}

}
