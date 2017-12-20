package edu.usu.cloud.wr.imageprocessing;

import java.awt.image.BufferedImage;
import java.io.File;
import java.io.FileWriter;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Hashtable;
import java.util.List;
import java.util.Set;

import edu.usu.cloud.wr.common.Converter;

public class TemplateLibrary {
	
	protected static Hashtable<String, ArrayList<String>> ratioToLabel;
	protected static Hashtable<String, ArrayList<ArrayList<Integer>>> labelToProjection;
	
	protected static int VERTICAL_PROJECTION_VALUE_THRESHOLD = 510; 
	protected static double VERTICAL_PROJECTION_NUM_THRESHOLD = 0;
	
	public String findWord(double mRatio, int [] mProjections){
		
		double rmRatio = (double) Math.round(mRatio * 100) / 100;
		
		ArrayList<String> labelList = lookupRatioToLabel(rmRatio);
		if( labelList != null )
			return lookupLabelToProjection(labelList, mProjections);
		
		return "";
	}
	
	private String lookupLabelToProjection(ArrayList<String> keys, int [] mProjections) {
		
		ArrayList<ArrayList<Integer>> projList;
		
		for(String k : keys){
			projList = labelToProjection.get(k);
			
			//Log.d(this.LOGCAT, "keys for LtoP: " + k);
			
			for(ArrayList<Integer> proj : projList) {
				int num = 0, length = proj.size();
				
				// Compare two projections
				if(length == mProjections.length){
					for(int i = 0; i < length; i++){
						int p = proj.get(i).intValue();
						if( (int) Math.abs(p - mProjections[i]) > VERTICAL_PROJECTION_VALUE_THRESHOLD ){
							num ++;
							
						}
					}
					if( num <= VERTICAL_PROJECTION_NUM_THRESHOLD * length ){
						//Log.d(this.LOGCAT, "Take it");
						return k.substring(0, k.indexOf('_'));
					}
				}
			}
		}
		return "";
	}
	
	private static ArrayList<String> lookupRatioToLabel(double rmRatio){
		Set<String> ratiosSet = ratioToLabel.keySet();
		ArrayList<String> labelList = new ArrayList<String>();
		for(String r : ratiosSet){
			double ratio = new Double(r).floatValue();  
			double roundedRatio = (double) Math.round(ratio * 100) / 100;
			
			if( rmRatio == roundedRatio ) {
				ArrayList<String> labels = ratioToLabel.get(r);
				for(String l : labels)
					labelList.add(l + "_" + r); 
			}
		}
		if( labelList.size() > 0 )
			return labelList;
		return null;
	}
	
	public static void loadLibrary(List<String> library){
		
		// Build hash tables
		ratioToLabel = (Hashtable <String,  ArrayList<String>>) new Hashtable ();
		labelToProjection = (Hashtable<String, ArrayList<ArrayList<Integer>>>) new Hashtable ();
		
		for(String line : library){
			
			// process each record -> [0]: label, [1]: ratio, [2]: projections
			String[] blobInfo = line.split("\t");
			
			if(blobInfo.length != 3) 		// a corrupted record
				continue;
			
			else {
				String bLabel = blobInfo[0];
				String bRatio = blobInfo[1];
				
				String [] projections = blobInfo[2].split(" ");
				ArrayList<Integer> bProjections = new ArrayList<Integer>();
				for(String s : projections)
					bProjections.add(new Integer(s));
					
				// Update ratioToString hash table: <ratio, List of blob names>
				ArrayList<String> blobLabelList;
				if(ratioToLabel.containsKey(bRatio)){
					blobLabelList = ratioToLabel.get(bRatio);
					
					// if bLabel has been stored
					boolean isExist = false;
					for(String l : blobLabelList){
						if(l.equals(bLabel)){
							isExist = true;
							break;
						}
					}
					if(!isExist){
						blobLabelList.add(bLabel);
						ratioToLabel.put(bRatio, blobLabelList);
					}
				}
				else {
					blobLabelList = new ArrayList<String>();
					blobLabelList.add(bLabel);
					ratioToLabel.put(bRatio, blobLabelList);
				}
				
				// Update stringTpProjection hash table <blob name, List of projections>
				String key = bLabel + "_" + bRatio;
				ArrayList<ArrayList<Integer>> projList;
				if( labelToProjection.containsKey(key) ){
					projList = labelToProjection.get(key);
					if( !projList.contains(bProjections) )
						projList.add(bProjections);
				}
				else{
					projList = new ArrayList<ArrayList<Integer>> ();
					projList.add(bProjections);
				}
				labelToProjection.put(key, projList);
			}
		}
	}
	
	
	
	public static void createTextFile(String path) throws IOException{

		File inputFolder = new File(path);
		inputFolder.mkdirs();
		StringBuilder table = new StringBuilder();
		
		for(File imageFile: ImageReaderWriter.readImageFiles(path)) {
			BufferedImage image = Converter.imageFileToBufferedImage(imageFile);
		    String imageName = imageFile.getName();
		    imageName = imageName.substring(0, imageName.indexOf("."));
		    if(imageName.indexOf("_") != -1)
		    	imageName = imageName.substring(0, imageName.indexOf("_"));
		    
		    // Down scale the image
		    BufferedImage resizedImage = scaleDown(image);
		    
		    // Do projection
		    Pixels curPix = new Pixels(imageName, resizedImage);
		    int [] projection = Projection.verticalProjection(curPix);
		    
		    double ratio = resizedImage.getHeight() / (double) resizedImage.getWidth();
		    
//		    table.append(imageName + "\t");
		    double roundedRatio = (double) Math.round(ratio * 10000) / 10000;
		    table.append(roundedRatio);
		    table.append("\t");
		    for(int i = 0; i < projection.length; i ++)
		    	table.append(projection[i] + " ");
		    table.append("\n");
		}
		
		writeTXT(path, table.toString());
	}
	
	public static BufferedImage scaleDown(BufferedImage bi){
		
		int width = bi.getWidth();
	    int height = bi.getHeight();
	    int newHeight = 10;
	    int newWidth = (int) width * newHeight / height;
	    return resizeImage(bi, newWidth, newHeight);
	}
	
	private static BufferedImage resizeImage(BufferedImage bi, int newWidth,int newHeight) {
		
		Pixels myPix = new Pixels("originImage", bi);
		int width = myPix.getWidth();
		int height = myPix.getHeight();
		myPix.genBinaryPixels();
		
		BufferedImage newBI = new BufferedImage(newWidth, newHeight, BufferedImage.TYPE_4BYTE_ABGR);
	    Pixels newPix = new Pixels("downScaled", newBI);
	    
	    double x_ratio = width / (double)newWidth ;
	    double y_ratio = height / (double)newHeight ;
	    double px, py ; 
	    
	    for (int row = 0; row < newHeight; row++) {
	        for (int col = 0; col < newWidth; col++) {
	            px = Math.floor( col * x_ratio ) ;
	            py = Math.floor( row * y_ratio ) ;
	            newPix.setBinaryPixel(row, col, (short) myPix.getBinaryPixel((int) py, (int) px));
	        }
	    }
	    return newPix.convertToBufferedImage();
	}
	
	// Write a text file to SD card
	private static void writeTXT(String path, String text) throws IOException {
	    
	    try {
	    	FileWriter file = new FileWriter(path);
	    	file.write(text);
	        file.flush();
	        file.close();

	    } catch (Exception e) {
	           e.printStackTrace();
	    }
	}
}
