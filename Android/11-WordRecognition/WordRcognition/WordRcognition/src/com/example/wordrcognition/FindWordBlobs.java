package com.example.wordrcognition;

import java.io.File;
import java.io.FileOutputStream;
import java.util.ArrayList;
import java.util.Hashtable;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.Environment;
import android.util.Log;


public class FindWordBlobs {

	final String LOGCAT = FindWordBlobs.class.getSimpleName() + "LOGCAT";
	
	private static String OutputAddress = Environment.getExternalStorageDirectory().getPath() + "/output_word_blobs/";
	private static String InputAddress = Environment.getExternalStorageDirectory().getPath() + "/input_images/";
	
	protected static BitmapFactory.Options BITMAP_OPTIONS = new BitmapFactory.Options();
	protected static Bitmap.Config BITMAP_CONFIG = Bitmap.Config.ARGB_8888;

	protected static int NIBLACK_BLOCK_SIZE = 50;
	protected static double NIBLACK_STD_THRESHOLD = 12.7;
	protected static double NIBLACK_CONSTANT = -0.25; 
	 
	static {
		 BITMAP_OPTIONS.inPreferredConfig = BITMAP_CONFIG;
	}
	
	protected static int [] vp;
	protected static int vp_mean;
	
	protected static int GAP_LENGTH_THRESHOLD = 3;
	protected static int BLOB_LENGTH_THRESHOLD = 5;
	protected static int GAP_VERTICAL_PROJECTION_THREHOLD = 6;
	protected static Bitmap invertedColor;
	protected static Bitmap middleZone;
	protected static Bitmap blurred;
	protected static Bitmap originalImage;
	protected static String imageName;
	
	FindWordBlobs(){
	}
	
	FindWordBlobs(Bitmap bm, String name){
		originalImage = bm;
		imageName = name;
	}
	
	public static void findBlobs(Bitmap bm, String name){
		originalImage = bm;
		imageName = name;
		
		// Find the middle zone
		middleZone = middleZone();
		
		// Go for vertical projection...
		findGaps(middleZone);
	}
	
	public static void findBlobs(){
		
		File inputFolder = new File(InputAddress);
        inputFolder.mkdirs();
        
		for(File imageFile: ImageReader.readImages(InputAddress)) {
		    originalImage = ImageReader.convertImageToBitmap(imageFile, BITMAP_OPTIONS);
		    imageName = imageFile.getName();
		    imageName = imageName.substring(0, imageName.indexOf("."));
			
		    // Find the middle zone
			middleZone = middleZone();
			
			// Go for vertical projection...
			findGaps(middleZone);
		}
	}
	
	private static void findGaps(Bitmap bm){
		
		// Blur the middle zone
		blurred = FilterImage.averageFilter(bm);
		
		// Invert the blurred middle zone
		invertedColor = invertColor(blurred);
		
		
		Pixels image = new Pixels("findGaps", invertedColor);
		int width = image.getWidth();
		
		int [] verticalProjection = Projection.verticalProjection(image);
		int verProj_mean = Projection.meanVerticalProjection(image, verticalProjection); 
		
		vp = verticalProjection;
		vp_mean = verProj_mean;
		
		ArrayList<Integer> croppingIndexes = new ArrayList<Integer>();
		
		int gapStart = -1, gapEnd = -1;
		int col = 0;
		
		int lastGapEnd = 0;
		while( col < width ){
			
			if( verticalProjection[col] == 0 && gapStart == -1 ){						// a gap starts
				gapStart = col;		
				while( col < width - 1 && verticalProjection[++col] == 0 );				// in the gap
				gapEnd = col;															// the gap ends
				
				if( gapEnd > gapStart  && ( lastGapEnd == 0 || gapStart - lastGapEnd > BLOB_LENGTH_THRESHOLD )  ) {
						
					if( gapEnd - gapStart > GAP_LENGTH_THRESHOLD ) {
						croppingIndexes.add(new Integer(gapStart));
						croppingIndexes.add(new Integer(gapEnd));
						lastGapEnd = gapEnd;
					}
					/*
					else if( gapStart - 1 >= 0 &&  verticalProjection[gapStart - 1] >= verProj_mean ){
						croppingIndexes.add(new Integer(gapStart));
						croppingIndexes.add(new Integer(gapEnd));
						
						Log.d(this.LOGCAT, "gapStart: " + gapStart + " gapEnd: " + gapEnd);
						Log.d(this.LOGCAT, "added");
					}
					else if ( gapEnd + 1 < width && verticalProjection[gapEnd + 1] >= verProj_mean ) {
						croppingIndexes.add(new Integer(gapStart));
						croppingIndexes.add(new Integer(gapEnd));
						
						Log.d(this.LOGCAT, "gapStart: " + gapStart + " gapEnd: " + gapEnd);
						Log.d(this.LOGCAT, "added");
					}
					*/
				
				}
				gapStart = -1;
			}
			col++;
		}
		
		if( croppingIndexes.isEmpty() ) {     // No gaps found
			croppingIndexes.add(0, new Integer(0));
			croppingIndexes.add(1, new Integer(width - 1));
		}
		else {
			if( croppingIndexes.get(0).intValue() != 0 )		// fix first index
				croppingIndexes.add(0, new Integer(0));
			else
				croppingIndexes.remove(0);
			
			int lastIndex = croppingIndexes.size() - 1;
			if( croppingIndexes.get(lastIndex).intValue() != width - 1 )		// fix last index
				croppingIndexes.add(lastIndex + 1, new Integer(width - 1));
			else
				croppingIndexes.remove(lastIndex);
		}
		
		int height = bm.getHeight();
		int numberOfBlobs = croppingIndexes.size() / 2;
		ArrayList<Bitmap> blobs = new ArrayList<Bitmap> ();
		
		for (int i = 0; i < numberOfBlobs; i++) {
			int blobStart = croppingIndexes.get(2 * i);
			int blobEnd = croppingIndexes.get(2 * i + 1);
			
			if( blobEnd - blobStart > BLOB_LENGTH_THRESHOLD ) {
				blobs.add(Bitmap.createBitmap(bm, blobStart, 0, (blobEnd - blobStart), height));
			}
		}
		
		// insert the image itself -> just for seeing the result -> DELETE it later
		blobs.add(0, originalImage);
		
		// Write to SD card
		for(int b = 0; b < blobs.size(); b++)
			saveImage(blobs.get(b), b);
	}
	
	
	
	private static Bitmap invertColor(Bitmap bm){
		
		Pixels pixelIn = new Pixels("In", bm);
		Pixels pixelOut = new Pixels("Out", bm);
		int width = pixelIn.getWidth();
		int height = pixelIn.getHeight();
		
		NiblackBinarizer.binarizeImage(pixelIn, NIBLACK_BLOCK_SIZE, NIBLACK_STD_THRESHOLD, NIBLACK_CONSTANT);
		
		for(int row = 0; row < height; row++){
			for(int col = 0; col < width; col++){
				if(pixelIn.getBinaryPixel(row, col) == 255)
					pixelOut.setBinaryPixel(row, col, (short) 0);
				else
					pixelOut.setBinaryPixel(row, col, (short) 255);
			}
		}
		
		return pixelOut.convertToBitmap();
	}
	
	public static Bitmap middleZone(){
		Pixels image = new Pixels("middleZone", originalImage);
		int height = image.getHeight();
		
		int [] horizonProj = Projection.horizontalProjection(image);
		int baseline = peak(horizonProj, 0, height / 2);
		int midline = peak(horizonProj, height / 2, height);
		
		Bitmap middleZone_bm = null;
		middleZone_bm = Bitmap.createBitmap(originalImage, 0, baseline, originalImage.getWidth(), (midline - baseline));
		return middleZone_bm;
	}
	
	// Find maximum in [start, end)
	private static int peak(int [] horizonProj, int start, int end){
		int max = start;
		for(int row = start; row < end; row++)
			if( horizonProj[row] >= horizonProj[max] )
				max = row;
		return max;
	}	
	
	private static void saveImage(Bitmap myBitmap, int b) {

	    File outputFolder = new File(OutputAddress + imageName + "/");
        outputFolder.mkdirs();
        
	    String fname = "blob_" + b + ".png";
	    File file = new File (outputFolder, fname);
	    if (file.exists ()) file.delete (); 
	    try {
	           FileOutputStream out = new FileOutputStream(file);
	           myBitmap.compress(Bitmap.CompressFormat.PNG, 90, out);
	           out.flush();
	           out.close();

	    } catch (Exception e) {
	           e.printStackTrace();
	    }
	}

}
