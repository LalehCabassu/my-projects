package com.example.linedetection_houghtransforms;

import java.io.File;
import android.os.Bundle;
import android.os.Environment;
import android.app.ListActivity;
import android.content.Intent;
import android.content.res.Resources;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Color;
import android.util.Log;
import android.view.Menu;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.ListView;
import android.widget.AdapterView.OnItemClickListener;

public class EdgeDetectorAct extends ListActivity 
	implements OnItemClickListener {

	final String LOGCAT = EdgeDetectorAct.class.getSimpleName() + "LOGCAT";
	Resources mRes = null;
	String[] mImageNumbers = null;
	static String OutputAddress = Environment.getExternalStorageDirectory().getPath();
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		//setContentView(R.layout.activity_edge_detector);
		
		mRes = getResources();
		mImageNumbers = mRes.getStringArray(R.array.imageNumbers);
		
		ListView lv = this.getListView();
		lv.setBackgroundColor(Color.WHITE);
		
		ArrayAdapter<String> mAdaptor = 
		new ArrayAdapter<String>(this, 
			android.R.layout.simple_list_item_1, 
			this.mImageNumbers);
		
		Log.d(this.LOGCAT, "ArrayAdapter created.");
		
		lv.setAdapter(mAdaptor);
		lv.setTextFilterEnabled(true);
		lv.setOnItemClickListener(this);	
		
		Log.d(this.LOGCAT, "onCreate() done");
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.edge_detector, menu);
		return true;
	}

	@Override
	public void onItemClick(AdapterView<?> lv, View v, int imageId, long id) {
		Log.d(this.LOGCAT, "onItemClick()");
		
		Bitmap bm = null;
		Pixels pxIn = null;
		Pixels pxOut = null;
		
		switch (imageId) {
		case 0: 
			bm = BitmapFactory.decodeResource(getResources(),R.drawable.image01);
			pxIn = new Pixels("image_01", bm);
			pxOut = new Pixels("image_01", bm);
			break;
		case 1:
			bm = BitmapFactory.decodeResource(getResources(),R.drawable.image02);
			pxIn = new Pixels("image_02", bm);
			pxOut = new Pixels("image_02", bm);
			break;
		case 2:
			bm = BitmapFactory.decodeResource(getResources(),R.drawable.image03);
			pxIn = new Pixels("image_03", bm);
			pxOut = new Pixels("image_03", bm);
			break;
		case 3:
			bm = BitmapFactory.decodeResource(getResources(),R.drawable.image04);
			pxIn = new Pixels("image_04", bm);
			pxOut = new Pixels("image_04", bm);
			break;
		case 4:
			bm = BitmapFactory.decodeResource(getResources(),R.drawable.image05);
			pxIn = new Pixels("image_05", bm);
			pxOut = new Pixels("image_05", bm);
			break;
		}
		Log.d(this.LOGCAT, "Decode to Bitmap");
		
		EdgeDetector.detect(pxIn, pxOut);
		Log.d(this.LOGCAT, "EdgeDtector() done");
		
		File outputFolder = new File(OutputAddress);
        outputFolder.mkdirs();
		
        String file_name = pxOut.getName() + "_edge_detected";
        try{
        	pxOut.writePixelsToFile(OutputAddress + "/output_images/", file_name);
	        Log.d(this.LOGCAT, "Write into SD card: done, " + OutputAddress + "/output_images/" + file_name);
        }
        catch(Exception ex){
        	Log.d(this.LOGCAT, "Write into SD card: failed");
        }
        
        Intent parentIntent = new Intent();
		parentIntent.putExtra(mRes.getString(R.string.activity_result_key), "/output_images/" + file_name+ ".png");
		this.setResult(RESULT_OK, parentIntent);
		this.finish();

		Log.d(this.LOGCAT, "onItemClick() done");
	}
}
