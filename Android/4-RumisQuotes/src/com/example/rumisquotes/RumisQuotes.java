package com.example.rumisquotes;

import java.util.Random;

//import com.example.intdatedisplay.R;

import android.os.Bundle;
import android.app.Activity;
import android.view.Menu;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.EditText;

public class RumisQuotes extends Activity implements OnClickListener {
	Button myButtonQuote = null;
	Button myButtonClear01 = null;
	Button myButtonClear02 = null;
	
	EditText myTextQuote01 = null;
	EditText myTextQuote02 = null;
	
	Random myRandGen = null;
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_rumis_quotes);
		
		myButtonQuote = (Button) this.findViewById(R.id.myQuoteButton);
		myButtonClear01 = (Button) this.findViewById(R.id.myClear01Button);
		myButtonClear02 = (Button) this.findViewById(R.id.myClear02Button);
		
		myTextQuote01 = (EditText) this.findViewById(R.id.myQuote01);
		myTextQuote02 = (EditText) this.findViewById(R.id.myQuote02);
		
		myRandGen = new Random();
		
		myButtonQuote.setOnClickListener(this);
		myButtonClear01.setOnClickListener(this);
		myButtonClear02.setOnClickListener(this);
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.activity_rumis_quotes, menu);
		return true;
	}

	@Override
	public void onClick(View v) {
		int ID = v.getId();
		
		switch(ID) {
		case R.id.myQuoteButton:
			int num = myRandGen.nextInt(2) + 1;
			if(num == 1)
				fillInQuote01();
			else if(num == 2)
				fillInQuote02();
			break;
		case R.id.myClear01Button:
			myTextQuote01.setText("");
			break;
		case R.id.myClear02Button:
			myTextQuote02.setText("");
			break;
		}
	}
	
	
	private String getRumiQuote() {
		int quoteNum = 1 + myRandGen.nextInt(7);
		String quote = "";
		switch ( quoteNum ) {
		   case 1: 
			   quote = this.getResources().getString(R.string.rumi_quote_01); 
			   break;
		   case 2: 
			   quote = this.getResources().getString(R.string.rumi_quote_02); 
			   break;
		   case 3: 
			   quote = this.getResources().getString(R.string.rumi_quote_03); 
			   break;
		   case 4: 
			   quote = this.getResources().getString(R.string.rumi_quote_04); 
			   break;
		   case 5: 
			   quote = this.getResources().getString(R.string.rumi_quote_05); 
			   break;
		   case 6: 
			   quote = this.getResources().getString(R.string.rumi_quote_06); 
			   break;
		   case 7: 
			   quote = this.getResources().getString(R.string.rumi_quote_07); 
			   break;
		 }
		 return quote;
	}

	private void fillInQuote01() {
	    for(int i = 0; i < 15; i++)
	        myTextQuote01.append("\n" + getRumiQuote() + "\n");
	}
	
	private void fillInQuote02() {
	    for(int i = 0; i < 15; i++)
	        myTextQuote02.append("\n" + getRumiQuote() + "\n");
	}
	

}
