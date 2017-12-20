package com.example.alignednutritiontablelocalization;

import android.util.Log;

public class Projection {
	
	final String LOGCAT = Projection.class.getSimpleName() + "LOGCAT";
	
	private Pixels image = null;
	private int [] horizonProj;
	private int [] verticalProj;
	private int width, height;
	private int xstart, xend;
	private int ystart, yend;
	private int ver_MP_Threshold = 4000;
	private int hor_MP_Threshpld = 1000;
	
	Projection(Pixels i){
		image = i;
		width = image.getWidth();
		height = image.getHeight();
		horizonProj = new int [height];
		verticalProj = new int [width];
		xstart = 0;
		xend = width;
		ystart = height;
		yend = 0;
	}
	
	private void verticalProjection(){
		
		for(int col = 0; col < width; col++){
			int gx = 0;	
	    	for(int row = 0; row < height; row++){
	    		gx += image.getPixel(row, col);	  
	    	}
	    	verticalProj[col] = gx;
		}
	}
	
	private void horizontalProjection(){
		
		for(int row = 0; row < height; row++){
			int fx = 0;	
	    	for(int col = 0; col < width; col++){
	    		fx += image.getPixel(row, col);
		    	}
	    	horizonProj[row] = fx;
		}
	}

	private int meanVerticalProjection(){
		int width = image.getWidth();
		int sumPro = 0;
		for(int col = 0; col < width; col++)
			sumPro += verticalProj[col];
		return (int) Math.ceil(sumPro / width);
	}
	
	private int meanHorizontalProjection(){
		int height = image.getHeight();
		int sumPro = 0;
		for(int row = 0; row < height; row++)
			sumPro += horizonProj[row];
		return (int) Math.ceil(sumPro / height);
	}
	
	private void verticalBoundries(){
		int meanVerProj = meanVerticalProjection();
		Log.d(this.LOGCAT, "meanVerProj: " + meanVerProj);
		int lastCol = width - 1;

		for(int col = 0; col < Math.ceil(verticalProj.length / 2); col++){
			if(verticalProj[col] >= meanVerProj)
				if(col > xstart && col < xend)
					xstart = col;
			if(verticalProj[lastCol - col] >= meanVerProj)
				if(lastCol - col < xend && lastCol - col > xstart)
					xend = lastCol - col;
		}
		Log.d(this.LOGCAT, "xstart: " + xstart + " xend: " + xend);
	}
	
	private void extendVerticalBoundries(){
		
		int leftMargin = 0; 
		int rightMargin = 0;
		int meanVerProj = meanVerticalProjection() + ver_MP_Threshold;
	
		for(int col = 0; col < verticalProj.length; col++){
			if(verticalProj[col] >= meanVerProj){
				if(col <= xstart && xstart - col > leftMargin)
					leftMargin = xstart - col;
				else if(col >= xend && col - xend > rightMargin)
					rightMargin = col - xend;
			}
		}
		xstart -= leftMargin;
		xend += rightMargin;
		Log.d(this.LOGCAT, "xstartE: " + xstart + " xendE: " + xend);
	}
	
	private void horizontalBoundries(){
		int meanHorizontalProj = meanHorizontalProjection();
		Log.d(this.LOGCAT, "meanHorizontalProj: " + meanHorizontalProj);
		
		for(int row = 0; row < height; row++){
			if(horizonProj[row] >= meanHorizontalProj - hor_MP_Threshpld){
				if(row < ystart)
					ystart = row;
				else if(row > yend)
					yend = row;
			}
		}
		Log.d(this.LOGCAT, "ystart: " + ystart + " yend: " + yend);
	}
	
	public int getXstart(){
		return xstart;
	}
	
	public int getXend(){
		return xend;
	}
	
	public int getYstart(){
		return ystart;
	}
	
	public int getYend(){
		return yend;
	}
	
	public void findVerticalBoundries(){
		verticalProjection();
		verticalBoundries();
		extendVerticalBoundries();
	}
	
	public void findHorizontalBoundries(){
		horizontalProjection();
		horizontalBoundries();
	}
}
