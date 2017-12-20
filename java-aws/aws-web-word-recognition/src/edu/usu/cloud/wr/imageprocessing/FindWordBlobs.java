package edu.usu.cloud.wr.imageprocessing;

import java.awt.image.BufferedImage;
import java.util.ArrayList;

public class FindWordBlobs {

	protected static int NIBLACK_BLOCK_SIZE = 50;
	protected static double NIBLACK_STD_THRESHOLD = 12.7;
	protected static double NIBLACK_CONSTANT = -0.25; 
	 
	protected static int [] vp;
	protected static int vp_mean;
	
	protected static int GAP_LENGTH_THRESHOLD = 3;
	protected static int BLOB_LENGTH_THRESHOLD = 5;
	protected static int GAP_VERTICAL_PROJECTION_THREHOLD = 6;
	protected static BufferedImage invertedColor;
	protected static BufferedImage middleZone;
	protected static BufferedImage blurred;
	protected static BufferedImage originalImage;
	protected static String imageName;
	
	FindWordBlobs(){
	}
	
	FindWordBlobs(BufferedImage bi, String name){
		originalImage = bi;
		imageName = name;
	}
	
	public static ArrayList<BufferedImage> findBlobs(BufferedImage bi, String name){
		originalImage = bi;
		imageName = name;
		
		// Find the middle zone
		middleZone = middleZone();
		
		// Go for vertical projection...
		return findGaps(middleZone);
	}
		
	private static ArrayList<BufferedImage> findGaps(BufferedImage bi){
		
		// Blur the middle zone
		blurred = FilterImage.averageFilter(bi);
		
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
		
		int height = bi.getHeight();
		int numberOfBlobs = croppingIndexes.size() / 2;
		ArrayList<BufferedImage> blobs = new ArrayList<BufferedImage> ();
		
		for (int i = 0; i < numberOfBlobs; i++) {
			int blobStart = croppingIndexes.get(2 * i);
			int blobEnd = croppingIndexes.get(2 * i + 1);
			
			if( blobEnd - blobStart > BLOB_LENGTH_THRESHOLD ) {
				blobs.add(bi.getSubimage(blobStart, 0, (blobEnd - blobStart), height));
			}
		}		
		return blobs;
	}
	
	private static BufferedImage invertColor(BufferedImage bi){
		
		Pixels pixelIn = new Pixels("In", bi);
		Pixels pixelOut = new Pixels("Out", bi);
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
		
		return pixelOut.convertToBufferedImage();
	}
	
	public static BufferedImage middleZone(){
		Pixels image = new Pixels("middleZone", originalImage);
		int height = image.getHeight();
		
		int [] horizonProj = Projection.horizontalProjection(image);
		int baseline = peak(horizonProj, 0, height / 2);
		int midline = peak(horizonProj, height / 2, height);
		
		BufferedImage middleZone_bm = null;
		middleZone_bm = originalImage.getSubimage(0, baseline, originalImage.getWidth(), (midline - baseline));
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
}
