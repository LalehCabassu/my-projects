package edu.usu.cloud.wr.model;

import java.net.URI;
import java.util.ArrayList;
import java.util.List;

public class Image {
	
	private String name;
	private URI uri;
	private String recognizedWords;
	
	public Image(String name)
	{
		this.name = name;
		this.recognizedWords = "";
	}
	
	public Image(String name, URI uri)
	{
		this.name = name;
		this.uri = uri;
		this.recognizedWords = "";
	}

	public URI getUri() {
		return uri;
	}

	public void setUri(URI uri) {
		this.uri = uri;
	}
	
	public void setRecognizedWords(List<String> words)
	{
		if(words.size() == 0)
			recognizedWords = "None!";
		
		for(String word: words)
			recognizedWords += word + " ";
	}
	
	public String getRecognizedWords()
	{
		return recognizedWords;
	}
	
	public String getName() {
		return name;
	}
}
