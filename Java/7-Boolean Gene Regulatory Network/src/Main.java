import java.net.URISyntaxException;

public class Main {
	
	static int grid_x = 30;
	static int grid_y = 30;
	static int iteration = 100;
	
	static String outputAddress = "" + System.getProperty("user.dir") + "/output/";
	static String inputAddress = "" + System.getProperty("user.dir") + "/input/net_segment_polarity_32.txt";
	
	public static void main(String[] args) throws URISyntaxException{
	
		//System.out.println("Read from an input file: ");
		//testInputNet(1, inputAddress, true);
        
		System.out.println("\n-------------------------\nCreate a random network: ");
		//for(int i = 0; i < 1; i++)
			testRandomNet(0);
		
	}
	
	static double testInputNet(int num, String inputAddress, boolean SLP){
		
		int [] dSizes = {2, 4, 6, 8};
		int [] cSizes = new int [dSizes.length];
		int [] netSize = {20, 40, 80, 300};
		
		cSizes[num] = (dSizes[num] + 1) * netSize[num];
    	int number_of_cells = cSizes[num]; 
    	int connectivity_degree = dSizes[num];
    	BooleanNetwork test;
    	
    	test = new BooleanNetwork(inputAddress, SLP);
		
    	long startTime, endTime;
		startTime = System.nanoTime();
		test.update(iteration);
		endTime = System.nanoTime();
		double runTime = Math.log10(endTime - startTime);
		
		//test.plotOut(outputAddress);
		
		return runTime;
	}
	
	static void testRandomNet(int num){
		
		int [] netBandwidth = {5, 6, 7, 8};
    	
		int number_of_nodes = 30;
    	int max_number_of_input = 3;		// K = 1 (ordered), 2 (critical), 3 (chaotic) 
    	int connectivity_degree = 4;		// Orthogonal (inputs from north(i - 1, j) and east (i, j + 1)
    	//int connectivity_degree = 4;		// Symmetric
    	//int bandwidth = netBandwidth[num];
    	int bandwidth = 7;
    	
    	for(int i = 0; i < 20; i ++) {
	    	BooleanNetwork test = new BooleanNetwork(grid_x, grid_y, number_of_nodes, max_number_of_input, connectivity_degree, bandwidth);
			test.update(iteration);
			String address = outputAddress + grid_x + "_" + max_number_of_input + "_" + connectivity_degree + "_" + bandwidth + "/" + (i + 1) + "/";
			//test.plotOutCurState(address, 0);
			test.plotOutAttractos(address, iteration);
    	}
        
	}
	
}
