

import java.io.BufferedWriter;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileWriter;
import java.io.IOException;
import java.util.ArrayList;
import java.util.Collections;
import java.util.Enumeration;
import java.util.Hashtable;
import java.util.Random;
import java.util.Scanner;

public class BooleanNetwork {

	protected int [][] connGraph;
	private int X;
	private int Y;
	private boolean [][][] networkWiring;
	private boolean [][] truthTable;
	private boolean [][] truthTable_inter;
	private int numberOfCells;
	private int connectivityDegree;		// connectivity degree should not be more than 4
	private int numberOfNodes;
	private int numberOfInput; 
	private int commBandwidth;				// Communication bandwidth: number of genes signal their neighbors
	
	private Random randGen;
	private ArrayList<ArrayList<Integer>> sameAttractors;
	
	private ArrayList<ArrayList<Integer>> allAttractors;
	private int time;									// Current time
	private boolean [][][] stateSlate;					// State Transition Graph (G) for all cells [nodes][2]
	private Hashtable<Integer, Integer> [] cellStates;		// Keep all states for each cell 
														// Key: state index		Value: state value
	private int currentState;
	private boolean SLP;
	
	private Hashtable<Integer, String> stateColorTable;
	
	//------------------------------------ COSTRUCTORs ------------------------------------
	
	BooleanNetwork(String inputFile, boolean slp){
		if(SLP)
			readGridFromInputFile(inputFile);
		numberOfCells = X * Y;
		randGen = new Random();
		
		sameAttractors = new ArrayList<ArrayList<Integer>> ();
		init_connGraph();
		
		stateSlate = new boolean [numberOfCells][numberOfNodes][2];
		
		cellStates = (Hashtable<Integer,Integer>[]) new Hashtable<?,?>[numberOfCells];
		allAttractors = new ArrayList<ArrayList<Integer>> ();
		
		SLP = slp;
		stateColorTable = new Hashtable<Integer, String>();
		initializeCellsRandomly();
	}
	
	BooleanNetwork(int x, int y, int number_of_nodes, int number_of_input, int connectivity_degree, int bandwidth){
		X = x;
		Y = y;
		numberOfCells = X * Y;
		randGen = new Random();
		//numberOfNodes = randGen.nextInt(number_of_nodes) + 1;
		numberOfNodes = randomInt(bandwidth, number_of_nodes);
		System.out.println(numberOfNodes);
		numberOfInput = number_of_input;
		connectivityDegree = connectivity_degree;
		commBandwidth = bandwidth;
		
		sameAttractors = new ArrayList<ArrayList<Integer>> ();
		stateSlate = new boolean [numberOfCells][numberOfNodes][2];
		cellStates = (Hashtable<Integer,Integer>[]) new Hashtable<?,?>[numberOfCells];
		allAttractors = new ArrayList<ArrayList<Integer>> ();
		SLP = false;
		stateColorTable = new Hashtable<Integer, String>();
		init_connGraph();
		randomNet();
		
		initializeCellsRandomly();
	}
	
	private void init_connGraph(){
		connGraph = new int [numberOfCells][connectivityDegree];
		
		int [][] grid = new int [X][Y];
		int cellID =0;
		
		for(int i = 0; i < X; i++)
			for(int j = 0; j < Y; j++)
				grid[i][j] = cellID++;
		
		for(int i = 0; i < X; i++){
			for(int j = 0; j < Y; j++){		// for each node
				int connDeg = 0;
				cellID = i * Y + j;
				
				if( connectivityDegree == 2 ) {
					// The first neighbor
					if( i - 1 >= 0 )
						connGraph[cellID][connDeg++] = grid[i - 1][j];
					else
						connGraph[cellID][connDeg++] = grid[X - 1][j];
					
					// The second neighbor
					connGraph[cellID][connDeg++] = grid[i][(j + 1) % Y];
				}
				else if ( connectivityDegree == 4 ) {
					// The first neighbor
					if( i - 1 >= 0 )
						connGraph[cellID][connDeg++] = grid[i - 1][j];
					else
						connGraph[cellID][connDeg++] = grid[X - 1][j];
					
					// The second neighbor
					connGraph[cellID][connDeg++] = grid[i][(j + 1) % Y];
					
					// The third neighbor
					if( j - 1 >= 0 )
						connGraph[cellID][connDeg++] = grid[i][j - 1];
					else
						connGraph[cellID][connDeg++] = grid[i][Y - 1];
					
					//The fourth neighbor
					connGraph[cellID][connDeg++] = grid[(i + 1) % X][j];
				}
			}
		}
	}
	
	public void randomNet(){
		
		//networkWiring = new boolean [numberOfCells][numberOfNodes][connectivityDegree + 1][numberOfNodes];
		networkWiring = new boolean [numberOfNodes][connectivityDegree + 1][numberOfNodes];
		
		// Initialize 'networkWiring'
		for(int i = 0; i < numberOfNodes; i++)
			for(int j = 0; j <= connectivityDegree; j++)
				for(int l = 0; l < numberOfNodes; l++)
					networkWiring[i][j][l] = false;
		
		// Build intra-cellular network
		for(int i = 0; i < numberOfNodes; i++){				//each function
			for(int n = 0; n < numberOfInput; n++){			// numberOfInput (K)-> K = 1 (ordered), 2 (critical), 3 (chaotic) 
				int randNode = randGen.nextInt(numberOfNodes);
				while(networkWiring[i][0][randNode])
					randNode = randGen.nextInt(numberOfNodes);
				networkWiring[i][0][randNode] = true;		
			}
		}
		
		// Orthogonal Signaling: Fill neighbors -> Build inter-cellular network
		int [] receivers = new int [commBandwidth];
		for(int i = 0; i < commBandwidth; i++)
			receivers[i] = -1;
		
		for(int i = 0; i < commBandwidth; i++) {
			int senderNode = randGen.nextInt(numberOfNodes);
			int receiverNode = randGen.nextInt(numberOfNodes);
			//while( networkWiring[receiverNode][1][senderNode] ) {	// if it is already taken
			while( isInList(receivers, receiverNode) ) {	// if it is already taken
				receiverNode = randGen.nextInt(numberOfNodes);
			}
			receivers[i] = receiverNode;
			//System.out.println("sender gene: " + senderNode + " receiverGene: " + receiverNode);
			for(int j = 1; j <= connectivityDegree; j++){	
				networkWiring[receiverNode][j][senderNode] = true;
			}
		}

		// Initialize 'truthTable' -> random intra-cellular network
		truthTable = new boolean [numberOfNodes][(int) Math.pow(2, numberOfInput)];
		for(int i = 0; i < numberOfNodes; i++)
			for(int j = 0; j < truthTable[0].length; j++)
				truthTable[i][j] = randGen.nextBoolean();
		
		// Truth table for inter-cellular network -> OR operation
		truthTable_inter = new boolean [numberOfNodes][(int) Math.pow(2, connectivityDegree + 1)];
		for(int i = 0; i < numberOfNodes; i++){
			truthTable_inter[i][0] = false;
			for(int j = 1; j < truthTable_inter[0].length; j++)
				truthTable_inter[i][j] = true;
		}
		
		
	}
	
	private boolean isInList(int [] list, int n){
		for(int i = 0; i < list.length; i++){
			if(list[i] == n)
				return true;
		}
		return false;
	}
	
	// Update the network itr times
	public void update(int itr){
		
		for(int i = 0; i < itr; i++){
			for(int j = 0; j < numberOfCells; j++)
				updateOnce(j);
			//printCurrentBinaryState();
			time++;
		}
		
		
		System.out.println("\nprintStates");
		for(int i = 0; i < numberOfCells; i++){
			System.out.print(i + ": ");
			
			printStates(i);
			
			System.out.println();
		}
		
		System.out.println("\nprintAttractors");
		
		for(int i = 0; i < numberOfCells; i++){
			System.out.print(i + ": ");
		
			findAttractors(i);
			printAttractors(i);
			
			System.out.println();
		}
		
		System.out.println("\nprintSameAttractors: ");
		findCellsOfSameAttractors();
		printSameAttractors();
	}
	
	
	// Print cells in same attractors
	public void printSameAttractors(){
		
		System.out.println("nSameAttr: " + sameAttractors.size());
		for(ArrayList<Integer> l : sameAttractors){
			for(int i = 0; i < l.size(); i++)
				System.out.print(l.get(i) + " ");
			System.out.println();
		}
		
	}

	//------------------------------ Private Methods ------------------------------ 
	// Find cells of the same attractors
	private void findCellsOfSameAttractors(){
		ArrayList<Integer> matchedAttractors = new ArrayList<Integer>() ;
		
		for(int i = 0; i < numberOfCells; i++){
			if(matchedAttractors.contains(i))
				continue;
			
			ArrayList<Integer> others = new ArrayList<Integer>();				// Others with the same attractor
			others.add(i);
			ArrayList<Integer> curAttractors = allAttractors.get(i);
			
			for(int j = i + 1; j < numberOfCells; j++){							// For each other attractors
				ArrayList<Integer> otherAttractors = allAttractors.get(j);
	
				if(curAttractors.size() == otherAttractors.size()){				// Only attractors of the same length
					int csIndx = 0, osIndx = 0;
					boolean match = true;
					
					while(csIndx < curAttractors.size() && match){				// For each state
						int first = curAttractors.get(csIndx).intValue();
						int second = otherAttractors.get(osIndx % otherAttractors.size()).intValue();
						if( first == second ){
							csIndx++;	osIndx++;
						}
						else if( csIndx != 0 )
							match = false;
						else if( osIndx == otherAttractors.size() - 1 )
							match = false;
						else
							osIndx ++;
					}
					if( match ){
						others.add(j);
						matchedAttractors.add(j);
					}
				}	
			}
			sameAttractors.add(others);
		}
	}
	
	// Update the network just once 
	public void updateOnce(int cell){
		
		for(int i = 0; i < numberOfNodes; i++){
			if( SLP && i == 0 )
				continue;
			int index = indexInTruthTable(cell, i);
			boolean internal_output = truthTable[i][index];
			int intra_index = indexInTruthTable_inrta(cell, i, internal_output);
			stateSlate[cell][i][(time + 1) % 2] = truthTable_inter[i][intra_index];
		}
		//time ++;
		cellStates[cell].put(cellStates[cell].keys().nextElement() + 1, getCurrentState(cell));
		
	}	
	
	// Find attractors
	public void findAttractors(int cell){
		
		Enumeration<Integer> statesEnum = cellStates[cell].elements();							 
		ArrayList<Integer> statesList = Collections.list(statesEnum);			// List of states
		boolean found = false;
		int numberOfStates = statesList.size();
		
		ArrayList<Integer> curAttractors = new ArrayList<Integer>();
		
		for(int index = 0; index < numberOfStates - 1 && !found; index++){
			Integer curState = statesList.get(index);
			if(curAttractors.isEmpty())
				curAttractors.add(curState);
			else if(!(curAttractors.contains(curState) && curAttractors.indexOf(curState) == 0))
				curAttractors.add(curState);
			else{
				for(int i = 1; i <= index  && index + i < statesList.size(); i++){
					int first = statesList.get(i).intValue();
					int second = statesList.get(index + i).intValue();
					if(first != second){
						curAttractors.add(curState);
						break;
					}
					if(i == index)
						found = true;	
				}
			}
		}
		allAttractors.add(curAttractors);
		
	}

	public boolean getNodeState(int cellID, int nodeID){
		return stateSlate[cellID][nodeID][time %2];
	}
	
	// Convert the base of a number from binary to decimal
	public int getCurrentState(int cellID){
		int dec = 0;
		for(int j = 0; j < numberOfNodes; j++){
			if(stateSlate[cellID][j][time % 2])
				dec += (int)(Math.pow(2, j));
		}
		currentState = dec;
		return currentState;
	}
	
	// Print attractors
	public void printAttractors(int cell){
	
		int nAttractors = allAttractors.get(cell).size();
		for(int i = 0; i < nAttractors; i++)
			System.out.print(allAttractors.get(cell).get(i) + " ");
	}
	
	// Print the hash table attractors
	public void printStates(int cell){
		Enumeration<Integer> curSet = cellStates[cell].elements();	
		while(curSet.hasMoreElements())
			System.out.print(curSet.nextElement() + " ");
	}
		
	// Print the current binary state
	public void printCurrentBinaryState(int cellID){
		for(int i = numberOfNodes - 1; i >= 0 ; i--){
			if( stateSlate[cellID][i][time%2] )
				System.out.print("1");
			else
				System.out.print("0");
		}
	}	


	// Initialize the initial state of all nodes in intra-cellular networks randomly (uniform distribution)
	private void initializeCellsRandomly(){
		boolean randBool;
		
		for(int i = 0; i < numberOfCells; i++)
			cellStates[i] = new Hashtable<Integer,Integer>();
		
		for(int i = 0; i < numberOfCells; i++){
			for(int j = 0; j < numberOfNodes; j++){
				if(SLP && j == 0){
					if((i % 4) + 1 == 1 || (i % 4) + 1 == 2)
						stateSlate[i][j][0] = stateSlate[i][j][1] = false;
					else
						stateSlate[i][j][0] = stateSlate[i][j][1] = true;
				}
				else{
					randBool = randGen.nextBoolean();
					stateSlate[i][j][0] = stateSlate[i][j][1] = randBool;
					//stateSlate[i][j][0] = stateSlate[i][j][1] = true;
				}
			}
			cellStates[i].put(0, getCurrentState(i));
		}
		
		System.out.println("Initial states: ");
		for(int i = 0; i < numberOfCells; i++){
			//printCurrentBinaryState(i);
			System.out.print(getCurrentState(i) + " ");
		}
	}
		
	// Find the regarding index in truthTable [][]
	private int indexInTruthTable(int cell, int func) {
		int state = 0, k = 0;
		
		// Inputs for the current cell
		for(int j = 0; j < numberOfNodes; j++){		 					
			if( networkWiring[func][0][j] ){		// if it has a neighbor
				if( getNodeState(cell, j) ){
					state += Math.pow(2, k);
					
				}
				k++;
			}
		}
		
		return state;
	}
	
	// Find the regarding index in truthTable [][]
	private int indexInTruthTable_inrta(int cell, int func, boolean internal) {
		int state = 0, k = 1;
		
		// Output of internal signals
		if(internal)
			state = 1;
		
		// Inputs from the neighborhood
		for(int i = 1; i <= connectivityDegree; i++){						// check on each cell (degree of connectivity of the graph)
			int neighbor = connGraph[cell][i - 1];								// look up a neighbor's ID in connGraph
			for(int j = 0; j < numberOfNodes; j++){		 					
				if( networkWiring[func][i][j]){						// here is an input
					if( neighbor != -1 ){								// here is a neighbor
						if( getNodeState(neighbor, j)){
							state += Math.pow(2, k);
							
						}
						k++;
					}
				}
			}
		}
		return state;
	}
	
	
	public void plotOutCurState(String outputAddress, int itr){
		String pov_header = "global_settings {\n  assumed_gamma 1\n}\nlight_source {\n  <-0.6, 1.6, 3.7>*10000\n  rgb 1.3\n}\ncamera {\n  location <0,0,100>\n  look_at <0,0,0>\n}\nbackground {\n  color rgb < 0.87, 0.97, 0.97 >\n}";
		String sphere_header = "\nsphere {\n";
		String pigment_header = "\npigment { color rgb ";
		String pigment_tail = "}";
		String sphere_tail = "}"; 
		StringBuilder pov_code_inti = new StringBuilder();
	
		int sphere_radius = 1;
		int x_dim = X * sphere_radius, y_dim = Y * sphere_radius;
		int z = 0;
		int cellID = 0;
		
		
		pov_code_inti.append(pov_header);
		
		for(int x = -(x_dim - sphere_radius); x <= (x_dim - sphere_radius); x += sphere_radius * 2){
			for(int y = -(y_dim - sphere_radius); y <= (y_dim - sphere_radius); y += sphere_radius * 2){
				// location
				pov_code_inti.append(sphere_header);
				String loc ="< " + x + ", " + y + ", " + z + " >, " + sphere_radius + "\n";
				pov_code_inti.append(loc);
				
				// color
				pov_code_inti.append(pigment_header);
				
				String color = "";
				Integer key = cellStates[cellID].get(itr);
				if(stateColorTable.containsKey(key))
					color = stateColorTable.get(key);
				else {
					double red = randGen.nextDouble();
					double green = randGen.nextDouble();
					double blue = randGen.nextDouble();
					color = "< " + red + ", " + green + ", " + blue + " >\n";
					stateColorTable.put(getCurrentState(cellID), color);
				}
				pov_code_inti.append(color + pigment_tail);
				
				pov_code_inti.append(sphere_tail);
				cellID ++;
			}
		}
		
		try {
			 
			File file = new File(outputAddress);
			if(file.mkdirs()){
 
				FileWriter fw = new FileWriter(outputAddress + "inti_" + itr + ".pov");
				BufferedWriter bw = new BufferedWriter(fw);
				bw.write(pov_code_inti.toString());
				bw.close();
	 
				System.out.println("Done");
			}
 
		} catch (IOException e) {
			e.printStackTrace();
		}
	}
	
	public void plotOutAttractos(String outputAddress, int itr){
	
		String pov_header = "global_settings {\n  assumed_gamma 1\n}\nlight_source {\n  <-0.6, 1.6, 3.7>*10000\n  rgb 1.3\n}\ncamera {\n  location <0,0,100>\n  look_at <0,0,0>\n}\nbackground {\n  color rgb < 0.87, 0.97, 0.97 >\n}";
		String sphere_header = "\nsphere {\n";
		String pigment_header = "\npigment { color rgb ";
		String pigment_tail = "}";
		String sphere_tail = "}"; 
		
		double sphere_radius = 1;
		double x_dim = X * sphere_radius, y_dim = Y * sphere_radius;
		int z = 0;
		
		StringBuilder pov_code_att = new StringBuilder();
		int cellID = 0;
		pov_code_att.append(pov_header);
		
		Hashtable<Integer, String> attractorColorTable = new Hashtable<Integer, String>();
		String color = "";
		
		for(ArrayList<Integer> attr : sameAttractors){
			if(attr.size() == 1){
				int cell = attr.get(0);
				Integer state = allAttractors.get(cell).get(0);
				color = stateColorTable.get(state);
			}
			if( attr.size() > 1 || color == null ) {
				double red = randGen.nextDouble();
				double green = randGen.nextDouble();
				double blue = randGen.nextDouble();
				color = "< " + red + ", " + green + ", " + blue + " >\n";			
			}
			int attrIndex = sameAttractors.indexOf(attr);
			attractorColorTable.put(attrIndex, color);
		}
		
		for(double x = -(x_dim - sphere_radius); x <= (x_dim - sphere_radius); x += sphere_radius * 2){
			for(double y = -(y_dim - sphere_radius); y <= (y_dim - sphere_radius); y += sphere_radius * 2){
				// location
				pov_code_att.append(sphere_header);
				String loc ="< " + x + ", " + y + ", " + z + " >, " + sphere_radius + "\n";
				pov_code_att.append(loc);
				
				// color
				pov_code_att.append(pigment_header);
				
				Integer key = findAttractorList(cellID);
				if ( key.intValue() != -1 ) {
					color = attractorColorTable.get(key);
				}
				pov_code_att.append(color + pigment_tail);
				pov_code_att.append(sphere_tail);
				cellID ++;
			}
		}
		
		try {
			 
			File file = new File(outputAddress);
			if(file.mkdirs()){
				
				FileWriter fw = new FileWriter(outputAddress + "attr_" + itr + ".pov");
				BufferedWriter bw = new BufferedWriter(fw);
				bw.write(pov_code_att.toString());
				bw.close();
	 
				System.out.println("Done");
			}
 
		} catch (IOException e) {
			e.printStackTrace();
		}
	}
	
	private int findAttractorList(int cellID){
		for(ArrayList<Integer> list : sameAttractors){
			for(Integer id : list){
				if(id.intValue() == cellID)
					return sameAttractors.indexOf(list);
			}
		}
		
	
		return -1;
	}
	
	
	// Initialize the grid from an input file (.txt)
	private void readGridFromInputFile(String inputAddress){		
				
		File inputFile = new File(inputAddress);
		try {
			Scanner s = new Scanner(inputFile);
			String line;
			if(s.hasNext()){
				numberOfNodes = s.nextInt();
				numberOfInput = s.nextInt();
				connectivityDegree = s.nextInt();
				X = s.nextInt();
				Y = s.nextInt();
				numberOfCells = X * Y;
				
				networkWiring = new boolean [numberOfNodes][connectivityDegree + 1][numberOfNodes];
				truthTable = new boolean [numberOfNodes][(int) Math.pow(2, numberOfInput)];
				
					for(int j = 0; j < numberOfNodes; j++)
						for(int k = 0; k <= connectivityDegree; k++)
							for(int l = 0; l < numberOfNodes; l++)
								if(s.nextInt() == 1)
									networkWiring[j][k][l] = true;
								else
									networkWiring[j][k][l] = false;
				
				for(int i = 0; i < truthTable.length; i ++){
					for(int j = 0; j < truthTable[0].length; j++)
						if(s.nextInt() == 1) {
							truthTable[i][j] = true;
						}
						else {
							truthTable[i][j] = false;
						}
				}
			}
		} catch (FileNotFoundException e) {
			// TODO Auto-generated catch block
			System.out.println(e.toString());
			e.printStackTrace();
		}		
	}
	
	// Generate a random integer between min(included) and max(excluded)
	int randomInt(int min, int max){
		return min + (int)(Math.random() * ((max - min - 1) + 1));
	}

}
