package edu.usu.wr.cloudutils;

import static org.junit.Assert.*;

import org.jclouds.blobstore.BlobStore;
import org.junit.Test;

public class AuthenticationUtilsTest {

	@Test
	public void test() {
		AuthenticationUtil authUtils = new AuthenticationUtil();
		
		BlobStore blobStore = authUtils.getBlobStore();
		
		assertNotNull("blobstore =?", blobStore);
	}

}
