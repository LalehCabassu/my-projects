package com.example.linedetection_houghtransforms;

import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;

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

public class HoughTransformsAct extends ListActivity 
	implements OnItemClickListener {
	
	final String LOGCAT = HoughTransformsAct.class.getSimpleName() + "LOGCAT";
	Resources mRes = null;
	String[] mImageNumbers = null;
	static String OutputAddress = Environment.getExternalStorageDirectory().getPath();

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		//setContentView(R.layout.activity_hough_transforms);
		
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
		getMenuInflater().inflate(R.menu.hough_transforms, menu);
		return true;
	}

	@Override
	public void onItemClick(AdapterView<?> lv, View v, int imageId, long id) {
		Log.d(this.LOGCAT, "onItemClick()");
		
		Bitmap bm = null;
		switch (imageId) {
		case 0: 
			bm = BitmapFactory.decodeResource(getResources(),R.drawable.image01);
			break;
		case 1:
			bm = BitmapFactory.decodeResource(getResources(),R.drawable.image02);
			break;
		case 2:
			bm = BitmapFactory.decodeResource(getResources(),R.drawable.image03);
			break;
		case 3:
			bm = BitmapFactory.decodeResource(getResources(),R.drawable.image04);
			break;
		case 4:
			bm = BitmapFactory.decodeResource(getResources(),R.drawable.image05);
			break;
		}
		Log.d(this.LOGCAT, "Decode to Bitmap");
		
		Bitmap outBitmap = HoughTransform.detectLines(bm);
		Log.d(this.LOGCAT, "HoughTransform() done");
		
		File outputFolder = new File(OutputAddress);
        outputFolder.mkdirs();
		String file_name = "image_hough_transformed";
		
		try {
            FileOutputStream out = new FileOutputStream(OutputAddress + "/output_images/" + file_name + ".png");
            outBitmap.compress(Bitmap.CompressFormat.PNG, 90, out);
            out.close();
        }
        catch (IOException ex){
            ex.printStackTrace();
        }
        Intent parentIntent = new Intent();
		parentIntent.putExtra(mRes.getString(R.string.activity_result_key), "/output_images/" + file_name+ ".png");
		this.setResult(RESULT_OK, parentIntent);
		this.finish();

		Log.d(this.LOGCAT, "onItemClick() done");
		
	}

}
