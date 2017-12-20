package com.example.wordrcognition;

import java.io.File;
import java.io.FileFilter;
import java.util.Locale;

import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
//import android.util.Log;

public class ImageReader {

	//static String DEBUG_TAG = ImageReader.class.getSimpleName() + "_TAG";

	// Convert File to Bitmap
	public static Bitmap convertImageToBitmap(File file, BitmapFactory.Options bm_options) {
		return BitmapFactory.decodeFile(file.getAbsolutePath(), bm_options);
	}

	// Put all images(.bmp, .jpg, .png) in an array of Files
	public static File[] readImages(String directory) {
		File sourceFolder = new File(directory);
		File[] images = sourceFolder.listFiles(
			new FileFilter() {
				@Override
				public boolean accept(File file) {
					String fileName = file.getName().toLowerCase(Locale.getDefault());
					//Log.d(DEBUG_TAG, fileName);
					if (fileName.endsWith(".bmp") || fileName.endsWith(".jpg") || fileName.endsWith(".png"))
						return true;
					return false;
				}
			}
		);
		return images;
	}
}
